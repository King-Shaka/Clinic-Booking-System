using MedicalLifeHealthcare.Areas.Identity.Data;
using MedicalLifeHealthcare.Models;
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
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    
    builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    }
    public DbSet<MedicalLifeHealthcare.Models.Appointments>? Appointments { get; set; }
    public DbSet<MedicalLifeHealthcare.Models.Alert>? Alerts { get; set; }
    public DbSet<MedicalLifeHealthcare.Models.Medical_File>? Medical_File { get; set; }
    public DbSet<MedicalLifeHealthcare.Models.Medical_Records>? Medical_Records { get; set; }
    public DbSet<MedicalLifeHealthcare.Models.Que>? Que { get; set; }
    public DbSet<MedicalLifeHealthcare.Models.Counselling_Sessions>? Counselling_Sessions { get; set; }
    public DbSet<MedicalLifeHealthcare.Models.Session_Feedback>? Session_Feedback { get; set; }
    public DbSet<MedicalLifeHealthcare.Models.TestRequest>? TestRequest { get; set; }
    public DbSet<MedicalLifeHealthcare.Models.Samples>? Samples { get; set; }
    public DbSet<MedicalLifeHealthcare.Models.SampleResults>? SampleResults { get; set; }
    public DbSet<MedicalLifeHealthcare.Models.Prescription>? Prescription { get; set; }
    public DbSet<MedicalLifeHealthcare.Models.MedicalRefill>? MedicalRefill { get; set; }
    public DbSet<MedicalLifeHealthcare.Models.Medical_Feedback>? Medical_Feedback { get; set; }
    public DbSet<MedicalLifeHealthcare.Models.IncidentReport>? IncidentReport { get; set; }
    public DbSet<MedicalLifeHealthcare.Models.Case>? Case { get; set; }
    public DbSet<MedicalLifeHealthcare.Models.Referal>? Referal { get; set; }
}

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.FirstName).HasMaxLength(255);
        builder.Property(u => u.LastName).HasMaxLength(255);
    }
}