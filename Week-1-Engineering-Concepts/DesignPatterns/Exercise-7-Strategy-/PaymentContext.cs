namespace StrategyPatternExample
{
    public class PaymentContext
    {
        private IPaymentStrategy paymentStrategy;

        public void SetPaymentStrategy(IPaymentStrategy paymentStrategy)
        {
            this.paymentStrategy = paymentStrategy;
        }

        public void ProcessPayment(decimal amount)
        {
            paymentStrategy.Pay(amount);
        }
    }
}
