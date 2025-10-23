using HrmApp.Domain.Common;

namespace HrmApp.Domain.Models;

public partial class WeekOff : BaseEntity
{
    public string? WeekOffDay { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}