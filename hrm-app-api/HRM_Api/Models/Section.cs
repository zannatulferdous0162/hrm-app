using System;
using System.Collections.Generic;

namespace HRM_Api.Models;

public partial class Section
{
    public int IdClient { get; set; }

    public int Id { get; set; }

    public string SectionName { get; set; } = null!;

    public string? SectionNameBangla { get; set; }

    public string ShortName { get; set; } = null!;

    public int? IdDepartment { get; set; }

    public DateTime? SetDate { get; set; }

    public string? CreatedBy { get; set; }

    public virtual Department? Department { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
