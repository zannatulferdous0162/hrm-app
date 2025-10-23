using HrmApp.Domain.Common;

namespace HrmApp.Domain.Models;

public partial class Religion : BaseEntity
{
    public string ReligionName { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}