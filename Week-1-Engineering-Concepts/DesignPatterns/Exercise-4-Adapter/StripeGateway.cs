using System;

namespace AdapterPatternExample
{
    public class StripeGateway
    {
        public void MakePayment(decimal amount)
        {
            Console.WriteLine($"Processing ₹{amount} payment through Stripe.");
        }
    }
}
