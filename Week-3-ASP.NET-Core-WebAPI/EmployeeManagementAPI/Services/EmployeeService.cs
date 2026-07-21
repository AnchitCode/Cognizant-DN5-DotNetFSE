using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repositories;

namespace EmployeeManagementAPI.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        this.employeeRepository = employeeRepository;
    }

    public Task<List<Employee>> GetAllAsync()
    {
        return employeeRepository.GetAllAsync();
    }

    public Task<Employee?> GetByIdAsync(int id)
    {
        return employeeRepository.GetByIdAsync(id);
    }

    public Task<Employee> AddAsync(Employee employee)
    {
        return employeeRepository.AddAsync(employee);
    }

    public Task<Employee?> UpdateAsync(int id, Employee employee)
    {
        return employeeRepository.UpdateAsync(id, employee);
    }

    public Task<Employee?> DeleteAsync(int id)
    {
        return employeeRepository.DeleteAsync(id);
    }
}
