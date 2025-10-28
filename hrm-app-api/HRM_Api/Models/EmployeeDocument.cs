using System;
using System.Collections.Generic;

namespace HRM_Api.Models;

public partial class EmployeeDocument
{
    public int IdClient { get; set; }

    public int Id { get; set; }

    public int IdEmployee { get; set; }

    public string DocumentName { get; set; } = null!;

    public string FileName { get; set; } = null!;

    public DateTime UploadDate { get; set; }

    public string? UploadedFileExtention { get; set; }

    public byte[]? UploadedFile { get; set; }

    public DateTime? SetDate { get; set; }

    public string? CreatedBy { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
