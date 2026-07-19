using System;

namespace SortingCustomerOrders
{
    class Program
    {
        static void Main(string[] args)
        {
            Order[] bubbleOrders =
            {
                new Order(101, "Rahul", 25000),
                new Order(102, "Aman", 8000),
                new Order(103, "Priya", 15000),
                new Order(104, "Neha", 50000),
                new Order(105, "Rohit", 12000)
            };

            Order[] quickOrders =
            {
                new Order(101, "Rahul", 25000),
                new Order(102, "Aman", 8000),
                new Order(103, "Priya", 15000),
                new Order(104, "Neha", 50000),
                new Order(105, "Rohit", 12000)
            };

            SortOperations sorter = new SortOperations();

            Console.WriteLine("Original Orders:");
            sorter.DisplayOrders(bubbleOrders);

            // Bubble Sort
            sorter.BubbleSort(bubbleOrders);

            Console.WriteLine("\nAfter Bubble Sort:");
            sorter.DisplayOrders(bubbleOrders);

            // Quick Sort
            sorter.QuickSort(quickOrders, 0, quickOrders.Length - 1);

            Console.WriteLine("\nAfter Quick Sort:");
            sorter.DisplayOrders(quickOrders);
        }
    }
}
