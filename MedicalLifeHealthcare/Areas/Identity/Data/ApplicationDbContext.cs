using MedicalLifeHealthcare.Areas.Identity.Data;
using MedicalLifeHealthcare.Models;
using MedicalLifeHealthcare.Models.CounsellingModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalLifeHealthcare.Areas.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
   
    public DbSet<ChronicMedication> ChronicMedicationTB { get; set; }
    public DbSet<Counselling> CounsellingTB { get; set; }
    public DbSet<GBVReport> GBVReportTB { get; set; }
    public DbSet<Labs> LabTB { get; set; }
    public DbSet<Patient> PatientTB { get; set; }
    public DbSet<Tests> TestTB { get; set; }
    public DbSet<Walkins> WalkinTB { get; set; }
    public DbSet<Appointment> AppointmentTB { get; set; }
    public DbSet<CounsellorPatient> CounsellorPatientTB { get; set; }
    public DbSet<Counselor> CounselorTB { get; set; }
    public DbSet<SessionBooking> SessionBooking { get; set; }
    public DbSet<SessionType> SessionType { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    
    builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    }


    public DbSet<MedicalLifeHealthcare.Models.CounsellingModels.SessionBooking>? SessionBooking_1 { get; set; }


    public DbSet<MedicalLifeHealthcare.Models.CounsellingModels.SessionType>? SessionType_1 { get; set; }
}

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.FirstName).HasMaxLength(255);
        builder.Property(u => u.LastName).HasMaxLength(255);
    }
}