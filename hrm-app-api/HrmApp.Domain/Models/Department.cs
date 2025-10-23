using HrmApp.Domain.Common;

namespace HrmApp.Domain.Models;

public partial class Department : BaseEntity
{
    public string DepartName { get; set; } = null!;

    public string? DepartNameBangla { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Section> Sections { get; set; } = new List<Section>();
}