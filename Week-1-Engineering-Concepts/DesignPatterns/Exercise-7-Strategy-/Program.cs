using System;

namespace StrategyPatternExample
{
    class Program
    {
        static void Main(string[] args)
        {
            PaymentContext paymentContext = new PaymentContext();

            paymentContext.SetPaymentStrategy(new CreditCardPayment());
            paymentContext.ProcessPayment(1500);

            paymentContext.SetPaymentStrategy(new PayPalPayment());
            paymentContext.ProcessPayment(2500);
        }
    }
}
