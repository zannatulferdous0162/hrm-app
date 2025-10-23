using Microsoft.AspNetCore.Http;

namespace HrmApp.Shared.DTOs;
public record DocumentDto
{
    public int IdClient { get; set; }
    public int Id { get; set; }
    public string DocumentName { get; set; } = null!;
    public string FileName { get; set; } = null!;
    public DateTime? SetDate { get; set; }
    public DateTime UploadDate { get; set; }
    public string? UploadedFileExtention { get; set; }
    public byte[]? UploadedFile { get; set; }
    public IFormFile? UpFile { get; set; }
    public string? FileBase64 { get; set; }
}