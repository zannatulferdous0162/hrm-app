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

        [HttpGet("departments")]
        public async Task<ActionResult<IEnumerable<object>>> GetDepartments()
        {
            var data = await _context.Departments
                .Select(d => new { d.Id, d.DepartName })
                .ToListAsync();
            return Ok(data);
        }

        [HttpGet("designations")]
        public async Task<ActionResult<IEnumerable<object>>> GetDesignations()
        {
            var data = await _context.Designations
                .Select(d => new { d.Id, d.DesignationName })
                .ToListAsync();
            return Ok(data);
        }

        [HttpGet("genders")]
        public async Task<ActionResult<IEnumerable<object>>> GetGenders()
        {
            var data = await _context.Genders
                .Select(g => new { g.Id, g.GenderName })
                .ToListAsync();
            return Ok(data);
        }

        [HttpGet("religions")]
        public async Task<ActionResult<IEnumerable<object>>> GetReligions()
        {
            var data = await _context.Religions
                .Select(r => new { r.Id, r.ReligionName })
                .ToListAsync();
            return Ok(data);
        }

        
        [HttpGet("employeetypes")]
        public async Task<ActionResult<IEnumerable<object>>> GetEmployeeTypes()
        {
            var data = await _context.EmployeeTypes
                .Select(et => new { et.Id, et.TypeName })
                .ToListAsync();
            return Ok(data);
        }

        [HttpGet("jobtypes")]
        public async Task<ActionResult<IEnumerable<object>>> GetJobTypes()
        {
            var data = await _context.JobTypes
                .Select(jt => new { jt.Id, jt.JobTypeName })
                .ToListAsync();
            return Ok(data);
        }

        [HttpGet("marital-status")]
        public async Task<ActionResult<IEnumerable<object>>> GetMaritalStatus()
        {
            var data = await _context.MaritalStatuses
                .Select(ms => new { ms.Id, ms.MaritalStatusName })
                .ToListAsync();
            return Ok(data);
        }

        [HttpGet("week-off")]
        public async Task<ActionResult<IEnumerable<object>>> GetWeekOff()
        {
            var data = await _context.WeekOffs
                .Select(wo => new { wo.Id, wo.WeekOffDay })
                .ToListAsync();
            return Ok(data);
        }

        [HttpGet("sections")]
        public async Task<ActionResult<IEnumerable<object>>> GetSections()
        {
            var data = await _context.Sections
                .Select(s => new { s.Id, s.SectionName })
                .ToListAsync();
            return Ok(data);
        }
    }
}
