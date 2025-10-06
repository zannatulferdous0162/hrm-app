namespace HRM_Api.DTOs
{
    public class EmployeeProfessionalCertificationDto
    {
        public int Id { get; set; }
        public int IdClient { get; set; }
        public string? CertificationTitle { get; set; }
        public string? CertificationInstitute { get; set; }
        public string? InstituteLocation { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? SetDate { get; set; }
    }
}
