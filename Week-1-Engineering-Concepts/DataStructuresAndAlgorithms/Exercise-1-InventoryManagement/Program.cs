using System;

namespace InventoryManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            InventoryManager manager = new InventoryManager();

            // Add Products
            manager.AddProduct(new Product(101, "Laptop", 10, 65000));
            manager.AddProduct(new Product(102, "Mouse", 50, 500));
            manager.AddProduct(new Product(103, "Keyboard", 30, 1500));

            // Display Inventory
            manager.DisplayProducts();

            // Update Product
            manager.UpdateProduct(102, 60, 550);

            // Delete Product
            manager.DeleteProduct(103);

            Console.WriteLine("\nAfter Update and Delete:");

            // Display Updated Inventory
            manager.DisplayProducts();
        }
    }
}
