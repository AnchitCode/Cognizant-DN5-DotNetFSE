namespace DecoratorPatternExample
{
    public abstract class NotifierDecorator : INotifier
    {
        protected readonly INotifier notifier;

        protected NotifierDecorator(INotifier notifier)
        {
            this.notifier = notifier;
        }

        public virtual void Send(string message)
        {
            notifier.Send(message);
        }
    }
}
