using HRM_Api.DTOs;
using HRM_Api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Sockets;

namespace HRM_Api.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly HanaHrmContext _context;
        public EmployeeController(HanaHrmContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetAllEmployees([FromQuery] int idClient)
        {
            var employees = await _context.Employees
                .AsNoTracking()
                .Where(e => e.IdClient == idClient)
                .Select(e => new EmployeeDTO
                {
                    Id = e.Id,
                    IdClient = e.IdClient,
                    EmployeeName = e.EmployeeName,
                    EmployeeNameBangla = e.EmployeeNameBangla,
                    FatherName = e.FatherName,
                    MotherName = e.MotherName,
                    BirthDate = e.BirthDate,
                    JoiningDate = e.JoiningDate,
                    IdDepartment = e.IdDepartment,
                    DepartmentName = e.Department.DepartName,
                    IdSection = e.IdSection,
                    SectionName = e.Section.SectionName,
                    IdDesignation = e.IdDesignation,
                    Designation = e.Designation != null ? e.Designation.DesignationName : null,
                    Address = e.Address,
                    PresentAddress = e.PresentAddress,
                    IdGender = e.IdGender,
                    GenderName = e.Gender != null ? e.Gender.GenderName : null,
                    IdReligion = e.IdReligion,
                    ReligionName = e.Religion != null ? e.Religion.ReligionName : null,
                    IdJobType = e.IdJobType,
                    JobTypeName = e.JobType != null ? e.JobType.JobTypeName : null,
                    IdEmployeeType = e.IdEmployeeType,
                    TypeName = e.EmployeeType != null ? e.EmployeeType.TypeName : null,
                    IdWeekOff = e.IdWeekOff,
                    WeekOffDay = e.WeekOff != null ? e.WeekOff.WeekOffDay : null,
                    IdMaritalStatus = e.IdMaritalStatus,
                    MaritalStatusName = e.MaritalStatus != null ? e.MaritalStatus.MaritalStatusName : null,
                    ContactNo = e.ContactNo,
                    NationalIdentificationNumber = e.NationalIdentificationNumber,

                    HasOvertime = e.HasOvertime,
                    IsActive = e.IsActive,

                    EmployeeImageBase = e.EmployeeImage != null ? Convert.ToBase64String(e.EmployeeImage) : null ,

                    EmployeeEducationInfos = e.EmployeeEducationInfos.Select(ed => new EducationInfoDto
                    {
                        Id = ed.Id,
                        IdClient = e.IdClient,
                        InstituteName = ed.InstituteName,
                        IdEducationLevel = ed.IdEducationLevel,
                        IdEducationExamination = ed.IdEducationExamination,
                        IdEducationResult = ed.IdEducationResult,
                        Cgpa = ed.Cgpa,
                        ExamScale = ed.ExamScale,
                        PassingYear = ed.PassingYear,
                        Achievement = ed.Achievement
                    }).ToList(),

                    EmployeeProfessionalCertifications = e.EmployeeProfessionalCertifications.Select(pc => new EmployeeProfessionalCertificationDto
                    {
                        Id = pc.Id,
                        IdClient = pc.IdClient,
                        CertificationTitle = pc.CertificationTitle,
                        CertificationInstitute = pc.CertificationInstitute,
                        InstituteLocation = pc.InstituteLocation,
                        FromDate = pc.FromDate,
                        ToDate = pc.ToDate,
                        SetDate = pc.SetDate
                    }).ToList(),

                    EmployeeFamilyInfos = e.EmployeeFamilyInfos.Select(f => new EmployeefamilyInfoDto
                    {
                        Id = f.Id,
                        IdClient = f.IdClient,
                        Name = f.Name,
                        IdGender = f.IdGender,
                        IdRelationship = f.IdRelationship,
                        DateOfBirth = f.DateOfBirth,
                        ContactNo = f.ContactNo,
                        CurrentAddress = f.CurrentAddress,
                        PermanentAddress = f.PermanentAddress,
                        SetDate = f.SetDate
                    }).ToList(),

                    //EmployeeDocuments = e.EmployeeDocuments.Select(d => new DocumentDto
                    //{
                    //    Id = d.Id,
                    //    IdClient = d.IdClient,
                    //    DocumentName = d.DocumentName,
                    //    FileName = d.FileName,
                    //    UploadDate = d.UploadDate,
                    //    UploadedFileExtention = d.UploadedFileExtention,
                    //    UploadedFile = d.UploadedFile,
                    //    SetDate = d.SetDate
                    //}).ToList()

                })
                .ToListAsync();


            if (employees == null || !employees.Any())
            {
                return NotFound(new { message = "No employees found for this client" });
            }

            return Ok(employees);


        }




        [HttpPost("createemployee")]
        public async Task<IActionResult> CreateEmployee([FromForm] EmployeeDTO dto)
        {
            if (dto == null)
                return BadRequest(new { Message = "Invalid employee data" });

            try
            {
                var form = await Request.ReadFormAsync();

            
                dto.EmployeeEducationInfos = form.TryGetValue("employeeEducationInfos", out var eduJson)
                    ? JsonConvert.DeserializeObject<List<EducationInfoDto>>(eduJson) ?? new List<EducationInfoDto>()
                    : new List<EducationInfoDto>();

                dto.EmployeeFamilyInfos = form.TryGetValue("employeeFamilyInfos", out var famJson)
                    ? JsonConvert.DeserializeObject<List<EmployeefamilyInfoDto>>(famJson) ?? new List<EmployeefamilyInfoDto>()
                    : new List<EmployeefamilyInfoDto>();

                dto.EmployeeProfessionalCertifications = form.TryGetValue("employeeProfessionalCertifications", out var certJson)
                    ? JsonConvert.DeserializeObject<List<EmployeeProfessionalCertificationDto>>(certJson) ?? new List<EmployeeProfessionalCertificationDto>()
                    : new List<EmployeeProfessionalCertificationDto>();

                dto.EmployeeDocuments = form.TryGetValue("employeeDocuments", out var docJson)
                    ? JsonConvert.DeserializeObject<List<DocumentDto>>(docJson) ?? new List<DocumentDto>()
                    : new List<DocumentDto>();

              
                var documentFiles = form.Files
                    .Where(f => f.Name.StartsWith("documentFile_"))
                    .OrderBy(f => f.Name)
                    .ToList();

                var employeeDocuments = await ProcessDocumentFilesAsync(dto.EmployeeDocuments, documentFiles);

              
                byte[]? employeeImageBytes = null;
                var profileFile = form.Files.FirstOrDefault(f => f.Name == "profileImage");
                if (profileFile != null && profileFile.Length > 0)
                {
                    employeeImageBytes = await ConvertFileToByteArrayAsync(profileFile);
                }

              
                var employee = new Employee
                {
                    IdClient = dto.IdClient,
                    EmployeeName = dto.EmployeeName,
                    EmployeeNameBangla = dto.EmployeeNameBangla,
                    FatherName = dto.FatherName,
                    MotherName = dto.MotherName,
                    BirthDate = dto.BirthDate,
                    JoiningDate = dto.JoiningDate,
                    IdDepartment = dto.IdDepartment,
                    IdSection = dto.IdSection,
                    IdDesignation = dto.IdDesignation,
                    Address = dto.Address,
                    PresentAddress = dto.PresentAddress,
                    IdGender = dto.IdGender,
                    IdReligion = dto.IdReligion,
                    IdReportingManager = dto.IdReportingManager,
                    IdJobType = dto.IdJobType,
                    IdEmployeeType = dto.IdEmployeeType,
                    IdWeekOff = dto.IdWeekOff,
                    IdMaritalStatus = dto.IdMaritalStatus,
                    ContactNo = dto.ContactNo,
                    NationalIdentificationNumber = dto.NationalIdentificationNumber,
                    HasAttendenceBonus = dto.HasAttendenceBonus,
                    HasOvertime = dto.HasOvertime,
                    IsActive = dto.IsActive ?? true,
                    SetDate = DateTime.Now,
                    CreatedBy = dto.CreatedBy ?? "System",
                    EmployeeImage = employeeImageBytes,

                    EmployeeDocuments = employeeDocuments,

                    EmployeeFamilyInfos = dto.EmployeeFamilyInfos.Select(f => new EmployeeFamilyInfo
                    {
                        IdClient = f.IdClient,
                        Name = f.Name,
                        IdGender = f.IdGender,
                        IdRelationship = f.IdRelationship,
                        DateOfBirth = f.DateOfBirth,
                        ContactNo = f.ContactNo,
                        CurrentAddress = f.CurrentAddress,
                        PermanentAddress = f.PermanentAddress,
                        SetDate = DateTime.Now
                    }).ToList(),

                    EmployeeEducationInfos = dto.EmployeeEducationInfos.Select(e => new EmployeeEducationInfo
                    {
                        IdClient = e.IdClient,
                        IdEducationLevel = e.IdEducationLevel,
                        IdEducationExamination = e.IdEducationExamination,
                        IdEducationResult = e.IdEducationResult,
                        Cgpa = e.Cgpa,
                        ExamScale = e.ExamScale,
                        Marks = e.Marks,
                        Major = e.Major,
                        PassingYear = e.PassingYear,
                        InstituteName = e.InstituteName,
                        IsForeignInstitute = e.IsForeignInstitute,
                        Duration = e.Duration,
                        Achievement = e.Achievement,
                        SetDate = DateTime.Now
                    }).ToList(),

                    EmployeeProfessionalCertifications = dto.EmployeeProfessionalCertifications.Select(p => new EmployeeProfessionalCertification
                    {
                        IdClient = p.IdClient,
                        CertificationTitle = p.CertificationTitle,
                        CertificationInstitute = p.CertificationInstitute,
                        InstituteLocation = p.InstituteLocation,
                        FromDate = p.FromDate,
                        ToDate = p.ToDate,
                        SetDate = DateTime.Now
                    }).ToList()
                };

                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Employee created successfully", EmployeeId = employee.Id });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating employee: {ex}");
                return StatusCode(500, new { Message = "Error creating employee", Error = ex.Message });
            }
        }

        private async Task<List<EmployeeDocument>> ProcessDocumentFilesAsync(List<DocumentDto> documentDtos, List<IFormFile> documentFiles)
        {
            var employeeDocuments = new List<EmployeeDocument>();

            for (int i = 0; i < documentDtos.Count; i++)
            {
                var docDto = documentDtos[i];
                byte[]? fileBytes = null;
                string? fileExtension = null;

                var matchingFile = documentFiles.FirstOrDefault(f => f.Name == $"documentFile_{i}");
                if (matchingFile != null && matchingFile.Length > 0)
                {
                    fileBytes = await ConvertFileToByteArrayAsync(matchingFile);
                    fileExtension = Path.GetExtension(matchingFile.FileName);
                }

                var employeeDocument = new EmployeeDocument
                {
                    IdClient = docDto.IdClient,
                    DocumentName = docDto.DocumentName,
                    FileName = string.IsNullOrEmpty(docDto.FileName)
                        ? Path.GetFileNameWithoutExtension(matchingFile?.FileName ?? "")
                        : docDto.FileName,
                    UploadDate = DateTime.Now,
                    UploadedFileExtention = fileExtension ?? docDto.UploadedFileExtention,
                    UploadedFile = fileBytes,
                    SetDate = DateTime.Now,
                    CreatedBy = "System"
                };

                employeeDocuments.Add(employeeDocument);
            }

            return employeeDocuments;
        }

        private async Task<byte[]?> ConvertFileToByteArrayAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }





        //[HttpPut("updateemployee")]
        //public async Task<IActionResult> UpdateEmployee([FromForm] EmployeeDTO dto)
        //{
        //    var existing = await _repository.GetByIdAsync(id);
        //    if (existing == null) return NotFound();

        //    existing.EmployeeName = dto.EmployeeName;
        //    existing.EmployeeNameBangla = dto.EmployeeNameBangla;
        //    existing.FatherName = dto.FatherName;
        //    existing.MotherName = dto.MotherName;
        //    existing.IdDepartment = dto.IdDepartment;
        //    existing.IdSection = dto.IdSection;
        //    existing.IdDesignation = dto.IdDesignation;
        //    existing.ContactNo = dto.ContactNo;
        //    existing.Address = dto.Address;
        //    existing.PresentAddress = dto.PresentAddress;
        //    existing.IdReligion = dto.IdReligion;
        //    existing.IdGender = dto.IdGender;
        //    existing.IdJobType = dto.IdJobType;
        //    existing.IdEmployeeType = dto.IdEmployeeType;
        //    existing.IsActive = dto.IsActive;

        //    if (dto.ProfileFile != null && dto.ProfileFile.Length > 0)
        //    {
        //        using var ms = new MemoryStream();
        //        await dto.ProfileFile.CopyToAsync(ms);
        //        existing.EmployeeImage = ms.ToArray();
        //    }

        //    await _repository.UpdateAsync(existing);
        //    return NoContent();
        //}


        //[HttpDelete("deleteemployee")]
        //public async Task<IActionResult> DeleteEmployee([FromRoute] int idClient, [FromRoute] int id)
        //{
        //    var existing = await _repository.GetByIdAsync(id);
        //    if (existing == null) return NotFound();

        //    await _repository.DeleteAsync(id);
        //    return NoContent();
        //}
    }
}
