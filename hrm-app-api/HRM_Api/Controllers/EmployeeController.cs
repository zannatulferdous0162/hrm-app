using HRM_Api.DTOs;
using HRM_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        //[HttpGet("getallemployee")]
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
                  
                    //EmployeeImageBase = e.EmployeeImage != null ? Convert.ToBase64String(e.EmployeeImage) : null ,

                    EmployeeEducationInfos = e.EmployeeEducationInfos.Select(ed => new EducationInfoDto
                    {
                        Id = ed.Id,
                        IdClient = e.IdClient,
                        InstituteName = ed.InstituteName,
                        //IdEducationLevel = ed.IdEducationLevel,
                        IdEducationExamination = ed.IdEducationExamination,
                        IdEducationResult = ed.IdEducationResult,
                        Cgpa = ed.Cgpa,
                        ExamScale = ed.ExamScale,
                        PassingYear = ed.PassingYear,
                        Achievement = ed.Achievement
                    }).ToList(),

                })
                .ToListAsync();


            if (employees == null || !employees.Any())
            {
                return NotFound(new { message = "No employees found for this client" });
            }

            return Ok(employees);


        }



        //[HttpGet("getemployeedetail")]
        //public async Task<ActionResult<EmployeeDTO>> GetEmployeeById([FromQuery] int idClient, [FromQuery] int id)
        //{
        //    try
        //    {
        //        var employees = await _context.Employees
        //            .Where(e => e.IdClient == idClient && e.Id == id)
        //            .Select(e => new EmployeeDTO
        //            {
        //                Id = e.Id,
        //                IdClient = e.IdClient,
        //                EmployeeName = e.EmployeeName,
        //                FatherName = e.FatherName,
        //                DepartmentName = e.Department != null ? e.Department.DepartName : null,
        //                SectionName = e.Section != null ? e.Section.SectionName : null,
        //                Designation = e.Designation != null ? e.Designation.DesignationName : null,
        //                ContactNo = e.ContactNo != null ? e.ContactNo : null,

        //            })
        //            .FirstOrDefaultAsync();

        //        if (employees == null)
        //            return NotFound(new { Message = "Employee not found." });

        //        return Ok(employees);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { Message = "Error while fetching employee detail.", Error = ex.Message });
        //    }
        //}


        [HttpPost("createemployee")]
        public async Task<IActionResult> CreateEmployee([FromForm] EmployeeDTO dto)
        {
            if (dto == null)
                return BadRequest(new { Message = "Invalid employee data" });

            try
            {
                
                //byte[]? imageBytes = null;
                //if (dto.ProfileFile != null && dto.ProfileFile.Length > 0)
                //{
                //    using (var ms = new MemoryStream())
                //    {
                //        await dto.ProfileFile.CopyToAsync(ms);
                //        imageBytes = ms.ToArray();
                //    }
                //}


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
                    //Address = dto.Address,
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
                    //EmployeeImage = imageBytes
                };

               
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Employee created successfully", EmployeeId = employee.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error creating employee", Error = ex.Message });
            }
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
