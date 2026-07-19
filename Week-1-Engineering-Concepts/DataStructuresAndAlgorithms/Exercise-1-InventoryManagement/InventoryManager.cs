using System;
using System.Collections.Generic;

namespace InventoryManagementSystem
{
    public class InventoryManager
    {
        private Dictionary<int, Product> inventory = new Dictionary<int, Product>();

        // Add Product
        public void AddProduct(Product product)
        {
            if (!inventory.ContainsKey(product.ProductId))
            {
                inventory.Add(product.ProductId, product);
                Console.WriteLine("Product added successfully.");
            }
            else
            {
                Console.WriteLine("Product ID already exists.");
            }
        }

        // Update Product
        public void UpdateProduct(int productId, int quantity, decimal price)
        {
            if (inventory.ContainsKey(productId))
            {
                inventory[productId].Quantity = quantity;
                inventory[productId].Price = price;

                Console.WriteLine("Product updated successfully.");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }

        // Delete Product
        public void DeleteProduct(int productId)
        {
            if (inventory.Remove(productId))
            {
                Console.WriteLine("Product deleted successfully.");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }

        // Display Products
        public void DisplayProducts()
        {
            Console.WriteLine("\nInventory:");

            foreach (Product product in inventory.Values)
            {
                Console.WriteLine(
                    $"ID: {product.ProductId}, " +
                    $"Name: {product.ProductName}, " +
                    $"Quantity: {product.Quantity}, " +
                    $"Price: ₹{product.Price}");
            }
        }
    }
}
