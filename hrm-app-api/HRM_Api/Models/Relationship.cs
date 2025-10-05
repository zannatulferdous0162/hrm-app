using System;
using System.Collections.Generic;

namespace HRM_Api.Models;

public partial class Relationship
{
    public int IdClient { get; set; }

    public int Id { get; set; }

    public string RelationName { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? SetDate { get; set; }

    public string? CreatedBy { get; set; }

    public virtual ICollection<EmployeeFamilyInfo> EmployeeFamilyInfos { get; set; } = new List<EmployeeFamilyInfo>();
}
