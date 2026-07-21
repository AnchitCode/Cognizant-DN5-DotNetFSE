using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly object gate = new();
    private readonly List<Employee> employees;
    private int nextId;

    public EmployeeRepository()
    {
        employees = CreateSeedEmployees();
        nextId = employees.Count + 1;
    }

    public Task<List<Employee>> GetAllAsync()
    {
        lock (gate)
        {
            return Task.FromResult(employees.Select(CloneEmployee).ToList());
        }
    }

    public Task<Employee?> GetByIdAsync(int id)
    {
        lock (gate)
        {
            var employee = employees.FirstOrDefault(current => current.Id == id);
            return Task.FromResult(employee is null ? null : CloneEmployee(employee));
        }
    }

    public Task<Employee> AddAsync(Employee employee)
    {
        lock (gate)
        {
            var createdEmployee = CloneEmployee(employee);
            createdEmployee.Id = nextId++;
            employees.Add(createdEmployee);
            return Task.FromResult(CloneEmployee(createdEmployee));
        }
    }

    public Task<Employee?> UpdateAsync(int id, Employee employee)
    {
        lock (gate)
        {
            var existingEmployee = employees.FirstOrDefault(current => current.Id == id);
            if (existingEmployee is null)
            {
                return Task.FromResult<Employee?>(null);
            }

            existingEmployee.Name = employee.Name;
            existingEmployee.Salary = employee.Salary;
            existingEmployee.Permanent = employee.Permanent;
            existingEmployee.Department = CloneDepartment(employee.Department);
            existingEmployee.Skills = employee.Skills.Select(CloneSkill).ToList();
            existingEmployee.DateOfBirth = employee.DateOfBirth;

            return Task.FromResult<Employee?>(CloneEmployee(existingEmployee));
        }
    }

    public Task<Employee?> DeleteAsync(int id)
    {
        lock (gate)
        {
            var existingEmployee = employees.FirstOrDefault(current => current.Id == id);
            if (existingEmployee is null)
            {
                return Task.FromResult<Employee?>(null);
            }

            employees.Remove(existingEmployee);
            return Task.FromResult<Employee?>(CloneEmployee(existingEmployee));
        }
    }

    public bool Exists(int id)
    {
        lock (gate)
        {
            return employees.Any(employee => employee.Id == id);
        }
    }

    private static List<Employee> CreateSeedEmployees()
    {
        return new List<Employee>
        {
            new()
            {
                Id = 1,
                Name = "Alice Johnson",
                Salary = 90000,
                Permanent = true,
                Department = new Department { Id = 1, Name = "Engineering" },
                Skills = new List<Skill>
                {
                    new() { Id = 1, Name = "C#" },
                    new() { Id = 2, Name = "SQL Server" }
                },
                DateOfBirth = new DateTime(1990, 4, 18)
            },
            new()
            {
                Id = 2,
                Name = "Michael Reed",
                Salary = 78000,
                Permanent = true,
                Department = new Department { Id = 2, Name = "Operations" },
                Skills = new List<Skill>
                {
                    new() { Id = 3, Name = "Communication" },
                    new() { Id = 4, Name = "Leadership" }
                },
                DateOfBirth = new DateTime(1988, 11, 2)
            },
            new()
            {
                Id = 3,
                Name = "Sophia Martinez",
                Salary = 65000,
                Permanent = false,
                Department = new Department { Id = 3, Name = "Support" },
                Skills = new List<Skill>
                {
                    new() { Id = 5, Name = "Customer Service" },
                    new() { Id = 6, Name = "Troubleshooting" }
                },
                DateOfBirth = new DateTime(1994, 7, 25)
            }
        };
    }

    private static Employee CloneEmployee(Employee employee)
    {
        return new Employee
        {
            Id = employee.Id,
            Name = employee.Name,
            Salary = employee.Salary,
            Permanent = employee.Permanent,
            Department = CloneDepartment(employee.Department),
            Skills = employee.Skills.Select(CloneSkill).ToList(),
            DateOfBirth = employee.DateOfBirth
        };
    }

    private static Department CloneDepartment(Department department)
    {
        return new Department
        {
            Id = department.Id,
            Name = department.Name
        };
    }

    private static Skill CloneSkill(Skill skill)
    {
        return new Skill
        {
            Id = skill.Id,
            Name = skill.Name
        };
    }
}
