using HRM_Api.DAL.Interfaces;
using HRM_Api.DTOs;
using HRM_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace HRM_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IGenericRepository<Employee> _repository;


        public EmployeeController(IGenericRepository<Employee> repository)
        {
            _repository = repository;
        }

        
        [HttpGet("GetEmployees")]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetAll()
        {
            var employees = await _repository
                .Query()
                .Include(e => e.Department)
                .Include(e => e.Section)
                .Include(e => e.Designation)
                .Include(e => e.Gender)
                .Include(e => e.Religion)
                .Include(e => e.JobType)
                .Include(e => e.EmployeeType)
                .Include(e => e.MaritalStatus)
                .Include(e => e.WeekOff)
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
                    HasAttendenceBonus = e.HasAttendenceBonus,
                    HasOvertime = e.HasOvertime,
                    IsActive = e.IsActive,
                    SetDate = e.SetDate,
                    CreatedBy = e.CreatedBy,
                    EmployeeImageBase = e.EmployeeImage != null ? Convert.ToBase64String(e.EmployeeImage) : null
                })
                .ToListAsync();

            return Ok(employees);
        }

        // ✅ GET: api/Employee/GetEmployeeById/5
        [HttpGet("GetEmployeeById/{id}")]
        public async Task<ActionResult<EmployeeDTO>> Get(int id)
        {
            var employee = await _repository.Query()
                .Include(e => e.Department)
                .Include(e => e.Section)
                .Include(e => e.Designation)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null) return NotFound();

            var dto = new EmployeeDTO
            {
                Id = employee.Id,
                IdClient = employee.IdClient,
                EmployeeName = employee.EmployeeName,
                FatherName = employee.FatherName,
                DepartmentName = employee.Department.DepartName,
                SectionName = employee.Section.SectionName,
                Designation = employee.Designation?.DesignationName,
                EmployeeImageBase = employee.EmployeeImage != null ? Convert.ToBase64String(employee.EmployeeImage) : null
            };

            return Ok(dto);
        }

        // ✅ POST: api/Employee/InsertEmployee
        [HttpPost("InsertEmployee")]
        public async Task<ActionResult> Post([FromForm] EmployeeDTO dto)
        {
            byte[]? imageBytes = null;

            if (dto.ProfileFile != null && dto.ProfileFile.Length > 0)
            {
                using var ms = new MemoryStream();
                await dto.ProfileFile.CopyToAsync(ms);
                imageBytes = ms.ToArray();
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
                IdJobType = dto.IdJobType,
                IdEmployeeType = dto.IdEmployeeType,
                IdMaritalStatus = dto.IdMaritalStatus,
                IdWeekOff = dto.IdWeekOff,
                NationalIdentificationNumber = dto.NationalIdentificationNumber,
                ContactNo = dto.ContactNo,
                HasOvertime = dto.HasOvertime,
                HasAttendenceBonus = dto.HasAttendenceBonus,
                IsActive = dto.IsActive,
                SetDate = DateTime.Now,
                CreatedBy = dto.CreatedBy,
                EmployeeImage = imageBytes
            };

            await _repository.AddAsync(employee);
            return CreatedAtAction(nameof(Get), new { id = employee.Id }, employee);
        }

     
        [HttpPut("UpdateEmployee/{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] EmployeeDTO dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.EmployeeName = dto.EmployeeName;
            existing.EmployeeNameBangla = dto.EmployeeNameBangla;
            existing.FatherName = dto.FatherName;
            existing.MotherName = dto.MotherName;
            existing.IdDepartment = dto.IdDepartment;
            existing.IdSection = dto.IdSection;
            existing.IdDesignation = dto.IdDesignation;
            existing.ContactNo = dto.ContactNo;
            existing.Address = dto.Address;
            existing.PresentAddress = dto.PresentAddress;
            existing.IdReligion = dto.IdReligion;
            existing.IdGender = dto.IdGender;
            existing.IdJobType = dto.IdJobType;
            existing.IdEmployeeType = dto.IdEmployeeType;
            existing.IsActive = dto.IsActive;

            if (dto.ProfileFile != null && dto.ProfileFile.Length > 0)
            {
                using var ms = new MemoryStream();
                await dto.ProfileFile.CopyToAsync(ms);
                existing.EmployeeImage = ms.ToArray();
            }

            await _repository.UpdateAsync(existing);
            return NoContent();
        }

        
        [HttpDelete("DeleteEmployee/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
