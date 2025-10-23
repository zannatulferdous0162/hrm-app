using HrmApp.Domain.Common;

namespace HrmApp.Domain.Models;

public partial class Employee : BaseEntity
{
    public string? EmployeeName { get; set; }

    public string? EmployeeNameBangla { get; set; }

    public byte[]? EmployeeImage { get; set; }

    public string? FatherName { get; set; }

    public string? MotherName { get; set; }

    public int? IdReportingManager { get; set; }

    public int? IdJobType { get; set; }

    public int? IdEmployeeType { get; set; }

    public DateTime? BirthDate { get; set; }

    public DateTime? JoiningDate { get; set; }

    public int? IdGender { get; set; }

    public int? IdReligion { get; set; }

    public int IdDepartment { get; set; }

    public int IdSection { get; set; }

    public int? IdDesignation { get; set; }

    public bool? HasOvertime { get; set; }

    public bool? HasAttendenceBonus { get; set; }

    public int? IdWeekOff { get; set; }

    public string? Address { get; set; }

    public string? PresentAddress { get; set; }

    public string? NationalIdentificationNumber { get; set; }

    public string? ContactNo { get; set; }

    public int? IdMaritalStatus { get; set; }

    public bool? IsActive { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual Designation? Designation { get; set; }

    public virtual ICollection<EmployeeDocument> EmployeeDocuments { get; set; } = new List<EmployeeDocument>();

    public virtual ICollection<EmployeeEducationInfo> EmployeeEducationInfos { get; set; } = new List<EmployeeEducationInfo>();

    public virtual ICollection<EmployeeFamilyInfo> EmployeeFamilyInfos { get; set; } = new List<EmployeeFamilyInfo>();

    public virtual ICollection<EmployeeProfessionalCertification> EmployeeProfessionalCertifications { get; set; } = new List<EmployeeProfessionalCertification>();

    public virtual EmployeeType? EmployeeType { get; set; }

    public virtual Gender? Gender { get; set; }

    public virtual JobType? JobType { get; set; }

    public virtual MaritalStatus? MaritalStatus { get; set; }

    public virtual Religion? Religion { get; set; }

    public virtual Section Section { get; set; } = null!;

    public virtual WeekOff? WeekOff { get; set; }
}