using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IT_13FinalProject.Models
{
    [Table("patients")] // Explicit table name
    public class Patient
    {
        [Key]
        [Column("patient_id")] // Map to correct column name
        public int PatientId { get; set; }

        [Column("user_id")] // Map to correct column name - allow null for patient-only records
        public int? UserId { get; set; }

        [Required]
        [Column("first_name")] // Map to correct column name
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Column("last_name")] // Map to correct column name
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Column("middle_name")] // Map to correct column name
        [StringLength(100)]
        public string? MiddleName { get; set; }

        [Column("gender")] // Map to correct column name
        [StringLength(10)]
        public string? Gender { get; set; }

        [Column("date_of_birth")] // Map to correct column name
        public DateTime? DateOfBirth { get; set; }

        [Column("phone")] // Map to correct column name
        [StringLength(20)]
        public string? Phone { get; set; }

        [Column("email")] // Map to correct column name
        [StringLength(100)]
        public string? Email { get; set; }

        [Column("address")] // Map to correct column name
        public string? Address { get; set; }

        [Column("blood_type")] // Map to correct column name
        [StringLength(5)]
        public string? BloodType { get; set; }

        [Column("emergency_contact_name")] // Map to correct column name
        [StringLength(100)]
        public string? EmergencyContactName { get; set; }

        [Column("emergency_contact_relationship")] // Map to correct column name
        [StringLength(50)]
        public string? EmergencyContactRelationship { get; set; }

        [Column("emergency_contact_number")] // Map to correct column name
        [StringLength(20)]
        public string? EmergencyContactNumber { get; set; }

        [Column("civil_status")] // Map to correct column name
        [StringLength(20)]
        public string? CivilStatus { get; set; }

        [Column("occupation")] // Map to correct column name
        [StringLength(100)]
        public string? Occupation { get; set; }

        [Column("nationality")] // Map to correct column name
        [StringLength(50)]
        public string? Nationality { get; set; }

        [Column("religion")] // Map to correct column name
        [StringLength(50)]
        public string? Religion { get; set; }

        [Column("created_at")] // Map to correct column name
        public DateTime? CreatedAt { get; set; }

        [Column("updated_at")] // Map to correct column name
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public virtual User? User { get; set; }
        public virtual ICollection<HealthRecord> HealthRecords { get; set; } = new List<HealthRecord>();
        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public virtual ICollection<Billing> Billings { get; set; } = new List<Billing>();
        public virtual ICollection<Insurance> Insurances { get; set; } = new List<Insurance>();
        public virtual ICollection<VitalSigns> VitalSigns { get; set; } = new List<VitalSigns>();
        public virtual ICollection<MedicalHistory> MedicalHistories { get; set; } = new List<MedicalHistory>();
    }
}