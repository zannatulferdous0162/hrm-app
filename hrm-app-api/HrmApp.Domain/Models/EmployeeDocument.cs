using HrmApp.Domain.Common;

namespace HrmApp.Domain.Models;

public partial class EmployeeDocument : BaseEntity
{
    public int IdEmployee { get; set; }

    public string DocumentName { get; set; } = null!;

    public string FileName { get; set; } = null!;

    public DateTime UploadDate { get; set; }

    public string? UploadedFileExtention { get; set; }

    public byte[]? UploadedFile { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}