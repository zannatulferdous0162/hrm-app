﻿using System;
using System.Collections.Generic;

namespace HRM_Api.Models;

public partial class EmployeeProfessionalCertification
{
    public int IdClient { get; set; }

    public int Id { get; set; }

    public int IdEmployee { get; set; }

    public string CertificationTitle { get; set; } = null!;

    public string CertificationInstitute { get; set; } = null!;

    public string InstituteLocation { get; set; } = null!;

    public DateTime FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public DateTime? SetDate { get; set; }

    public string? CreatedBy { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
