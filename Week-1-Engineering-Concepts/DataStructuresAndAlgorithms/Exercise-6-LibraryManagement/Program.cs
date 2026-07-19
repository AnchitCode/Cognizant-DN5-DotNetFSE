using System;

namespace LibraryManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Book[] books =
            {
                new Book(101, "Clean Code", "Robert C. Martin"),
                new Book(102, "Design Patterns", "GoF"),
                new Book(103, "Refactoring", "Martin Fowler"),
                new Book(104, "The Pragmatic Programmer", "Andrew Hunt")
            };

            // Array is already sorted by Title

            Console.WriteLine("Linear Search:");

            Book result1 = SearchOperations.LinearSearch(
                books,
                "Refactoring");

            if (result1 != null)
            {
                Console.WriteLine(
                    $"Found: {result1.Title} by {result1.Author}");
            }
            else
            {
                Console.WriteLine("Book not found.");
            }

            Console.WriteLine("\nBinary Search:");

            Book result2 = SearchOperations.BinarySearch(
                books,
                "Refactoring");

            if (result2 != null)
            {
                Console.WriteLine(
                    $"Found: {result2.Title} by {result2.Author}");
            }
            else
            {
                Console.WriteLine("Book not found.");
            }
        }
    }
}
