using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HRM_Api.Models;

public partial class HanaHrmContext : DbContext
{
    public HanaHrmContext()
    {
    }

    public HanaHrmContext(DbContextOptions<HanaHrmContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Designation> Designations { get; set; }

    public virtual DbSet<EducationExamination> EducationExaminations { get; set; }

    public virtual DbSet<EducationLevel> EducationLevels { get; set; }

    public virtual DbSet<EducationResult> EducationResults { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeDocument> EmployeeDocuments { get; set; }

    public virtual DbSet<EmployeeEducationInfo> EmployeeEducationInfos { get; set; }

    public virtual DbSet<EmployeeFamilyInfo> EmployeeFamilyInfos { get; set; }

    public virtual DbSet<EmployeeProfessionalCertification> EmployeeProfessionalCertifications { get; set; }

    public virtual DbSet<EmployeeType> EmployeeTypes { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<JobType> JobTypes { get; set; }

    public virtual DbSet<MaritalStatus> MaritalStatuses { get; set; }

    public virtual DbSet<Relationship> Relationships { get; set; }

    public virtual DbSet<Religion> Religions { get; set; }

    public virtual DbSet<Section> Sections { get; set; }

    public virtual DbSet<WeekOff> WeekOffs { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

    //    => optionsBuilder.UseSqlServer("Data Source=10.40.10.100;Initial Catalog=HANA-HRM;User ID=Sa;Password=Sa@123456;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => new { e.IdClient, e.Id });

            entity.ToTable("Department");

            entity.Property(e => e.IdClient).HasDefaultValue(10001001);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.DepartName).HasMaxLength(50);
            entity.Property(e => e.DepartNameBangla).HasMaxLength(100);
        });

        modelBuilder.Entity<Designation>(entity =>
        {
            entity.HasKey(e => new { e.IdClient, e.Id });

            entity.ToTable("Designation");

            entity.Property(e => e.IdClient).HasDefaultValue(10001001);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.DesignationName).HasMaxLength(100);
            entity.Property(e => e.DesignationNameBangla).HasMaxLength(100);
        });

        modelBuilder.Entity<EducationExamination>(entity =>
        {
            entity.HasKey(e => new { e.IdClient, e.Id });

            entity.ToTable("EducationExamination");

            entity.Property(e => e.IdClient).HasDefaultValue(10001001);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.ExamName).HasMaxLength(250);

            entity.HasOne(d => d.EducationLevel).WithMany(p => p.EducationExaminations)
                .HasForeignKey(d => new { d.IdClient, d.IdEducationLevel })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EducationExamination_EducationLevel");
        });

        modelBuilder.Entity<EducationLevel>(entity =>
        {
            entity.HasKey(e => new { e.IdClient, e.Id });

            entity.ToTable("EducationLevel");

            entity.Property(e => e.IdClient).HasDefaultValue(10001001);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(250);
            entity.Property(e => e.EducationLevelName).HasMaxLength(50);
        });

        modelBuilder.Entity<EducationResult>(entity =>
        {
            entity.HasKey(e => new { e.IdClient, e.Id });

            entity.ToTable("EducationResult");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(250);
            entity.Property(e => e.ResultName).HasMaxLength(250);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => new { e.IdClient, e.Id });

            entity.ToTable("Employee");

            entity.Property(e => e.IdClient).HasDefaultValue(10001001);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Address).HasMaxLength(250);
            entity.Property(e => e.ContactNo).HasMaxLength(250);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.EmployeeName).HasMaxLength(250);
            entity.Property(e => e.EmployeeNameBangla).HasMaxLength(250);
            entity.Property(e => e.FatherName).HasMaxLength(250);
            entity.Property(e => e.HasAttendenceBonus).HasDefaultValue(false);
            entity.Property(e => e.HasOvertime).HasDefaultValue(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.MotherName).HasMaxLength(250);
            entity.Property(e => e.NationalIdentificationNumber).HasMaxLength(30);
            entity.Property(e => e.PresentAddress).HasMaxLength(250);

            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .HasForeignKey(d => new { d.IdClient, d.IdDepartment })
                .HasConstraintName("FK_Employee_Department");

            entity.HasOne(d => d.Designation).WithMany(p => p.Employees)
                .HasForeignKey(d => new { d.IdClient, d.IdDesignation })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Employee_Designation");

            entity.HasOne(d => d.EmployeeType).WithMany(p => p.Employees)
                .HasForeignKey(d => new { d.IdClient, d.IdEmployeeType })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Employee_EmployeeType");

            entity.HasOne(d => d.Gender).WithMany(p => p.Employees)
                .HasForeignKey(d => new { d.IdClient, d.IdGender })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Employee_Gender");

            entity.HasOne(d => d.JobType).WithMany(p => p.Employees)
                .HasForeignKey(d => new { d.IdClient, d.IdJobType })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Employee_JobType");

            entity.HasOne(d => d.MaritalStatus).WithMany(p => p.Employees)
                .HasForeignKey(d => new { d.IdClient, d.IdMaritalStatus })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Employee_MaritalStatus");

            entity.HasOne(d => d.Religion).WithMany(p => p.Employees)
                .HasForeignKey(d => new { d.IdClient, d.IdReligion })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Employee_Religion");

            entity.HasOne(d => d.Section).WithMany(p => p.Employees)
                .HasForeignKey(d => new { d.IdClient, d.IdSection })
                .HasConstraintName("FK_Employee_Section");

            entity.HasOne(d => d.WeekOff).WithMany(p => p.Employees)
                .HasForeignKey(d => new { d.IdClient, d.IdWeekOff })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Employee_WeekOff");
        });

        modelBuilder.Entity<EmployeeDocument>(entity =>
        {
            entity.HasKey(e => new { e.IdClient, e.Id });

            entity.ToTable("EmployeeDocument");

            entity.HasIndex(e => new { e.IdClient, e.IdEmployee, e.FileName, e.UploadedFileExtention }, "UNQ_EmployeeDocument").IsUnique();

            entity.Property(e => e.IdClient).HasDefaultValue(10001001);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.DocumentName).HasMaxLength(200);
            entity.Property(e => e.FileName).HasMaxLength(100);
            entity.Property(e => e.UploadedFileExtention).HasMaxLength(10);

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeDocuments)
                .HasForeignKey(d => new { d.IdClient, d.IdEmployee })
                .HasConstraintName("FK_EmployeeDocument_Employee");
        });

        modelBuilder.Entity<EmployeeEducationInfo>(entity =>
        {
            entity.HasKey(e => new { e.IdClient, e.Id });

            entity.ToTable("EmployeeEducationInfo");

            entity.HasIndex(e => new { e.IdClient, e.IdEmployee, e.IdEducationLevel, e.IdEducationExamination }, "UNQ_EmployeeEducationInfo").IsUnique();

            entity.Property(e => e.IdClient).HasDefaultValue(10001001);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Cgpa).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.Duration).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.ExamScale).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.InstituteName).HasMaxLength(250);
            entity.Property(e => e.Major).HasMaxLength(50);
            entity.Property(e => e.Marks).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.PassingYear).HasColumnType("numeric(18, 2)");

            entity.HasOne(d => d.EducationExamination).WithMany(p => p.EmployeeEducationInfos)
                .HasForeignKey(d => new { d.IdClient, d.IdEducationExamination })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeEducationInfo_EducationExamination");

            entity.HasOne(d => d.EducationLevel).WithMany(p => p.EmployeeEducationInfos)
                .HasForeignKey(d => new { d.IdClient, d.IdEducationLevel })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeEducationInfo_EducationLevel");

            entity.HasOne(d => d.EducationResult).WithMany(p => p.EmployeeEducationInfos)
                .HasForeignKey(d => new { d.IdClient, d.IdEducationResult })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeEducationInfo_EducationResult");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeEducationInfos)
                .HasForeignKey(d => new { d.IdClient, d.IdEmployee })
                .HasConstraintName("FK_EmployeeEducationInfo_Employee");
        });

        modelBuilder.Entity<EmployeeFamilyInfo>(entity =>
        {
            entity.HasKey(e => new { e.IdClient, e.Id });

            entity.ToTable("EmployeeFamilyInfo");

            entity.HasIndex(e => new { e.IdClient, e.IdEmployee, e.Name }, "UNQ_EmployeeFamilyInfo").IsUnique();

            entity.Property(e => e.IdClient).HasDefaultValue(10001001);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ContactNo).HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CurrentAddress).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PermanentAddress).HasMaxLength(500);

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeFamilyInfos)
                .HasForeignKey(d => new { d.IdClient, d.IdEmployee })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeFamilyInfo_Employee");

            entity.HasOne(d => d.Gender).WithMany(p => p.EmployeeFamilyInfos)
                .HasForeignKey(d => new { d.IdClient, d.IdGender })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeFamilyInfo_Gender");

            entity.HasOne(d => d.Relationship).WithMany(p => p.EmployeeFamilyInfos)
                .HasForeignKey(d => new { d.IdClient, d.IdRelationship })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeFamilyInfo_Relationship");
        });

        modelBuilder.Entity<EmployeeProfessionalCertification>(entity =>
        {
            entity.HasKey(e => new { e.IdClient, e.Id });

            entity.ToTable("EmployeeProfessionalCertification");

            entity.HasIndex(e => new { e.IdClient, e.IdEmployee, e.CertificationTitle, e.FromDate }, "UNQ_EmployeeProfessionalCertification").IsUnique();

            entity.Property(e => e.IdClient).HasDefaultValue(10001001);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.CertificationInstitute).HasMaxLength(250);
            entity.Property(e => e.CertificationTitle).HasMaxLength(255);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.InstituteLocation).HasMaxLength(250);

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeProfessionalCertifications)
                .HasForeignKey(d => new { d.IdClient, d.IdEmployee })
                .HasConstraintName("FK_EmployeeProfessionalCertification_Employee");
        });

        modelBuilder.Entity<EmployeeType>(entity =>
        {
            entity.HasKey(e => new { e.IdClient, e.Id });

            entity.ToTable("EmployeeType");

            entity.Property(e => e.IdClient).HasDefaultValue(10001001);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.SetDate).HasColumnType("datetime");
            entity.Property(e => e.TypeName).HasMaxLength(100);
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => new { e.IdClient, e.Id });

            entity.ToTable("Gender");

            entity.Property(e => e.IdClient).HasDefaultValue(10001001);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.GenderName).HasMaxLength(50);
        });

        modelBuilder.Entity<JobType>(entity =>
        {
            entity.HasKey(e => new { e.IdClient, e.Id });

            entity.ToTable("JobType");

            entity.Property(e => e.IdClient).HasDefaultValue(10001001);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.JobTypeBanglaName).HasMaxLength(50);
            entity.Property(e => e.JobTypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<MaritalStatus>(entity =>
        {
            entity.HasKey(e => new { e.IdClient, e.Id });

            entity.ToTable("MaritalStatus");

            entity.Property(e => e.IdClient).HasDefaultValue(10001001);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.MaritalStatusName).HasMaxLength(50);
        });

        modelBuilder.Entity<Relationship>(entity =>
        {
            entity.HasKey(e => new { e.IdClient, e.Id });

            entity.ToTable("Relationship");

            entity.Property(e => e.IdClient).HasDefaultValue(10001001);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.RelationName).HasMaxLength(50);
        });

        modelBuilder.Entity<Religion>(entity =>
        {
            entity.HasKey(e => new { e.IdClient, e.Id });

            entity.ToTable("Religion");

            entity.Property(e => e.IdClient).HasDefaultValue(10001001);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.ReligionName).HasMaxLength(50);
        });

        modelBuilder.Entity<Section>(entity =>
        {
            entity.HasKey(e => new { e.IdClient, e.Id });

            entity.ToTable("Section");

            entity.HasIndex(e => new { e.IdClient, e.IdDepartment, e.SectionName }, "UNQ_Section").IsUnique();

            entity.Property(e => e.IdClient).HasDefaultValue(10001001);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.SectionName).HasMaxLength(100);
            entity.Property(e => e.SectionNameBangla).HasMaxLength(100);
            entity.Property(e => e.ShortName).HasMaxLength(50);

            entity.HasOne(d => d.Department).WithMany(p => p.Sections)
                .HasForeignKey(d => new { d.IdClient, d.IdDepartment })
                .HasConstraintName("FK_Section_Department");
        });

        modelBuilder.Entity<WeekOff>(entity =>
        {
            entity.HasKey(e => new { e.IdClient, e.Id });

            entity.ToTable("WeekOff");

            entity.HasIndex(e => e.IdClient, "UNQ_WeekOff").IsUnique();

            entity.Property(e => e.IdClient).HasDefaultValue(10001001);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.WeekOffDay).HasMaxLength(3);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
