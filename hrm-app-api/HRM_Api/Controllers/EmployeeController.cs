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
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetAllEmployees([FromQuery] int idClient, CancellationToken cancellationToken)
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

                    EmployeeImageBase = e.EmployeeImage != null ? Convert.ToBase64String(e.EmployeeImage) : null,

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

                    EmployeeDocuments = e.EmployeeDocuments.Select(d => new DocumentDto
                    {
                        Id = d.Id,
                        IdClient = d.IdClient,
                        DocumentName = d.DocumentName,
                        FileName = d.FileName,
                        UploadDate = d.UploadDate,
                        UploadedFileExtention = d.UploadedFileExtention,
                        UploadedFile = d.UploadedFile,
                        SetDate = d.SetDate
                    }).ToList()

                })
                .ToListAsync(cancellationToken);


            if (employees == null || !employees.Any())
            {
                return NotFound(new { message = "No employees found for this client" });
            }

            return Ok(employees);


        }

        [HttpPost("createemployee")]
        public async Task<IActionResult> CreateEmployee([FromForm] EmployeeDTO dto, CancellationToken cancellationToken)
        {
            if (dto == null)
                return BadRequest(new { Message = "Invalid employee data" });

            try
            {


                var form = await Request.ReadFormAsync(cancellationToken);



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
               

                for (int i = 0; i < dto.EmployeeFamilyInfos.Count; i++)
                {
                    var family = dto.EmployeeFamilyInfos[i];
                    Console.WriteLine($"Family {i}: Name='{family.Name}', Gender='{family.IdGender}', Relationship='{family.IdRelationship}'");
                }


                var documentFiles = form.Files
                    .Where(f => f.Name.StartsWith("documentFile_"))
                    .OrderBy(f => f.Name)
                    .ToList();

                foreach (var file in documentFiles)
                {
                    Console.WriteLine($"File: {file.Name}, Size: {file.Length}, ContentType: {file.ContentType}");
                }

                var employeeDocuments = await ProcessDocumentFilesAsync(dto.EmployeeDocuments, documentFiles, cancellationToken);

                byte[]? employeeImageBytes = null;
                var profileFile = form.Files.FirstOrDefault(f => f.Name == "profileImage");
                if (profileFile != null && profileFile.Length > 0)
                {
                    employeeImageBytes = await ConvertFileToByteArrayAsync(profileFile, cancellationToken);
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
                        IdEmployee = e.Id,
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

                Console.WriteLine($"Total Documents to save: {employee.EmployeeDocuments.Count}");
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync(cancellationToken);

                Console.WriteLine("=== EMPLOYEE CREATED SUCCESSFULLY ===");
                return Ok(new { Message = "Employee created successfully", EmployeeId = employee.Id });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"=== ERROR CREATING EMPLOYEE ===");
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Error: {ex.InnerException.Message}");
                    Console.WriteLine($"Inner Stack Trace: {ex.InnerException.StackTrace}");
                }
                return StatusCode(500, new { Message = "Error creating employee", Error = ex.Message });
            }
        }


        private async Task<List<EmployeeDocument>> ProcessDocumentFilesAsync(List<DocumentDto> documentDtos, List<IFormFile> documentFiles, CancellationToken cancellationToken)
        {
            var employeeDocuments = new List<EmployeeDocument>();

            Console.WriteLine($"Processing {documentDtos.Count} document DTOs with {documentFiles.Count} files");
            Console.WriteLine("Available files:");
            foreach (var file in documentFiles)
            {
                Console.WriteLine($"  - {file.Name}");
            }

            for (int i = 0; i < documentDtos.Count; i++)
            {
                var docDto = documentDtos[i];
                byte[]? fileBytes = null;
                string? fileExtension = null;

              
                var matchingFile = documentFiles.FirstOrDefault(f => f.Name == $"documentFile_{i}");

                if (matchingFile == null)
                {
                    
                    matchingFile = i < documentFiles.Count ? documentFiles[i] : null;
                }

                if (matchingFile != null && matchingFile.Length > 0)
                {
                    fileBytes = await ConvertFileToByteArrayAsync(matchingFile, cancellationToken);
                    fileExtension = Path.GetExtension(matchingFile.FileName);

                    Console.WriteLine($"✅ Processing document {i}: {docDto.DocumentName}, File: {matchingFile.FileName}, Size: {matchingFile.Length} bytes");
                }
                else
                {
                    Console.WriteLine($"⚠️ No file found for document {i}: {docDto.DocumentName}");
                    Console.WriteLine($"   Looking for: documentFile_{i}");
                    Console.WriteLine($"   Available files: {string.Join(", ", documentFiles.Select(f => f.Name))}");
                }

                var employeeDocument = new EmployeeDocument
                {
                    IdClient = docDto.IdClient,
                    DocumentName = docDto.DocumentName,
                    FileName = string.IsNullOrEmpty(docDto.FileName)
                        ? Path.GetFileNameWithoutExtension(matchingFile?.FileName ?? $"Document_{i}")
                        : docDto.FileName,
                    UploadDate = docDto.UploadDate != default ? docDto.UploadDate : DateTime.Now,
                    UploadedFileExtention = fileExtension ?? docDto.UploadedFileExtention ?? ".txt",
                    UploadedFile = fileBytes,
                    SetDate = DateTime.Now,
                    CreatedBy = "System"
                };

                employeeDocuments.Add(employeeDocument);
               
            }

         
            return employeeDocuments;
        }

        private async Task<byte[]?> ConvertFileToByteArrayAsync(IFormFile file, CancellationToken cancellationToken)
        {
            if (file == null || file.Length == 0)
                return null;

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream, cancellationToken);
            return memoryStream.ToArray();
        }









        [HttpPut("updateemployee")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromForm] EmployeeDTO dto, CancellationToken cancellationToken)
        {
            if (dto == null || id <= 0)
                return BadRequest(new { Message = "Invalid employee data" });

            try
            {

                var employee = await _context.Employees
                    .Include(e => e.EmployeeEducationInfos)
                    .Include(e => e.EmployeeFamilyInfos)
                    .Include(e => e.EmployeeProfessionalCertifications)
                    .Include(e => e.EmployeeDocuments)
                    .FirstOrDefaultAsync(e => e.Id == id && e.IdClient == dto.IdClient, cancellationToken);

                if (employee == null)
                    return NotFound(new { Message = "Employee not found" });

                var form = await Request.ReadFormAsync(cancellationToken);
                Console.WriteLine($"Form keys received: {string.Join(", ", form.Keys)}");


                dto.EmployeeEducationInfos = DeserializeSafe<List<EducationInfoDto>>(form, "employeeEducationInfos");

                dto.EmployeeFamilyInfos = DeserializeSafe<List<EmployeefamilyInfoDto>>(form, "employeeFamilyInfos");
                dto.EmployeeProfessionalCertifications = DeserializeSafe<List<EmployeeProfessionalCertificationDto>>(form, "employeeProfessionalCertifications");
                dto.EmployeeDocuments = DeserializeSafe<List<DocumentDto>>(form, "employeeDocuments");


                var documentFiles = form.Files
                    .Where(f => f.Name.StartsWith("documentFile_"))
                    .OrderBy(f => f.Name)
                    .ToList();

                var updatedDocuments = await ProcessDocumentFilesAsync(dto.EmployeeDocuments, documentFiles, cancellationToken);


                var profileFile = form.Files.FirstOrDefault(f => f.Name == "profileImage");
                if (profileFile != null && profileFile.Length > 0)
                    employee.EmployeeImage = await ConvertFileToByteArrayAsync(profileFile, cancellationToken);


                employee.EmployeeName = dto.EmployeeName;
                employee.EmployeeNameBangla = dto.EmployeeNameBangla;
                employee.FatherName = dto.FatherName;
                employee.MotherName = dto.MotherName;
                employee.BirthDate = dto.BirthDate;
                employee.JoiningDate = dto.JoiningDate;
                employee.IdDepartment = dto.IdDepartment;
                employee.IdSection = dto.IdSection;
                employee.IdDesignation = dto.IdDesignation;
                employee.Address = dto.Address;
                employee.PresentAddress = dto.PresentAddress;
                employee.IdGender = dto.IdGender;
                employee.IdReligion = dto.IdReligion;
                employee.IdReportingManager = dto.IdReportingManager;
                employee.IdJobType = dto.IdJobType;
                employee.IdEmployeeType = dto.IdEmployeeType;
                employee.IdWeekOff = dto.IdWeekOff;
                employee.IdMaritalStatus = dto.IdMaritalStatus;
                employee.ContactNo = dto.ContactNo;
                employee.NationalIdentificationNumber = dto.NationalIdentificationNumber;
                employee.HasAttendenceBonus = dto.HasAttendenceBonus;
                employee.HasOvertime = dto.HasOvertime;
                employee.IsActive = dto.IsActive ?? true;
                employee.CreatedBy = dto.CreatedBy ?? "System";
                employee.SetDate = DateTime.Now;

                
                using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

                try
                {
                   
                    _context.EmployeeEducationInfos.RemoveRange(employee.EmployeeEducationInfos);
                    _context.EmployeeFamilyInfos.RemoveRange(employee.EmployeeFamilyInfos);
                    _context.EmployeeProfessionalCertifications.RemoveRange(employee.EmployeeProfessionalCertifications);
                    _context.EmployeeDocuments.RemoveRange(employee.EmployeeDocuments);

                   
                    employee.EmployeeEducationInfos = dto.EmployeeEducationInfos.Select(e => new EmployeeEducationInfo
                    {
                        IdClient = e.IdClient,
                        IdEmployee = employee.Id,
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
                    }).ToList();

                    employee.EmployeeFamilyInfos = dto.EmployeeFamilyInfos.Select(f => new EmployeeFamilyInfo
                    {
                        IdClient = f.IdClient,
                        IdEmployee = employee.Id,
                        Name = f.Name,
                        IdGender = f.IdGender,
                        IdRelationship = f.IdRelationship,
                        DateOfBirth = f.DateOfBirth,
                        ContactNo = f.ContactNo,
                        CurrentAddress = f.CurrentAddress,
                        PermanentAddress = f.PermanentAddress,
                        SetDate = DateTime.Now
                    }).ToList();

                    employee.EmployeeProfessionalCertifications = dto.EmployeeProfessionalCertifications.Select(p => new EmployeeProfessionalCertification
                    {
                        IdClient = p.IdClient,
                        IdEmployee = employee.Id,
                        CertificationTitle = p.CertificationTitle,
                        CertificationInstitute = p.CertificationInstitute,
                        InstituteLocation = p.InstituteLocation,
                        FromDate = p.FromDate,
                        ToDate = p.ToDate,
                        SetDate = DateTime.Now
                    }).ToList();

                    employee.EmployeeDocuments = updatedDocuments;
                    foreach (var doc in employee.EmployeeDocuments)
                    {
                        doc.IdEmployee = employee.Id;
                    }

                   
                    await _context.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);

                    return Ok(new { Message = "Employee updated successfully", EmployeeId = employee.Id });
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    Console.WriteLine($"Transaction error: {ex}");
                    return StatusCode(500, new { Message = "Error during transaction", Error = ex.Message });
                }
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"Database update error: {dbEx.InnerException?.Message ?? dbEx.Message}");
                return StatusCode(500, new
                {
                    Message = "Database error while updating employee",
                    Error = dbEx.InnerException?.Message,
                    Details = "Check foreign key constraints and required fields"
                });
            }

        }

        private static T DeserializeSafe<T>(IFormCollection form, string key)
        {
            try
            {
                return form.TryGetValue(key, out var json)
                    ? JsonConvert.DeserializeObject<T>(json) ?? Activator.CreateInstance<T>()
                    : Activator.CreateInstance<T>();
            }
            catch
            {
                return Activator.CreateInstance<T>();
            }
        }



        [HttpDelete("deleteemployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
                return BadRequest(new { Message = "Invalid employee ID" });

            try
            {

                var employee = await _context.Employees
                    .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

                if (employee == null)
                    return NotFound(new { Message = "Employee not found" });


                employee.IsActive = false;
                employee.SetDate = DateTime.Now;

                await _context.SaveChangesAsync(cancellationToken);

                return Ok(new { Message = "Employee deleted successfully", EmployeeId = id });
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"Database delete error: {dbEx.InnerException?.Message ?? dbEx.Message}");
                return StatusCode(500, new
                {
                    Message = "Database error while deleting employee",
                    Error = dbEx.InnerException?.Message
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting employee: {ex}");
                return StatusCode(500, new
                {
                    Message = "Error deleting employee",
                    Error = ex.Message
                });
            }
        }
    }


}