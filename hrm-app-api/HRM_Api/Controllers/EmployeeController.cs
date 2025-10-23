using HrmApp.Application.Features.Commands.Employees.CreateEmployee;
using HrmApp.Application.Features.Commands.Employees.DeleteEmployee;
using HrmApp.Application.Features.Quries.Employees.GetAllEmployees;
using HrmApp.Application.Features.Quries.Employees.GetEmployeeById;
using HrmApp.Shared.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HrmApp.API.Controllers;

[Route("api/employee")]
[ApiController]
public class EmployeeController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<EmployeeDto>>> GetAllEmployees([FromQuery] int idClient, CancellationToken cancellationToken)
    {
        var query = new GetAllEmployeesQuery()
        {
            IdClient = idClient,
        };
        var employees = await mediator.Send(query, cancellationToken);

        if (employees == null || !employees.Any())
        {
            return NotFound(new { message = "No employees found for this client" });
        }

        return Ok(employees);
    }

    [HttpGet("details")]
    public async Task<ActionResult<EmployeeDto>> GetEmployeeById([FromQuery] int idClient, [FromQuery] int id, CancellationToken cancellationToken)
    {
        var query = new GetEmployeeByIdQuery()
        {
            IdClient = idClient,
            Id = id
        };
        var employee = await mediator.Send(query, cancellationToken);

        if (employee == null)
        {
            return NotFound(new { message = "Employee not found" });
        }

        return Ok(employee);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command, CancellationToken cancellationToken)
    {
        if (command == null)
            return BadRequest(new { Message = "Invalid employee data" });

        var result = await mediator.Send(command, cancellationToken);

        if (result == 0)
        {
            return StatusCode(500, new { Message = "Error creating employee" });
        }
        else
        {
            return Ok(new { Message = "Employee created successfully", EmployeeId = result });
        }
    }

    [HttpPut("delete")]
    public async Task<IActionResult> DeleteEmployee([FromQuery] int idClient, [FromQuery] int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0 || idClient <= 0)
            return BadRequest(new { Message = "Invalid employee ID or client ID" });

        var command = new DeleteEmployeeCommand()
        {
            IdClient = idClient,
            Id = id
        };
        var result = await mediator.Send(command, cancellationToken);

        return result switch
        {
            0 => NotFound(new { Message = "Employee not found" }),
            1 => Ok(new { Message = "Employee deleted successfully", EmployeeId = id }),
            _ => StatusCode(500, new { Message = "Error deleting employee" })
        };
    }
}