using System;

namespace AdapterPatternExample
{
    public class PayPalGateway
    {
        public void SendPayment(decimal amount)
        {
            Console.WriteLine($"Processing ₹{amount} payment through PayPal.");
        }
    }
}
