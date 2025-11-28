using IT_13FinalProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Text;

namespace IT_13FinalProject.Services
{
    public partial class ApplicationDbContext : DbContext
    {
        // Constructor for dependency injection - NO connection string here!
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Your DbSets - initialize with null-forgiving operator to satisfy the compiler
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Patient> Patients { get; set; } = null!;
        public DbSet<Doctor> Doctors { get; set; } = null!;
        public DbSet<Nurse> Nurses { get; set; } = null!;
        public DbSet<BillingStaff> BillingStaff { get; set; } = null!;
        public DbSet<InventoryStaff> InventoryStaff { get; set; } = null!;
        public DbSet<HealthRecord> HealthRecords { get; set; } = null!;
        public DbSet<Appointment> Appointments { get; set; } = null!;
        public DbSet<Billing> Billing { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;
        public DbSet<Insurance> Insurance { get; set; } = null!;
        public DbSet<Medication> Medications { get; set; } = null!;
        public DbSet<Prescription> Prescriptions { get; set; } = null!;
        public DbSet<VitalSigns> VitalSigns { get; set; } = null!;
        public DbSet<MedicalHistory> MedicalHistory { get; set; } = null!;
        public DbSet<ActivityLog> ActivityLogs { get; set; } = null!;
        public DbSet<Notification> Notifications { get; set; } = null!;
        public DbSet<Inventory> Inventory { get; set; } = null!;
        public DbSet<Department> Departments { get; set; } = null!;
        public DbSet<InventoryTransaction> InventoryTransactions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure User entity to match database column names
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users"); // Explicit table name

                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserId).HasColumnName("user_id"); // Map property to column

                entity.HasIndex(e => e.Username).IsUnique();
                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("username"); // Map property to column

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("password_hash"); // Map property to column

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("role"); // Map property to column

                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("GETDATE()")
                    .HasColumnName("created_at"); // Map property to column

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email"); // Map property to column

                entity.Property(e => e.FullName)
                    .HasMaxLength(200)
                    .HasColumnName("full_name"); // Map property to column

                entity.Property(e => e.IsActive)
                    .HasDefaultValue(true)
                    .HasColumnName("is_active"); // Map property to column
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.PatientId);
                entity.Property(e => e.UserId).HasColumnName("user_id"); // Allow null values
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.DoctorId);
            });

            modelBuilder.Entity<Nurse>(entity =>
            {
                entity.HasKey(e => e.NurseId);
            });

            modelBuilder.Entity<BillingStaff>(entity =>
            {
                entity.HasKey(e => e.BillingStaffId);
            });

            modelBuilder.Entity<InventoryStaff>(entity =>
            {
                entity.HasKey(e => e.InventoryStaffId);
            });

            modelBuilder.Entity<HealthRecord>(entity =>
            {
                entity.HasKey(e => e.RecordId);
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(e => e.AppointmentId);
            });

            modelBuilder.Entity<Billing>(entity =>
            {
                entity.HasKey(e => e.BillId);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.PaymentId);
            });

            modelBuilder.Entity<Insurance>(entity =>
            {
                entity.HasKey(e => e.InsuranceId);
            });

            modelBuilder.Entity<Medication>(entity =>
            {
                entity.HasKey(e => e.MedicationId);
            });

            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.HasKey(e => e.PrescriptionId);
            });

            modelBuilder.Entity<VitalSigns>(entity =>
            {
                entity.HasKey(e => e.VitalSignId);
            });

            modelBuilder.Entity<MedicalHistory>(entity =>
            {
                entity.HasKey(e => e.HistoryId);
            });

            modelBuilder.Entity<ActivityLog>(entity =>
            {
                entity.HasKey(e => e.LogId);
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => e.NotificationId);
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasKey(e => e.InventoryId);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DepartmentId);
            });

            modelBuilder.Entity<InventoryTransaction>(entity =>
            {
                entity.HasKey(e => e.TransactionId);
            });

            // Set explicit table names to match your database
            modelBuilder.Entity<ActivityLog>().ToTable("activity_logs");
            modelBuilder.Entity<Appointment>().ToTable("appointments");
            modelBuilder.Entity<Billing>().ToTable("billing");
            modelBuilder.Entity<BillingStaff>().ToTable("billing_staff");
            modelBuilder.Entity<Department>().ToTable("departments");
            modelBuilder.Entity<Doctor>().ToTable("doctors");
            modelBuilder.Entity<HealthRecord>().ToTable("health_records");
            modelBuilder.Entity<Insurance>().ToTable("insurance");
            modelBuilder.Entity<Inventory>().ToTable("inventory");
            modelBuilder.Entity<InventoryStaff>().ToTable("inventory_staff");
            modelBuilder.Entity<InventoryTransaction>().ToTable("inventory_transactions");
            modelBuilder.Entity<MedicalHistory>().ToTable("medical_history");
            modelBuilder.Entity<Medication>().ToTable("medications");
            modelBuilder.Entity<Notification>().ToTable("notifications");
            modelBuilder.Entity<Nurse>().ToTable("nurses");
            modelBuilder.Entity<Patient>().ToTable("patients");
            modelBuilder.Entity<Payment>().ToTable("payments");
            modelBuilder.Entity<Prescription>().ToTable("prescriptions");
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<VitalSigns>().ToTable("vital_signs");

            base.OnModelCreating(modelBuilder);
        }

        // Helper method to test database connection
        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                return await Database.CanConnectAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Database connection test failed: {ex.Message}");
                return false;
            }
        }
    }
}