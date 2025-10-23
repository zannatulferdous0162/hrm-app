using HrmApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HrmApp.Domain;

public interface IHanaHrmContext
{
    DbContext Instance { get; }
    DbSet<Department> Departments { get; set; }
    DbSet<Designation> Designations { get; set; }
    DbSet<EducationExamination> EducationExaminations { get; set; }
    DbSet<EducationLevel> EducationLevels { get; set; }
    DbSet<EducationResult> EducationResults { get; set; }
    DbSet<EmployeeDocument> EmployeeDocuments { get; set; }
    DbSet<EmployeeEducationInfo> EmployeeEducationInfos { get; set; }
    DbSet<EmployeeFamilyInfo> EmployeeFamilyInfos { get; set; }
    DbSet<EmployeeProfessionalCertification> EmployeeProfessionalCertifications { get; set; }
    DbSet<Employee> Employees { get; set; }
    DbSet<EmployeeType> EmployeeTypes { get; set; }
    DbSet<Gender> Genders { get; set; }
    DbSet<JobType> JobTypes { get; set; }
    DbSet<MaritalStatus> MaritalStatuses { get; set; }
    DbSet<Relationship> Relationships { get; set; }
    DbSet<Religion> Religions { get; set; }
    DbSet<Section> Sections { get; set; }
    DbSet<WeekOff> WeekOffs { get; set; }
}