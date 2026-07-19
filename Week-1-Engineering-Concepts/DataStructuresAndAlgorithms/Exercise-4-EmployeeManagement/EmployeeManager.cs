using System;

namespace EmployeeManagementSystem
{
    public class EmployeeManager
    {
        private Employee[] employees = new Employee[100];
        private int count = 0;

        // Add Employee
        public void AddEmployee(Employee employee)
        {
            if (count < employees.Length)
            {
                employees[count] = employee;
                count++;
                Console.WriteLine("Employee added successfully.");
            }
            else
            {
                Console.WriteLine("Employee array is full.");
            }
        }

        // Search Employee
        public Employee SearchEmployee(int employeeId)
        {
            for (int i = 0; i < count; i++)
            {
                if (employees[i].EmployeeId == employeeId)
                {
                    return employees[i];
                }
            }

            return null;
        }

        // Traverse Employees
        public void TraverseEmployees()
        {
            Console.WriteLine("\nEmployee List:");

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(
                    $"ID: {employees[i].EmployeeId}, " +
                    $"Name: {employees[i].Name}, " +
                    $"Position: {employees[i].Position}, " +
                    $"Salary: ₹{employees[i].Salary}");
            }
        }

        // Delete Employee
        public void DeleteEmployee(int employeeId)
        {
            int index = -1;

            for (int i = 0; i < count; i++)
            {
                if (employees[i].EmployeeId == employeeId)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
            {
                Console.WriteLine("Employee not found.");
                return;
            }

            for (int i = index; i < count - 1; i++)
            {
                employees[i] = employees[i + 1];
            }

            employees[count - 1] = null;
            count--;

            Console.WriteLine("Employee deleted successfully.");
        }
    }
}
