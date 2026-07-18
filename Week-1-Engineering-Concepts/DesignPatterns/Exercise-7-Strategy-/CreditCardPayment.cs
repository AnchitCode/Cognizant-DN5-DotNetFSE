using System;

namespace StrategyPatternExample
{
    public class CreditCardPayment : IPaymentStrategy
    {
        public void Pay(decimal amount)
        {
            Console.WriteLine($"Paid ₹{amount} using Credit Card.");
        }
    }
}
