using Microsoft.EntityFrameworkCore;
using RetailInventory.Models;

namespace RetailInventory.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<Product> Products => Set<Product>();

    public DbSet<ProductDetail> ProductDetails => Set<ProductDetail>();

    public DbSet<Tag> Tags => Set<Tag>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Categories");
            entity.HasKey(category => category.Id);
            entity.Property(category => category.Name).IsRequired().HasMaxLength(100);
            entity.HasIndex(category => category.Name).IsUnique();
            entity.HasData(
                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Home Appliances" });
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Products");
            entity.HasKey(product => product.Id);
            entity.Property(product => product.Name).IsRequired().HasMaxLength(200);
            entity.Property(product => product.Price).HasPrecision(18, 2);
            entity.Property(product => product.StockQuantity).IsRequired();
            entity.Property(product => product.RowVersion).IsRowVersion();
            entity.HasOne(product => product.Category)
                .WithMany(category => category.Products)
                .HasForeignKey(product => product.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasData(
                new Product { Id = 1, Name = "Laptop", Price = 1200.00m, StockQuantity = 15, CategoryId = 1 },
                new Product { Id = 2, Name = "Smartphone", Price = 800.00m, StockQuantity = 30, CategoryId = 1 },
                new Product { Id = 3, Name = "Vacuum Cleaner", Price = 250.00m, StockQuantity = 20, CategoryId = 2 });
        });

        modelBuilder.Entity<ProductDetail>(entity =>
        {
            entity.ToTable("ProductDetails");
            entity.HasKey(productDetail => productDetail.ProductDetailId);
            entity.Property(productDetail => productDetail.WarrantyInfo).IsRequired().HasMaxLength(200);
            entity.HasIndex(productDetail => productDetail.ProductId).IsUnique();
            entity.HasOne(productDetail => productDetail.Product)
                .WithOne(product => product.ProductDetail)
                .HasForeignKey<ProductDetail>(productDetail => productDetail.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasData(
                new ProductDetail { ProductDetailId = 1, WarrantyInfo = "2 years manufacturer warranty", ProductId = 1 },
                new ProductDetail { ProductDetailId = 2, WarrantyInfo = "1 year manufacturer warranty", ProductId = 2 },
                new ProductDetail { ProductDetailId = 3, WarrantyInfo = "18 months service warranty", ProductId = 3 });
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.ToTable("Tags");
            entity.HasKey(tag => tag.Id);
            entity.Property(tag => tag.Name).IsRequired().HasMaxLength(100);
            entity.HasIndex(tag => tag.Name).IsUnique();
            entity.HasData(
                new Tag { Id = 1, Name = "Featured" },
                new Tag { Id = 2, Name = "Popular" },
                new Tag { Id = 3, Name = "Clearance" });
        });

        modelBuilder.Entity<Product>()
            .HasMany(product => product.Tags)
            .WithMany(tag => tag.Products)
            .UsingEntity<Dictionary<string, object>>(
                "ProductTag",
                right => right.HasOne<Tag>()
                    .WithMany()
                    .HasForeignKey("TagId")
                    .OnDelete(DeleteBehavior.Cascade),
                left => left.HasOne<Product>()
                    .WithMany()
                    .HasForeignKey("ProductId")
                    .OnDelete(DeleteBehavior.Cascade),
                join =>
                {
                    join.ToTable("ProductTag");
                    join.HasKey("ProductId", "TagId");
                    join.HasData(
                        new { ProductId = 1, TagId = 1 },
                        new { ProductId = 1, TagId = 2 },
                        new { ProductId = 2, TagId = 2 },
                        new { ProductId = 3, TagId = 3 });
                });

        base.OnModelCreating(modelBuilder);
    }
}
