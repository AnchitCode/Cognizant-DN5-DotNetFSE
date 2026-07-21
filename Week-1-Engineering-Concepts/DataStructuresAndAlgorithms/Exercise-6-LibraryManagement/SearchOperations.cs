namespace LibraryManagementSystem
{
    public class SearchOperations
    {
        // Linear Search
        public static Book LinearSearch(Book[] books, string title)
        {
            foreach (Book book in books)
            {
                if (book.Title.Equals(title,
                    System.StringComparison.OrdinalIgnoreCase))
                {
                    return book;
                }
            }

            return null;
        }

        // Binary Search (Array must be sorted by Title)
        public static Book BinarySearch(Book[] books, string title)
        {
            int left = 0;
            int right = books.Length - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;

                int comparison = string.Compare(
                    books[mid].Title,
                    title,
                    System.StringComparison.OrdinalIgnoreCase);

                if (comparison == 0)
                {
                    return books[mid];
                }
                else if (comparison < 0)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            return null;
        }
    }
}
