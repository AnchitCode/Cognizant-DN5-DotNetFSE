using System;

namespace DecoratorPatternExample
{
    class Program
    {
        static void Main(string[] args)
        {
            INotifier notifier = new SMSNotifierDecorator(
                                    new EmailNotifier());

            notifier.Send("Your order has been shipped!");
        }
    }
}
