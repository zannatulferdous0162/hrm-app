using System;
using System.Collections.Generic;

namespace HRM_Api.Models;

public partial class EmployeeFamilyInfo
{
    public int IdClient { get; set; }

    public int Id { get; set; }

    public int IdEmployee { get; set; }

    public string Name { get; set; } = null!;

    public int IdGender { get; set; }

    public int IdRelationship { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? ContactNo { get; set; }

    public string? CurrentAddress { get; set; }

    public string? PermanentAddress { get; set; }

    public DateTime? SetDate { get; set; }

    public string? CreatedBy { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Gender Gender { get; set; } = null!;

    public virtual Relationship Relationship { get; set; } = null!;
}
