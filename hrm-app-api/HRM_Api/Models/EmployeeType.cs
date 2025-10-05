using System;
using System.Collections.Generic;

namespace HRM_Api.Models;

public partial class EmployeeType
{
    public int IdClient { get; set; }

    public int Id { get; set; }

    public string? TypeName { get; set; }

    public DateTime? SetDate { get; set; }

    public string? CreatedBy { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
