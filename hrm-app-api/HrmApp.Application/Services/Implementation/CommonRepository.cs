using HrmApp.Application.Common.Interfaces;
using HrmApp.Application.Services.Interface;
using HrmApp.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HrmApp.Application.Services.Implementation;

public class CommonRepository(ICommonDbContext context): ICommonRepository
{
    public async Task<List<CommonViewModel>> GetAllDepartment(int idClient)
    {
        return await context.Departments
            .AsNoTracking()
            .Where(d => d.IdClient == idClient)
            .OrderBy(d => d.DepartName)
            .Select(d => new CommonViewModel
            {
                Id = d.Id,
                Text = d.DepartName
            })
            .ToListAsync();
    }

    public async Task<List<CommonViewModel>> GetAllDesignation(int idClient)
    {
        return await context.Designations
            .AsNoTracking()
            .Where(d => d.IdClient == idClient && (d.IsActive == null || d.IsActive == true))
            .OrderBy(d => d.DesignationName)
            .Select(d => new CommonViewModel
            {
                Id = d.Id,
                Text = d.DesignationName
            })
            .ToListAsync();
    }

    public async Task<List<CommonViewModel>> GetAllEducationExamination(int idClient)
    {
        return await context.EducationExaminations
            .AsNoTracking()
            .Where(e => e.IdClient == idClient && (e.Status == null || e.Status == true))
            .OrderBy(e => e.ExamName)
            .Select(e => new CommonViewModel
            {
                Id = e.Id,
                Text = e.ExamName
            })
            .ToListAsync();
    }

    public async Task<List<CommonViewModel>> GetAllEducationLevel(int idClient)
    {
        return await context.EducationLevels
            .AsNoTracking()
            .Where(e => e.IdClient == idClient)
            .OrderBy(e => e.EducationLevelName)
            .Select(e => new CommonViewModel
            {
                Id = e.Id,
                Text = e.EducationLevelName
            })
            .ToListAsync();
    }

    public async Task<List<CommonViewModel>> GetAllEducationResult(int idClient)
    {
        return await context.EducationResults
            .AsNoTracking()
            .Where(e => e.IdClient == idClient)
            .OrderBy(e => e.ResultName)
            .Select(e => new CommonViewModel
            {
                Id = e.Id,
                Text = e.ResultName
            })
            .ToListAsync();
    }

    public async Task<List<CommonViewModel>> GetAllEmployeeType(int idClient)
    {
        return await context.EmployeeTypes
            .AsNoTracking()
            .Where(e => e.IdClient == idClient)
            .OrderBy(e => e.TypeName)
            .Select(e => new CommonViewModel
            {
                Id = e.Id,
                Text = e.TypeName ?? string.Empty
            })
            .ToListAsync();
    }

    public async Task<List<CommonViewModel>> GetAllGender(int idClient)
    {
        return await context.Genders
            .AsNoTracking()
            .Where(g => g.IdClient == idClient)
            .OrderBy(g => g.GenderName)
            .Select(g => new CommonViewModel
            {
                Id = g.Id,
                Text = g.GenderName ?? string.Empty
            })
            .ToListAsync();
    }

    public async Task<List<CommonViewModel>> GetAllJobType(int idClient)
    {
        return await context.JobTypes
            .AsNoTracking()
            .Where(j => j.IdClient == idClient)
            .OrderBy(j => j.JobTypeName)
            .Select(j => new CommonViewModel
            {
                Id = j.Id,
                Text = j.JobTypeName
            })
            .ToListAsync();
    }

    public async Task<List<CommonViewModel>> GetAllMaritalStatus(int idClient)
    {
        return await context.MaritalStatuses
            .AsNoTracking()
            .Where(m => m.IdClient == idClient)
            .OrderBy(m => m.MaritalStatusName)
            .Select(m => new CommonViewModel
            {
                Id = m.Id,
                Text = m.MaritalStatusName
            })
            .ToListAsync();
    }

    public async Task<List<CommonViewModel>> GetAllRelationship(int idClient)
    {
        return await context.Relationships
            .AsNoTracking()
            .Where(r => r.IdClient == idClient)
            .OrderBy(r => r.RelationName)
            .Select(r => new CommonViewModel
            {
                Id = r.Id,
                Text = r.RelationName
            })
            .ToListAsync();
    }

    public async Task<List<CommonViewModel>> GetAllReligion(int idClient)
    {
        return await context.Religions
            .AsNoTracking()
            .Where(r => r.IdClient == idClient)
            .OrderBy(r => r.ReligionName)
            .Select(r => new CommonViewModel
            {
                Id = r.Id,
                Text = r.ReligionName
            })
            .ToListAsync();
    }

    public async Task<List<CommonViewModel>> GetAllSection(int idClient)
    {
        return await context.Sections
            .AsNoTracking()
            .Where(s => s.IdClient == idClient)
            .OrderBy(s => s.SectionName)
            .Select(s => new CommonViewModel
            {
                Id = s.Id,
                Text = s.SectionName
            })
            .ToListAsync();
    }

    public async Task<List<CommonViewModel>> GetAllWeekOff(int idClient)
    {
        return await context.WeekOffs
            .AsNoTracking()
            .Where(w => w.IdClient == idClient)
            .OrderBy(w => w.WeekOffDay)
            .Select(w => new CommonViewModel
            {
                Id = w.Id,
                Text = w.WeekOffDay ?? string.Empty
            })
            .ToListAsync();
    }
}