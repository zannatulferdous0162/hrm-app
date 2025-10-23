using HrmApp.Application.Features.Quries.Employees.GetAllEmployees;
using HrmApp.Domain;
using HrmApp.Shared.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HrmApp.Application.Features.Handlers;

public record GetAllEmployeesQueryHandler(IHanaHrmContext context) : IRequestHandler<GetAllEmployeesQuery, List<EmployeeDto>>
{
    public async Task<List<EmployeeDto>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
    {
        var employees = await context.Employees
            .AsNoTracking()
            .Where(e => e.IdClient == request.IdClient)
            .Select(e => new EmployeeDto
            {
                Id = e.Id,
                IdClient = e.IdClient,
                EmployeeName = e.EmployeeName,
                IdDesignation = e.IdDesignation,
            })
            .ToListAsync(cancellationToken);

        return employees;
    }
}