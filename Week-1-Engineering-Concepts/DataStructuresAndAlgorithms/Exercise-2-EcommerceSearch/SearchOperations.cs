using System;

namespace EcommerceSearch
{
    public class SearchOperations
    {
        // Linear Search
        public Product LinearSearch(Product[] products, string productName)
        {
            foreach (Product product in products)
            {
                if (product.ProductName.Equals(productName, StringComparison.OrdinalIgnoreCase))
                {
                    return product;
                }
            }

            return null;
        }

        // Binary Search
        public Product BinarySearch(Product[] products, string productName)
        {
            int left = 0;
            int right = products.Length - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;

                int comparison = string.Compare(
                    products[mid].ProductName,
                    productName,
                    StringComparison.OrdinalIgnoreCase);

                if (comparison == 0)
                    return products[mid];

                if (comparison < 0)
                    left = mid + 1;
                else
                    right = mid - 1;
            }

            return null;
        }
    }
}
