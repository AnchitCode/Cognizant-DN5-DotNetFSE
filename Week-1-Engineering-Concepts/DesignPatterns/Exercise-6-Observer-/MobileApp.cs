using System;

namespace ObserverPatternExample
{
    public class MobileApp : IObserver
    {
        public void Update(string message)
        {
            Console.WriteLine($"Mobile App received notification: {message}");
        }
    }
}
