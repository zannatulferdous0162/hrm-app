using HRM_Api.DTOs;
using HRM_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRM_Api.Controllers
{
    [Route("api/dropdowns")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly HanaHrmContext _context;
        public CommonController(HanaHrmContext context)
        {
            _context = context;
        }
        [HttpGet("relationships")]
        public async Task<ActionResult<IEnumerable<DropDownDTO>>> GetRelationships(int idClient)
        {
            var data = await _context.Relationships
                .AsNoTracking()
                .Where(r => r.IdClient == idClient)
                .Select(r => new DropDownDTO
                {
                    Value = r.Id,
                    Text = r.RelationName ?? string.Empty
                })
                .ToListAsync();

            return Ok(data);
        }


        [HttpGet("educationresults")]
        public async Task<ActionResult<IEnumerable<DropDownDTO>>> GetEducationResults(int idClient)
        {
            var data = await _context.EducationResults
                .AsNoTracking()
                .Where(e => e.IdClient == idClient)
                .Select(e => new DropDownDTO
                {
                    Value = e.Id,
                    Text = e.ResultName ?? string.Empty
                })
                .ToListAsync();

            return Ok(data);
        }
        [HttpGet("educationexaminations")]
        public async Task<ActionResult<IEnumerable<DropDownDTO>>> GetEducationExaminations(int idClient)
        {
            var data = await _context.EducationExaminations
                .AsNoTracking()
                .Where(e => e.IdClient == idClient)
                .Select(e => new DropDownDTO
                {
                    Value = e.Id,
                    Text = e.ExamName ?? string.Empty
                })
                .ToListAsync();

            return Ok(data);
        }


        [HttpGet("educationlevels")]
        public async Task<ActionResult<IEnumerable<DropDownDTO>>> GetEducationLevels(int idClient)
        {
            var data = await _context.EducationLevels
                .AsNoTracking()
                .Where(e => e.IdClient == idClient)
                .Select(e => new DropDownDTO
                {
                    Value = e.Id,
                    Text = e.EducationLevelName ?? string.Empty
                })
                .ToListAsync();

            return Ok(data);
        }



        [HttpGet("departments")]
        public async Task<ActionResult<IEnumerable<DropDownDTO>>> GetDepartments(int idClient)
        {
            var data = await _context.Departments
                .AsNoTracking()
                .Where(i => i.IdClient == idClient)
                .Select(d => new DropDownDTO
                {
                    Value = d.Id,
                    Text = d.DepartName ?? string.Empty
                })
                .ToListAsync();
            return Ok(data);
        }

        [HttpGet("designations")]
        public async Task<ActionResult<IEnumerable<DropDownDTO>>> GetDesignations(int idClient)
        {
            var data = await _context.Designations
                .AsNoTracking()
                .Where(i => i.IdClient == idClient)
                .Select(d => new DropDownDTO
                {
                    Value = d.Id,
                    Text = d.DesignationName ?? string.Empty
                })
                .ToListAsync();
            return Ok(data);
        }

        [HttpGet("genders")]
        public async Task<ActionResult<IEnumerable<DropDownDTO>>> GetGenders(int idClient)
        {
            var data = await _context.Genders
                .AsNoTracking()
                .Where(i => i.IdClient == idClient)
                .Select(g => new DropDownDTO
                {
                    Value = g.Id,
                    Text = g.GenderName ?? string.Empty
                })
                .ToListAsync();
            return Ok(data);
        }

        [HttpGet("religions")]
        public async Task<ActionResult<IEnumerable<DropDownDTO>>> GetReligions(int idClient)
        {
            var data = await _context.Religions
                .AsNoTracking()
                .Where(i => i.IdClient == idClient)
                .Select(r => new DropDownDTO
                {
                    Value = r.Id,
                    Text = r.ReligionName ?? string.Empty
                })
                .ToListAsync();
            return Ok(data);
        }

        [HttpGet("employeetypes")]
        public async Task<ActionResult<IEnumerable<DropDownDTO>>> GetEmployeeTypes(int idClient)
        {
            var data = await _context.EmployeeTypes
                .AsNoTracking()
                .Where(i => i.IdClient == idClient)
                .Select(et => new DropDownDTO
                {
                    Value = et.Id,
                    Text = et.TypeName ?? string.Empty
                })
                .ToListAsync();
            return Ok(data);
        }

        [HttpGet("jobtypes")]
        public async Task<ActionResult<IEnumerable<DropDownDTO>>> GetJobTypes(int idClient)
        {
            var data = await _context.JobTypes
                .AsNoTracking()
                .Where(i => i.IdClient == idClient)
                .Select(jt => new DropDownDTO
                {
                    Value = jt.Id,
                    Text = jt.JobTypeName ?? string.Empty
                })
                .ToListAsync();
            return Ok(data);
        }

        [HttpGet("maritalstatus")]
        public async Task<ActionResult<IEnumerable<DropDownDTO>>> GetMaritalStatus(int idClient)
        {
            var data = await _context.MaritalStatuses
                .AsNoTracking()
                .Where(i => i.IdClient == idClient)
                .Select(ms => new DropDownDTO
                {
                    Value = ms.Id,
                    Text = ms.MaritalStatusName ?? string.Empty
                })
                .ToListAsync();
            return Ok(data);
        }

        [HttpGet("weekoff")]
        public async Task<ActionResult<IEnumerable<DropDownDTO>>> GetWeekOff(int idClient)
        {
            var data = await _context.WeekOffs
                .AsNoTracking()
                .Where(i => i.IdClient == idClient)
                .Select(wo => new DropDownDTO
                {
                    Value = wo.Id,
                    Text = wo.WeekOffDay ?? string.Empty
                })
                .ToListAsync();
            return Ok(data);
        }

        [HttpGet("sections")]
        public async Task<ActionResult<IEnumerable<DropDownDTO>>> GetSections(int idClient)
        {
            var data = await _context.Sections
                .AsNoTracking()
                .Where(i => i.IdClient == idClient)
                .Select(s => new DropDownDTO
                {
                    Value = s.Id,
                    Text = s.SectionName ?? string.Empty
                })
                .ToListAsync();
            return Ok(data);
        }
    }
}