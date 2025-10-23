using MediatR;

namespace HrmApp.Application.Features.Commands.Employees.DeleteEmployee;

public record DeleteEmployeeCommand : IRequest<int>
{
    public int IdClient { get; set; }
    public int Id { get; set; }
}