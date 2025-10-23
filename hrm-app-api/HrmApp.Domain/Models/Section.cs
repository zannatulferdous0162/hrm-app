using HrmApp.Domain.Common;

namespace HrmApp.Domain.Models;

public partial class Section : BaseEntity
{
    public string SectionName { get; set; } = null!;

    public string? SectionNameBangla { get; set; }

    public string ShortName { get; set; } = null!;

    public int? IdDepartment { get; set; }

    public virtual Department? Department { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}