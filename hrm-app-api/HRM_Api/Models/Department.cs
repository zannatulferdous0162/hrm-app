using System;
using System.Collections.Generic;

namespace HRM_Api.Models;

public partial class Department
{
    public int IdClient { get; set; }

    public int Id { get; set; }

    public string DepartName { get; set; } = null!;

    public string? DepartNameBangla { get; set; }

    public DateTime? SetDate { get; set; }

    public string? CreatedBy { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Section> Sections { get; set; } = new List<Section>();
}
