using HrmApp.Domain.Common;

namespace HrmApp.Domain.Models;

public partial class EmployeeType : BaseEntity
{
    public string? TypeName { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}