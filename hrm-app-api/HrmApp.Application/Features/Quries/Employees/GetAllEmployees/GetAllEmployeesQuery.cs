using HrmApp.Shared.DTOs;
using MediatR;

namespace HrmApp.Application.Features.Quries.Employees.GetAllEmployees;

public record GetAllEmployeesQuery : IRequest<List<EmployeeDto>>
{
    public int IdClient { get; set; }
}