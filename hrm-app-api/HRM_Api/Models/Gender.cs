using System;
using System.Collections.Generic;

namespace HRM_Api.Models;

public partial class Gender
{
    public int IdClient { get; set; }

    public int Id { get; set; }

    public string? GenderName { get; set; }

    public DateTime? SetDate { get; set; }

    public string? CreatedBy { get; set; }

    public virtual ICollection<EmployeeFamilyInfo> EmployeeFamilyInfos { get; set; } = new List<EmployeeFamilyInfo>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
