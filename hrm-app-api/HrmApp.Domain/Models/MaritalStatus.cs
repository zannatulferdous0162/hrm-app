using HrmApp.Domain.Common;

namespace HrmApp.Domain.Models;

public partial class MaritalStatus : BaseEntity
{
    public string MaritalStatusName { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}