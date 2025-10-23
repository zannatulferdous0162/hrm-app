using HrmApp.Domain.Common;

namespace HrmApp.Domain.Models;

public partial class EducationResult : BaseEntity
{
    public string ResultName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<EmployeeEducationInfo> EmployeeEducationInfos { get; set; } = new List<EmployeeEducationInfo>();
}