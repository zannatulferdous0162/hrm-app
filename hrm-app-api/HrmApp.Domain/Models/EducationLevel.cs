using HrmApp.Domain.Common;

namespace HrmApp.Domain.Models;

public partial class EducationLevel : BaseEntity
{
    public string EducationLevelName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<EducationExamination> EducationExaminations { get; set; } = new List<EducationExamination>();

    public virtual ICollection<EmployeeEducationInfo> EmployeeEducationInfos { get; set; } = new List<EmployeeEducationInfo>();
}