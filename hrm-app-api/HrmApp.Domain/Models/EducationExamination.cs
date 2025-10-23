using HrmApp.Domain.Common;

namespace HrmApp.Domain.Models;

public partial class EducationExamination : BaseEntity
{
    public string ExamName { get; set; } = null!;

    public int IdEducationLevel { get; set; }

    public bool? Status { get; set; }

    public virtual EducationLevel EducationLevel { get; set; } = null!;

    public virtual ICollection<EmployeeEducationInfo> EmployeeEducationInfos { get; set; } = new List<EmployeeEducationInfo>();
}