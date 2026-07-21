namespace FinancialForecasting
{
    public class FinancialForecast
    {
        // Recursive method to calculate future value
        public static double CalculateFutureValue(
            double presentValue,
            double growthRate,
            int years)
        {
            // Base Case
            if (years == 0)
            {
                return presentValue;
            }

            // Recursive Case
            return CalculateFutureValue(
                presentValue,
                growthRate,
                years - 1) * (1 + growthRate);
        }
    }
}
