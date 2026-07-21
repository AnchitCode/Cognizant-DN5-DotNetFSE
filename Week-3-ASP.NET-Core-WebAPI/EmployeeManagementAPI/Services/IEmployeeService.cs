using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Services;

public interface IEmployeeService
{
    Task<List<Employee>> GetAllAsync();

    Task<Employee?> GetByIdAsync(int id);

    Task<Employee> AddAsync(Employee employee);

    Task<Employee?> UpdateAsync(int id, Employee employee);

    Task<Employee?> DeleteAsync(int id);
}
