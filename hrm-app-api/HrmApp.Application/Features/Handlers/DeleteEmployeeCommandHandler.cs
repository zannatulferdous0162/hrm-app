using HrmApp.Application.Features.Commands.Employees.DeleteEmployee;
using HrmApp.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HrmApp.Application.Features.Handlers;

public record DeleteEmployeeCommandHandler(IHanaHrmContext context) : IRequestHandler<DeleteEmployeeCommand, int>
{
    public async Task<int> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await context
            .Employees
            .FirstOrDefaultAsync(e => e.IdClient == request.IdClient && e.Id == request.Id, cancellationToken);

        if (employee == null)
            return 0;

        employee.IsActive = false;
        employee.SetDate = DateTime.Now;

        var result = await context.Instance.SaveChangesAsync(cancellationToken);
        return result;
    }
}