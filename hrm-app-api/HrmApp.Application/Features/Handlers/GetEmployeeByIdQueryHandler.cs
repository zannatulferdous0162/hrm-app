using HrmApp.Application.Features.Quries.Employees.GetEmployeeById;
using HrmApp.Domain;
using HrmApp.Shared.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HrmApp.Application.Features.Handlers;

public record GetEmployeeByIdQueryHandler(IHanaHrmContext context) : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto>
{
    public async Task<EmployeeDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var employee = await context.Employees
            .AsNoTracking()
            .AsSplitQuery()
            .Where(e => e.IdClient == request.IdClient && e.Id == request.Id)
            .Select(e => new EmployeeDto
            {
                IdClient = e.IdClient,
                Id = e.Id,
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
                Designation = e.Designation != null ?
                e.Designation.DesignationName : null,
                Address = e.Address,
                PresentAddress = e.PresentAddress,
                IdGender = e.IdGender,
                GenderName = e.Gender != null ?
                e.Gender.GenderName : null,
                IdReligion = e.IdReligion,
                ReligionName = e.Religion != null ?
                e.Religion.ReligionName : null,
                IdJobType = e.IdJobType,
                JobTypeName = e.JobType != null ?
                e.JobType.JobTypeName : null,
                IdEmployeeType = e.IdEmployeeType,
                TypeName = e.EmployeeType != null ?
                e.EmployeeType.TypeName : null,
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
                    IdClient = e.IdClient,
                    Id = ed.Id,
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
                    IdClient = pc.IdClient,
                    Id = pc.Id,
                    CertificationTitle = pc.CertificationTitle,
                    CertificationInstitute = pc.CertificationInstitute,
                    InstituteLocation = pc.InstituteLocation,
                    FromDate = pc.FromDate,
                    ToDate = pc.ToDate,
                    SetDate = pc.SetDate
                }).ToList(),

                EmployeeFamilyInfos = e.EmployeeFamilyInfos.Select(f => new EmployeefamilyInfoDto
                {
                    IdClient = f.IdClient,
                    Id = f.Id,
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
                    IdClient = d.IdClient,
                    Id = d.Id,
                    DocumentName = d.DocumentName,
                    FileName = d.FileName,
                    UploadDate = d.UploadDate,
                    UploadedFileExtention = d.UploadedFileExtention,
                    UploadedFile = d.UploadedFile,
                    FileBase64 = d.UploadedFile != null ? Convert.ToBase64String(d.UploadedFile) : null,
                    SetDate = d.SetDate,
                }).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        return employee;
    }
}