using System;

namespace ObserverPatternExample
{
    class Program
    {
        static void Main(string[] args)
        {
            NewsAgency newsAgency = new NewsAgency();

            IObserver mobileApp = new MobileApp();
            IObserver webApp = new WebApp();

            newsAgency.RegisterObserver(mobileApp);
            newsAgency.RegisterObserver(webApp);

            newsAgency.NotifyObservers("Breaking News: Observer Pattern implemented successfully!");

            newsAgency.RemoveObserver(webApp);

            Console.WriteLine();

            newsAgency.NotifyObservers("Latest Update: Only Mobile App receives this notification.");
        }
    }
}
