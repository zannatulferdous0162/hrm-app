using System;
using System.Collections.Generic;

namespace HRM_Api.Models;

public partial class EducationExamination
{
    public int IdClient { get; set; }

    public int Id { get; set; }

    public string ExamName { get; set; } = null!;

    public int IdEducationLevel { get; set; }

    public bool? Status { get; set; }

    public DateTime? SetDate { get; set; }

    public string? CreatedBy { get; set; }

    public virtual EducationLevel EducationLevel { get; set; } = null!;

    public virtual ICollection<EmployeeEducationInfo> EmployeeEducationInfos { get; set; } = new List<EmployeeEducationInfo>();
}
