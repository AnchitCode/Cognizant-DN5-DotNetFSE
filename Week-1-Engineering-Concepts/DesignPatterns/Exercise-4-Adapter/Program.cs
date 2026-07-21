using System;

namespace AdapterPatternExample
{
    class Program
    {
        static void Main(string[] args)
        {
            IPaymentProcessor payPalProcessor =
                new PayPalAdapter(new PayPalGateway());

            IPaymentProcessor stripeProcessor =
                new StripeAdapter(new StripeGateway());

            payPalProcessor.ProcessPayment(1500);
            stripeProcessor.ProcessPayment(2500);
        }
    }
}
