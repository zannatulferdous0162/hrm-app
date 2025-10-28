using System;
using System.Collections.Generic;

namespace HRM_Api.Models;

public partial class EducationLevel
{
    public int IdClient { get; set; }

    public int Id { get; set; }

    public string EducationLevelName { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? SetDate { get; set; }

    public string? CreatedBy { get; set; }

    public virtual ICollection<EducationExamination> EducationExaminations { get; set; } = new List<EducationExamination>();

    public virtual ICollection<EmployeeEducationInfo> EmployeeEducationInfos { get; set; } = new List<EmployeeEducationInfo>();
}
