using HrmApp.Domain.Common;

namespace HrmApp.Domain.Models;

public partial class Designation : BaseEntity
{
    public string DesignationName { get; set; } = null!;

    public string? DesignationNameBangla { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}