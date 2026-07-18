namespace AdapterPatternExample
{
    public class PayPalAdapter : IPaymentProcessor
    {
        private readonly PayPalGateway _payPalGateway;

        public PayPalAdapter(PayPalGateway payPalGateway)
        {
            _payPalGateway = payPalGateway;
        }

        public void ProcessPayment(decimal amount)
        {
            _payPalGateway.SendPayment(amount);
        }
    }
}
