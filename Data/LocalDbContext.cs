using IT_13FinalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace IT_13FinalProject.Data
{
    public class LocalDbContext : DbContext
    {
        public LocalDbContext(DbContextOptions<LocalDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Patient> Patients { get; set; } = null!;
        public DbSet<HealthRecord> HealthRecords { get; set; } = null!;
        public DbSet<VitalSign> VitalSigns { get; set; } = null!;
        public DbSet<PharmacyInventory> PharmacyInventory { get; set; } = null!;
        public DbSet<PatientBill> PatientBills { get; set; } = null!;
        public DbSet<NurseNote> NurseNotes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Role).IsRequired().HasMaxLength(20);
                entity.Property(e => e.FullName).HasMaxLength(200);
                entity.Property(e => e.IsActive).HasDefaultValue(true);

                // Ensure unique username and email
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
            });

            // Configure Patient entity
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.PatientId);
                entity.Property(e => e.PatientId)
                    .ValueGeneratedOnAdd()
                    .UseIdentityColumn();
                
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Phone).HasMaxLength(20);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.Address).HasMaxLength(500);
                entity.Property(e => e.Gender).HasMaxLength(10);
                entity.Property(e => e.CivilStatus).HasMaxLength(20);
                entity.Property(e => e.Occupation).HasMaxLength(100);
                entity.Property(e => e.Nationality).HasMaxLength(50);
                entity.Property(e => e.Religion).HasMaxLength(50);
                entity.Property(e => e.EmergencyContactName).HasMaxLength(100);
                entity.Property(e => e.EmergencyContactRelationship).HasMaxLength(50);
                entity.Property(e => e.EmergencyContactNumber).HasMaxLength(20);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            // Configure PatientBill entity
            modelBuilder.Entity<PatientBill>(entity =>
            {
                entity.HasKey(e => e.BillId);
                // Remove ValueGeneratedOnAdd to allow manual ID setting from cloud database
                
                entity.Property(e => e.PatientName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.PatientEmail).HasMaxLength(100);
                entity.Property(e => e.AssessmentStatus).IsRequired().HasMaxLength(50).HasDefaultValue("Pending");
                entity.Property(e => e.TotalAmount).HasDefaultValue(0);
                entity.Property(e => e.InsuranceCoverage).HasDefaultValue(0);
                entity.Property(e => e.PatientResponsibility).HasDefaultValue(0);
                entity.Property(e => e.PaymentStatus).IsRequired().HasMaxLength(50).HasDefaultValue("Unpaid");
                entity.Property(e => e.PaymentMethod).HasMaxLength(50);
                entity.Property(e => e.InsuranceProvider).HasMaxLength(100);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.IsArchived).HasDefaultValue(false);
            });

            modelBuilder.Entity<NurseNote>(entity =>
            {
                entity.HasKey(e => e.NurseNoteId);
                entity.Property(e => e.NurseNoteId)
                    .ValueGeneratedOnAdd()
                    .UseIdentityColumn();

                entity.Property(e => e.Category).HasMaxLength(100);
                entity.Property(e => e.Details).HasMaxLength(2000);
                entity.Property(e => e.PainScale).HasMaxLength(10);
                entity.Property(e => e.Mood).HasMaxLength(50);
                entity.Property(e => e.Activity).HasMaxLength(50);
                entity.Property(e => e.Nutrition).HasMaxLength(50);
                entity.Property(e => e.NurseName).HasMaxLength(100);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.IsArchived).HasDefaultValue(false);

                entity.HasOne(e => e.Patient)
                    .WithMany()
                    .HasForeignKey(e => e.PatientId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
