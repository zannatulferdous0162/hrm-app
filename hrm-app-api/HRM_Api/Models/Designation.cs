using System;
using System.Collections.Generic;

namespace HRM_Api.Models;

public partial class Designation
{
    public int IdClient { get; set; }

    public int Id { get; set; }

    public string DesignationName { get; set; } = null!;

    public string? DesignationNameBangla { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? SetDate { get; set; }

    public string? CreatedBy { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
