using HrmApp.Shared.DTOs;
using MediatR;

namespace HrmApp.Application.Features.Quries.Employees.GetEmployeeById;

public record GetEmployeeByIdQuery : IRequest<EmployeeDto>
{
    public int IdClient { get; set; }
    public int Id { get; set; }
}