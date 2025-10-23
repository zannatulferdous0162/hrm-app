using HrmApp.Application.Features.Commands.Employees.CreateEmployee;
using HrmApp.Domain;
using HrmApp.Domain.Models;
using MediatR;

public record CreateEmployeeCommandHandler(IHanaHrmContext context) : IRequestHandler<CreateEmployeeCommand, int>
{
    public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        using var transaction = await context.Instance.Database.BeginTransactionAsync(cancellationToken);

        var employee = new Employee
        {
            IdClient = request.IdClient,
            EmployeeName = request.EmployeeName,
            EmployeeNameBangla = request.EmployeeNameBangla,
            FatherName = request.FatherName,
            MotherName = request.MotherName,
            BirthDate = request.BirthDate,
            JoiningDate = request.JoiningDate,
            IdDepartment = request.IdDepartment,
            IdSection = request.IdSection,
            IdDesignation = request.IdDesignation,
            Address = request.Address,
            PresentAddress = request.PresentAddress,
            IdGender = request.IdGender,
            IdReligion = request.IdReligion,
            IdReportingManager = request.IdReportingManager,
            IdJobType = request.IdJobType,
            IdEmployeeType = request.IdEmployeeType,
            IdWeekOff = request.IdWeekOff,
            IdMaritalStatus = request.IdMaritalStatus,
            ContactNo = request.ContactNo,
            NationalIdentificationNumber = request.NationalIdentificationNumber,
            HasAttendenceBonus = request.HasAttendenceBonus ?? false,
            HasOvertime = request.HasOvertime ?? false,
            IsActive = request.IsActive ?? true,
            CreatedBy = request.CreatedBy ?? "System",
            EmployeeImage = request.EmployeeImage,
            SetDate = DateTime.Now,
            EmployeeFamilyInfos = request.EmployeeFamilyInfos
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
            }).ToList(),
            EmployeeProfessionalCertifications = request.EmployeeProfessionalCertifications
            .Select(eps => new EmployeeProfessionalCertification
            {
                IdClient = request.IdClient,
                CertificationTitle = eps.CertificationTitle ?? string.Empty,
                CertificationInstitute = eps.CertificationInstitute ?? string.Empty,
                InstituteLocation = eps.InstituteLocation ?? string.Empty,
                FromDate = eps.FromDate,
                ToDate = eps.ToDate,
                SetDate = DateTime.Now
            }).ToList(),
            EmployeeEducationInfos = request.EmployeeEducationInfos
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
            }).ToList(),
            EmployeeDocuments = request.EmployeeDocuments
            .Select(ed => new EmployeeDocument
            {
                IdClient = request.IdClient,
                DocumentName = ed.DocumentName,
                FileName = ed.FileName,
                UploadedFileExtention = ed.UploadedFileExtention,
                UploadedFile = ed.UploadedFile,
                UploadDate = DateTime.Now,
                SetDate = DateTime.Now
            }).ToList()
        };

        context.Employees.Add(employee);
        var result = await context.Instance.SaveChangesAsync(cancellationToken);

        return result;
    }
}