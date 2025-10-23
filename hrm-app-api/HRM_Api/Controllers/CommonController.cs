using HrmApp.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRM_Api.Controllers;

[Route("api/common")]
[ApiController]
public class CommonController : ControllerBase
{
    private readonly HanaHrmContext _context;

    public CommonController(HanaHrmContext context)
    {
        _context = context;
    }

    [HttpGet("relationships")]
    public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetRelationships(int idClient)
    {
        var data = await _context.Relationships
            .AsNoTracking()
            .Where(r => r.IdClient == idClient)
            .Select(r => new CommonViewModel
            {
                Id = r.Id,
                Text = r.RelationName ?? string.Empty
            })
            .ToListAsync();

        return Ok(data);
    }

    [HttpGet("educationresults")]
    public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetEducationResults(int idClient)
    {
        var data = await _context.EducationResults
            .AsNoTracking()
            .Where(e => e.IdClient == idClient)
            .Select(e => new CommonViewModel
            {
                Id = e.Id,
                Text = e.ResultName ?? string.Empty
            })
            .ToListAsync();

        return Ok(data);
    }

    [HttpGet("educationexaminations")]
    public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetEducationExaminations(int idClient)
    {
        var data = await _context.EducationExaminations
            .AsNoTracking()
            .Where(e => e.IdClient == idClient)
            .Select(e => new CommonViewModel
            {
                Id = e.Id,
                Text = e.ExamName ?? string.Empty
            })
            .ToListAsync();

        return Ok(data);
    }

    [HttpGet("educationlevels")]
    public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetEducationLevels(int idClient)
    {
        var data = await _context.EducationLevels
            .AsNoTracking()
            .Where(e => e.IdClient == idClient)
            .Select(e => new CommonViewModel
            {
                Id = e.Id,
                Text = e.EducationLevelName ?? string.Empty
            })
            .ToListAsync();

        return Ok(data);
    }

    [HttpGet("departments")]
    public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetDepartments(int idClient)
    {
        var data = await _context.Departments
            .AsNoTracking()
            .Where(i => i.IdClient == idClient)
            .Select(d => new CommonViewModel
            {
                Id = d.Id,
                Text = d.DepartName ?? string.Empty
            })
            .ToListAsync();
        return Ok(data);
    }

    [HttpGet("designations")]
    public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetDesignations(int idClient)
    {
        var data = await _context.Designations
            .AsNoTracking()
            .Where(i => i.IdClient == idClient)
            .Select(d => new CommonViewModel
            {
                Id = d.Id,
                Text = d.DesignationName ?? string.Empty
            })
            .ToListAsync();
        return Ok(data);
    }

    [HttpGet("genders")]
    public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetGenders(int idClient)
    {
        var data = await _context.Genders
            .AsNoTracking()
            .Where(i => i.IdClient == idClient)
            .Select(g => new CommonViewModel
            {
                Id = g.Id,
                Text = g.GenderName ?? string.Empty
            })
            .ToListAsync();
        return Ok(data);
    }

    [HttpGet("religions")]
    public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetReligions(int idClient)
    {
        var data = await _context.Religions
            .AsNoTracking()
            .Where(i => i.IdClient == idClient)
            .Select(r => new CommonViewModel
            {
                Id = r.Id,
                Text = r.ReligionName ?? string.Empty
            })
            .ToListAsync();
        return Ok(data);
    }

    [HttpGet("employeetypes")]
    public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetEmployeeTypes(int idClient)
    {
        var data = await _context.EmployeeTypes
            .AsNoTracking()
            .Where(i => i.IdClient == idClient)
            .Select(et => new CommonViewModel
            {
                Id = et.Id,
                Text = et.TypeName ?? string.Empty
            })
            .ToListAsync();
        return Ok(data);
    }

    [HttpGet("jobtypes")]
    public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetJobTypes(int idClient)
    {
        var data = await _context.JobTypes
            .AsNoTracking()
            .Where(i => i.IdClient == idClient)
            .Select(jt => new CommonViewModel
            {
                Id = jt.Id,
                Text = jt.JobTypeName ?? string.Empty
            })
            .ToListAsync();
        return Ok(data);
    }

    [HttpGet("maritalstatus")]
    public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetMaritalStatus(int idClient)
    {
        var data = await _context.MaritalStatuses
            .AsNoTracking()
            .Where(i => i.IdClient == idClient)
            .Select(ms => new CommonViewModel
            {
                Id = ms.Id,
                Text = ms.MaritalStatusName ?? string.Empty
            })
            .ToListAsync();
        return Ok(data);
    }

    [HttpGet("weekoff")]
    public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetWeekOff(int idClient)
    {
        var data = await _context.WeekOffs
            .AsNoTracking()
            .Where(i => i.IdClient == idClient)
            .Select(wo => new CommonViewModel
            {
                Id = wo.Id,
                Text = wo.WeekOffDay ?? string.Empty
            })
            .ToListAsync();
        return Ok(data);
    }

    [HttpGet("sections")]
    public async Task<ActionResult<IEnumerable<CommonViewModel>>> GetSections(int idClient)
    {
        var data = await _context.Sections
            .AsNoTracking()
            .Where(i => i.IdClient == idClient)
            .Select(s => new CommonViewModel
            {
                Id = s.Id,
                Text = s.SectionName ?? string.Empty
            })
            .ToListAsync();
        return Ok(data);
    }
}