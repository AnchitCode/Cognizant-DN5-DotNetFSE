using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers;

[ApiController]
[Route("api/Emp")]
[Authorize(Roles = "Admin,POC")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        this.employeeService = employeeService;
    }

    [HttpGet("standard")]
    [AllowAnonymous]
    [ActionName("GetStandrad")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<List<Employee>> GetStandrad()
    {
        return Ok(GetStandardEmployeeList());
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<List<Employee>>> GetEmployees()
    {
        var employees = await employeeService.GetAllAsync();
        return Ok(employees);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Employee>> GetById([FromRoute] int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid employee id");
        }

        var employee = await employeeService.GetByIdAsync(id);
        if (employee is null)
        {
            return NotFound();
        }

        return Ok(employee);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Employee>> PostEmployee([FromBody] Employee employee)
    {
        if (employee is null)
        {
            return BadRequest("Invalid employee data");
        }

        var createdEmployee = await employeeService.AddAsync(employee);
        return CreatedAtAction(nameof(GetById), new { id = createdEmployee.Id }, createdEmployee);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Employee>> PutEmployee([FromRoute] int id, [FromBody] Employee employee)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid employee id");
        }

        var updatedEmployee = await employeeService.UpdateAsync(id, employee);
        if (updatedEmployee is null)
        {
            return NotFound();
        }

        return Ok(updatedEmployee);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Employee>> DeleteEmployee([FromRoute] int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid employee id");
        }

        var deletedEmployee = await employeeService.DeleteAsync(id);
        if (deletedEmployee is null)
        {
            return NotFound();
        }

        return Ok(deletedEmployee);
    }

    private static List<Employee> GetStandardEmployeeList()
    {
        return new List<Employee>
        {
            new()
            {
                Id = 1,
                Name = "John Smith",
                Salary = 85000,
                Permanent = true,
                Department = new Department { Id = 1, Name = "Engineering" },
                Skills = new List<Skill>
                {
                    new() { Id = 1, Name = "C#" },
                    new() { Id = 2, Name = "ASP.NET Core" }
                },
                DateOfBirth = new DateTime(1991, 1, 15)
            },
            new()
            {
                Id = 2,
                Name = "Sara Wilson",
                Salary = 72000,
                Permanent = false,
                Department = new Department { Id = 2, Name = "QA" },
                Skills = new List<Skill>
                {
                    new() { Id = 3, Name = "Testing" },
                    new() { Id = 4, Name = "Automation" }
                },
                DateOfBirth = new DateTime(1993, 5, 20)
            },
            new()
            {
                Id = 3,
                Name = "David Brown",
                Salary = 95000,
                Permanent = true,
                Department = new Department { Id = 3, Name = "Architecture" },
                Skills = new List<Skill>
                {
                    new() { Id = 5, Name = "Azure" },
                    new() { Id = 6, Name = "Microservices" }
                },
                DateOfBirth = new DateTime(1987, 9, 8)
            }
        };
    }
}
