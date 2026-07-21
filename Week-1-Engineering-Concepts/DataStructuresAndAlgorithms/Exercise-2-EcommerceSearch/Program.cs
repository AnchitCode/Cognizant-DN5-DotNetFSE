using System;

namespace EcommerceSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            Product[] products =
            {
                new Product(101, "Camera", "Electronics"),
                new Product(102, "Keyboard", "Accessories"),
                new Product(103, "Laptop", "Electronics"),
                new Product(104, "Mouse", "Accessories"),
                new Product(105, "Printer", "Electronics")
            };

            SearchOperations search = new SearchOperations();

            // Linear Search
            Console.WriteLine("Linear Search:");

            Product linearResult = search.LinearSearch(products, "Mouse");

            if (linearResult != null)
                Console.WriteLine($"Found: {linearResult.ProductName} ({linearResult.Category})");
            else
                Console.WriteLine("Product not found.");

            Console.WriteLine();

            // Binary Search
            Console.WriteLine("Binary Search:");

            Product binaryResult = search.BinarySearch(products, "Mouse");

            if (binaryResult != null)
                Console.WriteLine($"Found: {binaryResult.ProductName} ({binaryResult.Category})");
            else
                Console.WriteLine("Product not found.");
        }
    }
}
