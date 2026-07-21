using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EmployeeManagementAPI.Filters;

public class CustomExceptionFilter : IExceptionFilter
{
    private readonly IWebHostEnvironment environment;

    public CustomExceptionFilter(IWebHostEnvironment environment)
    {
        this.environment = environment;
    }

    public void OnException(ExceptionContext context)
    {
        var logDirectory = Path.Combine(environment.ContentRootPath, "Logs");
        Directory.CreateDirectory(logDirectory);

        var logPath = Path.Combine(logDirectory, "exceptions.log");
        var logEntry = $"{DateTime.UtcNow:o} | {context.Exception}\n";
        File.AppendAllText(logPath, logEntry);

        context.Result = new ObjectResult(new { Message = "An unexpected error occurred." })
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
        context.ExceptionHandled = true;
    }
}
