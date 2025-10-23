using HrmApp.Domain.Common;

namespace HrmApp.Domain.Models;

public partial class Relationship : BaseEntity
{
    public string RelationName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<EmployeeFamilyInfo> EmployeeFamilyInfos { get; set; } = new List<EmployeeFamilyInfo>();
}