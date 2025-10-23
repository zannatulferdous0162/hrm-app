using HrmApp.Application.Features.Commands.Employees.UpdateEmployee;
using HrmApp.Domain;
using HrmApp.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace HrmApp.Application.Features.Handlers;

public record UpdateEmployeeCommandHandler(IHanaHrmContext context) : IRequestHandler<UpdateEmployeeCommand, int>
{
    public async Task<int> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await context.Employees

            .FirstOrDefaultAsync(e => e.IdClient == request.IdClient && e.Id == request.Id, cancellationToken);

        if (employee == null)
            return 0;

        employee.EmployeeName = request.EmployeeName;
        employee.EmployeeNameBangla = request.EmployeeNameBangla;
        employee.FatherName = request.FatherName;
        employee.MotherName = request.MotherName;
        employee.BirthDate = request.BirthDate;
        employee.JoiningDate = request.JoiningDate;
        employee.IdDepartment = request.IdDepartment;
        employee.IdSection = request.IdSection;
        employee.IdDesignation = request.IdDesignation;
        employee.Address = request.Address;
        employee.PresentAddress = request.PresentAddress;
        employee.IdGender = request.IdGender;
        employee.IdReligion = request.IdReligion;
        employee.IdReportingManager = request.IdReportingManager;
        employee.IdJobType = request.IdJobType;
        employee.IdEmployeeType = request.IdEmployeeType;
        employee.IdWeekOff = request.IdWeekOff;
        employee.IdMaritalStatus = request.IdMaritalStatus;
        employee.ContactNo = request.ContactNo;
        employee.NationalIdentificationNumber = request.NationalIdentificationNumber;
        employee.HasAttendenceBonus = request.HasAttendenceBonus ?? employee.HasAttendenceBonus;
        employee.HasOvertime = request.HasOvertime ?? employee.HasOvertime;
        employee.IsActive = request.IsActive ?? employee.IsActive;
        employee.EmployeeImage = request.EmployeeImage;
        employee.SetDate = DateTime.Now;

        employee.EmployeeFamilyInfos = request.EmployeeFamilyInfos
            .Select(efi => new EmployeeFamilyInfo
            {
                IdClient = request.IdClient,
                Name = efi.Name,
                IdGender = efi.IdGender,
                IdRelationship = efi.IdRelationship,
                DateOfBirth = efi.DateOfBirth,
                ContactNo = efi.ContactNo,
                CurrentAddress = efi.CurrentAddress,
                PermanentAddress = efi.PermanentAddress,
                SetDate = DateTime.Now
            }).ToList();

        employee.EmployeeProfessionalCertifications = request.EmployeeProfessionalCertifications
            .Select(eps => new EmployeeProfessionalCertification
            {
                IdClient = request.IdClient,
                CertificationTitle = eps.CertificationTitle ?? string.Empty,
                CertificationInstitute = eps.CertificationInstitute ?? string.Empty,
                InstituteLocation = eps.InstituteLocation ?? string.Empty,
                FromDate = eps.FromDate,
                ToDate = eps.ToDate,
                SetDate = DateTime.Now
            }).ToList();

        employee.EmployeeEducationInfos = request.EmployeeEducationInfos
            .Select(eei => new EmployeeEducationInfo
            {
                IdClient = request.IdClient,
                InstituteName = eei.InstituteName,
                IdEducationLevel = eei.IdEducationLevel,
                IdEducationExamination = eei.IdEducationExamination,
                IdEducationResult = eei.IdEducationResult,
                Cgpa = eei.Cgpa,
                ExamScale = eei.ExamScale,
                Marks = eei.Marks,
                Major = eei.Major,
                PassingYear = eei.PassingYear,
                IsForeignInstitute = eei.IsForeignInstitute,
                Duration = eei.Duration,
                Achievement = eei.Achievement,
                SetDate = DateTime.Now
            }).ToList();

        employee.EmployeeDocuments = request.EmployeeDocuments
            .Select(ed => new EmployeeDocument
            {
                IdClient = request.IdClient,
                DocumentName = ed.DocumentName,
                FileName = ed.FileName,
                UploadedFileExtention = ed.UploadedFileExtention,
                UploadedFile = ed.UploadedFile,
                UploadDate = DateTime.Now,
                SetDate = DateTime.Now
            }).ToList();

        context.Employees.Update(employee);

        var result = await context.Instance.SaveChangesAsync(cancellationToken);

        return employee.Id;
    }
}