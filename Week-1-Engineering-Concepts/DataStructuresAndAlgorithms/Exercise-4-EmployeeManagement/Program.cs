using System;

namespace EmployeeManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            EmployeeManager manager = new EmployeeManager();

            // Add Employees
            manager.AddEmployee(new Employee(101, "Rahul", "Developer", 60000));
            manager.AddEmployee(new Employee(102, "Priya", "Tester", 50000));
            manager.AddEmployee(new Employee(103, "Aman", "Manager", 90000));

            // Traverse Employees
            manager.TraverseEmployees();

            // Search Employee
            Console.WriteLine("\nSearching for Employee ID 102...");

            Employee employee = manager.SearchEmployee(102);

            if (employee != null)
            {
                Console.WriteLine(
                    $"Found: {employee.Name}, " +
                    $"{employee.Position}, " +
                    $"Salary: ₹{employee.Salary}");
            }
            else
            {
                Console.WriteLine("Employee not found.");
            }

            // Delete Employee
            Console.WriteLine("\nDeleting Employee ID 102...");
            manager.DeleteEmployee(102);

            // Traverse Again
            manager.TraverseEmployees();
        }
    }
}
