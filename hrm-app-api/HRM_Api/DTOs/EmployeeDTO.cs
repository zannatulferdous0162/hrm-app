namespace HRM_Api.DTOs
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public int IdClient { get; set; }
        public string? EmployeeName { get; set; }
        public string? EmployeeNameBangla { get; set; }
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? JoiningDate { get; set; }
        public int IdDepartment { get; set; }
        public string? DepartmentName { get; set; }
        public int IdSection { get; set; }
        public string? SectionName { get; set; }
        public int? IdDesignation { get; set; }
        public string? Designation { get; set; }
        public string? Address { get; set; }
        public int? IdGender { get; set; }
        public string? GenderName { get; set; }
        public int? IdReligion { get; set; }
        public string? ReligionName { get; set; }
        public int? IdReportingManager { get; set; }
        public string? ReportingManager { get; set; }
        public int? IdJobType { get; set; }
        public string? JobTypeName { get; set; }
        public int? IdEmployeeType { get; set; }
        public string? TypeName { get; set; }
        public string? PresentAddress { get; set; }
        public string? NationalIdentificationNumber { get; set; }
        public string? ContactNo { get; set; }
        public bool? IsActive { get; set; }
        public bool? HasOvertime { get; set; }
        public bool? HasAttendenceBonus { get; set; }
        public int? IdWeekOff { get; set; }
        public string? WeekOffDay { get; set; }
        public int? IdMaritalStatus { get; set; }
        public string? MaritalStatusName { get; set; }
        public DateTime? SetDate { get; set; }
        public string? CreatedBy { get; set; }

        public IFormFile? ProfileFile { get; set; }
        public string? EmployeeImageBase { get; set; }
        public byte[]? EmployeeImage { get; set; }

        public List<DocumentDto> EmployeeDocuments { get; set; } = [];
        public List<EducationInfoDto> EmployeeEducationInfos { get; set; } = [];
        public List<EmployeefamilyInfoDto> EmployeeFamilyInfos { get; set; } = [];
        public List<EmployeeProfessionalCertificationDto> EmployeeProfessionalCertifications { get; set; } = [];

    }
}
