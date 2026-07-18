using System;

namespace ObserverPatternExample
{
    public class WebApp : IObserver
    {
        public void Update(string message)
        {
            Console.WriteLine($"Web App received notification: {message}");
        }
    }
}
