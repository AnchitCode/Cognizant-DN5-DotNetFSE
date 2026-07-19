namespace SortingCustomerOrders
{
    public class Order
    {
        public int OrderId { get; set; }

        public string CustomerName { get; set; }

        public decimal TotalPrice { get; set; }

        public Order(int orderId, string customerName, decimal totalPrice)
        {
            OrderId = orderId;
            CustomerName = customerName;
            TotalPrice = totalPrice;
        }
    }
}
