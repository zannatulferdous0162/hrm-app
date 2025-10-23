using HrmApp.Domain.Common;

namespace HrmApp.Domain.Models;

public partial class Gender : BaseEntity
{
    public string? GenderName { get; set; }

    public virtual ICollection<EmployeeFamilyInfo> EmployeeFamilyInfos { get; set; } = new List<EmployeeFamilyInfo>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}