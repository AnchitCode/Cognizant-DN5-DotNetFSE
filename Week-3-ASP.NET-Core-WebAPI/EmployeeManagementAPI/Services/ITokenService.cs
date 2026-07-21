namespace EmployeeManagementAPI.Services;

public interface ITokenService
{
    string GenerateToken(int userId, string userRole);
}
