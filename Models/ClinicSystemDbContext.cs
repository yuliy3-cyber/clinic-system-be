using Microsoft.EntityFrameworkCore;

namespace clinic_system_be.Models;

public partial class ClinicSystemDbContext : DbContext
{
    public ClinicSystemDbContext()
    {
    }

    public ClinicSystemDbContext(DbContextOptions<ClinicSystemDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Prescription> Prescriptions { get; set; }

    public virtual DbSet<PrescriptionDetail> PrescriptionDetails { get; set; }

    public virtual DbSet<User> Users { get; set; }

    string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
        return config["ConnectionStrings:DefaultConnection"];
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(GetConnectionString());
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__Appointm__8ECDFCC2DBBD0B36");

            entity.HasOne(d => d.Doctor).WithMany(p => p.AppointmentDoctors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__Docto__4316F928");

            entity.HasOne(d => d.Patient).WithMany(p => p.AppointmentPatients)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__Patie__4222D4EF");

            entity.HasOne(d => d.Prescription).WithMany(p => p.Appointments).HasConstraintName("FK_Appointment_Prescription");
        });

        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.HasKey(e => e.PrescriptionId).HasName("PK__Prescrip__40130832FB455910");
        });

        modelBuilder.Entity<PrescriptionDetail>(entity =>
        {
            entity.HasKey(e => e.PrescriptionDetailId).HasName("PK__Prescrip__6DB766EA5A67A82A");

            entity.HasOne(d => d.Prescription).WithMany(p => p.PrescriptionDetails).HasConstraintName("FK__Prescript__Presc__48CFD27E");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4C3DEDC4FE");

            entity.Property(e => e.Gender).HasDefaultValue(true);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
