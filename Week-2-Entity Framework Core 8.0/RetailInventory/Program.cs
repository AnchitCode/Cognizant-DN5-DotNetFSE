using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RetailInventory.Data;
using RetailInventory.DTOs;
using RetailInventory.Models;

namespace RetailInventory;

internal static class Program
{
	private static readonly Func<AppDbContext, decimal, IEnumerable<Product>> CompiledProductsByMinimumPriceQuery =
		EF.CompileQuery((AppDbContext context, decimal minimumPrice) =>
			context.Products
				.AsNoTracking()
				.Where(product => product.Price >= minimumPrice)
				.OrderByDescending(product => product.Price));

	private static async Task Main()
	{
		var configuration = new ConfigurationBuilder()
			.SetBasePath(AppContext.BaseDirectory)
			.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
			.Build();

		var services = new ServiceCollection();
		services.AddDbContextFactory<AppDbContext>(options =>
		{
			options.UseLazyLoadingProxies();
			options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), sql =>
			{
				sql.EnableRetryOnFailure();
				sql.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
			});
			options.EnableSensitiveDataLogging();
		});

		await using var serviceProvider = services.BuildServiceProvider();
		var contextFactory = serviceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();

		await using (var context = await contextFactory.CreateDbContextAsync())
		{
			await context.Database.MigrateAsync();
		}

		await RunLab4InsertAsync(contextFactory);
		await RunLab5RetrievalAsync(contextFactory);
		await RunLab6UpdateDeleteAsync(contextFactory);
		await RunLab7LinqAsync(contextFactory);
		await RunLab10LoadingAsync(contextFactory);
		await RunLab12DtoProjectionAsync(contextFactory);
		await RunLab13TrackingAndCompiledQueryAsync(contextFactory);
		await RunLab14BulkUpdateAsync(contextFactory);
		await RunLab15ConcurrencyAsync(contextFactory);
	}

	private static async Task RunLab4InsertAsync(IDbContextFactory<AppDbContext> contextFactory)
	{
		await using var context = await contextFactory.CreateDbContextAsync();

		if (await context.Products.AnyAsync(product => product.Name == "Noise Cancelling Headphones"))
		{
			Console.WriteLine("Lab 4: sample product already exists.");
			return;
		}

		var product = new Product
		{
			Name = "Noise Cancelling Headphones",
			Price = 299.99m,
			StockQuantity = 12,
			CategoryId = 1
		};

		await context.Products.AddAsync(product);
		await context.SaveChangesAsync();

		Console.WriteLine($"Lab 4: inserted {product.Name} with Id {product.Id}.");
	}

	private static async Task RunLab5RetrievalAsync(IDbContextFactory<AppDbContext> contextFactory)
	{
		await using var context = await contextFactory.CreateDbContextAsync();

		var allProducts = await context.Products
			.Include(product => product.Category)
			.ToListAsync();

		var productById = await context.Products.FindAsync(1);
		var firstExpensiveProduct = await context.Products
			.AsNoTracking()
			.FirstOrDefaultAsync(product => product.Price > 500m);

		Console.WriteLine($"Lab 5: ToListAsync returned {allProducts.Count} products.");
		Console.WriteLine($"Lab 5: FindAsync returned {productById?.Name ?? "no product"}.");
		Console.WriteLine($"Lab 5: FirstOrDefaultAsync returned {firstExpensiveProduct?.Name ?? "no product"}.");
	}

	private static async Task RunLab6UpdateDeleteAsync(IDbContextFactory<AppDbContext> contextFactory)
	{
		await using var context = await contextFactory.CreateDbContextAsync();

		var demoProduct = await context.Products.FirstAsync(product => product.Name == "Noise Cancelling Headphones");
		demoProduct.Price = 279.99m;
		demoProduct.StockQuantity = 10;
		await context.SaveChangesAsync();

		var productToDelete = await context.Products.FirstOrDefaultAsync(product => product.Name == "Noise Cancelling Headphones");
		if (productToDelete is not null)
		{
			context.Products.Remove(productToDelete);
			await context.SaveChangesAsync();
		}

		Console.WriteLine("Lab 6: updated and deleted the sample product.");
	}

	private static async Task RunLab7LinqAsync(IDbContextFactory<AppDbContext> contextFactory)
	{
		await using var context = await contextFactory.CreateDbContextAsync();

		var projectedProducts = await context.Products
			.Where(product => product.StockQuantity > 10)
			.OrderByDescending(product => product.Price)
			.Select(product => new
			{
				product.Name,
				product.Price,
				CategoryName = product.Category.Name
			})
			.ToListAsync();

		Console.WriteLine("Lab 7: LINQ projection results:");

		foreach (var product in projectedProducts)
		{
			Console.WriteLine($"- {product.Name} | {product.CategoryName} | {product.Price:C}");
		}
	}

	private static async Task RunLab10LoadingAsync(IDbContextFactory<AppDbContext> contextFactory)
	{
		await using var eagerContext = await contextFactory.CreateDbContextAsync();
		var eagerLoadedProduct = await eagerContext.Products
			.Include(product => product.Category)
			.Include(product => product.ProductDetail)
			.Include(product => product.Tags)
			.FirstAsync(product => product.Id == 1);

		Console.WriteLine($"Lab 10: eager loading found {eagerLoadedProduct.Name} with {eagerLoadedProduct.Tags.Count} tags.");

		await using var explicitContext = await contextFactory.CreateDbContextAsync();
		var explicitProduct = await explicitContext.Products.FirstAsync(product => product.Id == 2);
		await explicitContext.Entry(explicitProduct).Reference(product => product.Category).LoadAsync();
		await explicitContext.Entry(explicitProduct).Reference(product => product.ProductDetail).LoadAsync();
		await explicitContext.Entry(explicitProduct).Collection(product => product.Tags).LoadAsync();

		Console.WriteLine($"Lab 10: explicit loading found {explicitProduct.Name} in {explicitProduct.Category.Name}.");

		await using var lazyContext = await contextFactory.CreateDbContextAsync();
		var lazyProduct = await lazyContext.Products.FirstAsync(product => product.Id == 3);
		var lazyCategoryName = lazyProduct.Category.Name;
		var lazyTagCount = lazyProduct.Tags.Count;

		Console.WriteLine($"Lab 10: lazy loading found {lazyProduct.Name} in {lazyCategoryName} with {lazyTagCount} tags.");
	}

	private static async Task RunLab12DtoProjectionAsync(IDbContextFactory<AppDbContext> contextFactory)
	{
		await using var context = await contextFactory.CreateDbContextAsync();

		var productDtos = await context.Products
			.AsNoTracking()
			.Select(product => new ProductDTO
			{
				Name = product.Name,
				CategoryName = product.Category.Name
			})
			.ToListAsync();

		Console.WriteLine("Lab 12: DTO projection results:");

		foreach (var dto in productDtos)
		{
			Console.WriteLine($"- {dto.Name} | {dto.CategoryName}");
		}
	}

	private static async Task RunLab13TrackingAndCompiledQueryAsync(IDbContextFactory<AppDbContext> contextFactory)
	{
		await using var context = await contextFactory.CreateDbContextAsync();

		var noTrackingProducts = await context.Products
			.AsNoTracking()
			.Where(product => product.StockQuantity > 0)
			.ToListAsync();

		Console.WriteLine($"Lab 13: AsNoTracking returned {noTrackingProducts.Count} products.");

		var compiledQueryResults = CompiledProductsByMinimumPriceQuery(context, 200m).ToList();

		Console.WriteLine($"Lab 13: compiled query returned {compiledQueryResults.Count} products.");
	}

	private static async Task RunLab14BulkUpdateAsync(IDbContextFactory<AppDbContext> contextFactory)
	{
		await using var context = await contextFactory.CreateDbContextAsync();

		var productsToUpdate = await context.Products
			.AsNoTracking()
			.Where(product => product.CategoryId == 1)
			.ToListAsync();

		foreach (var product in productsToUpdate)
		{
			product.StockQuantity += 5;
		}

		await context.BulkUpdateAsync(productsToUpdate);

		Console.WriteLine($"Lab 14: bulk updated {productsToUpdate.Count} products.");
	}

	private static async Task RunLab15ConcurrencyAsync(IDbContextFactory<AppDbContext> contextFactory)
	{
		await using var firstContext = await contextFactory.CreateDbContextAsync();
		await using var secondContext = await contextFactory.CreateDbContextAsync();

		var firstProduct = await firstContext.Products.FirstAsync(product => product.Id == 1);
		var secondProduct = await secondContext.Products.FirstAsync(product => product.Id == 1);

		firstProduct.Price += 10m;
		await firstContext.SaveChangesAsync();

		secondProduct.Price += 15m;

		try
		{
			await secondContext.SaveChangesAsync();
			Console.WriteLine("Lab 15: concurrency conflict did not occur.");
		}
		catch (DbUpdateConcurrencyException)
		{
			Console.WriteLine("Lab 15: concurrency conflict handled successfully.");
		}
	}
}
