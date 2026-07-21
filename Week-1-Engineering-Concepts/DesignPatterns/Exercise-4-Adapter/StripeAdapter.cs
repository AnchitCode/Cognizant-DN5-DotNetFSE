namespace AdapterPatternExample
{
    public class StripeAdapter : IPaymentProcessor
    {
        private readonly StripeGateway _stripeGateway;

        public StripeAdapter(StripeGateway stripeGateway)
        {
            _stripeGateway = stripeGateway;
        }

        public void ProcessPayment(decimal amount)
        {
            _stripeGateway.MakePayment(amount);
        }
    }
}
