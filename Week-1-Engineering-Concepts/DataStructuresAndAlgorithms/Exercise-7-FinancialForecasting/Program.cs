using System;

namespace FinancialForecasting
{
    class Program
    {
        static void Main(string[] args)
        {
            double presentValue = 1000;
            double growthRate = 0.10; // 10%
            int years = 3;

            double futureValue = FinancialForecast.CalculateFutureValue(
                presentValue,
                growthRate,
                years);

            Console.WriteLine($"Present Value : ₹{presentValue}");
            Console.WriteLine($"Growth Rate   : {growthRate * 100}%");
            Console.WriteLine($"Years         : {years}");
            Console.WriteLine($"Future Value  : ₹{futureValue:F2}");
        }
    }
}
