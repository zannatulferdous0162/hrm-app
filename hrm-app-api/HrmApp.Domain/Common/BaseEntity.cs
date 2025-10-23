namespace HrmApp.Domain.Common;

public class BaseEntity
{
    public int IdClient { get; set; }

    public int Id { get; set; }

    public DateTime? SetDate { get; set; }

    public string? CreatedBy { get; set; }
}