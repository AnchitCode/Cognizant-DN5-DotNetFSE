using System;

namespace BuilderPatternExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Computer gamingComputer = new Computer.Builder()
                .SetCPU("Intel Core i9")
                .SetRAM("32 GB")
                .SetStorage("1 TB SSD")
                .Build();

            Computer officeComputer = new Computer.Builder()
                .SetCPU("Intel Core i5")
                .SetRAM("16 GB")
                .SetStorage("512 GB SSD")
                .Build();

            Console.WriteLine("Gaming Computer:");
            gamingComputer.ShowConfiguration();

            Console.WriteLine("Office Computer:");
            officeComputer.ShowConfiguration();
        }
    }
}
