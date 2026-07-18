using System;

namespace StrategyPatternExample
{
    public class PayPalPayment : IPaymentStrategy
    {
        public void Pay(decimal amount)
        {
            Console.WriteLine($"Paid ₹{amount} using PayPal.");
        }
    }
}
