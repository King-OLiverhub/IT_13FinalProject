using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IT_13FinalProject.Models
{
    [Table("nurses")]
    public class Nurse
    {
        [Key]
        [Column("nurse_id")]
        public int NurseId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [Column("first_name")]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Column("last_name")]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Column("middle_name")]
        [StringLength(100)]
        public string? MiddleName { get; set; }

        [Column("license_number")]
        [StringLength(50)]
        public string? LicenseNumber { get; set; }

        [Column("phone")]
        [StringLength(20)]
        public string? Phone { get; set; }

        [Column("email")]
        [StringLength(100)]
        public string? Email { get; set; }

        [Column("specialization")]
        [StringLength(200)]
        public string? Specialization { get; set; }

        [Column("department")]
        [StringLength(100)]
        public string? Department { get; set; }

        [Column("years_of_experience")]
        public int? YearsOfExperience { get; set; }

        [Column("shift")]
        [StringLength(20)]
        public string? Shift { get; set; }

        [Column("is_on_duty")]
        public bool IsOnDuty { get; set; } = true;

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public virtual User? User { get; set; }
        public virtual ICollection<HealthRecord> HealthRecords { get; set; } = new List<HealthRecord>();
        public virtual ICollection<VitalSigns> VitalSigns { get; set; } = new List<VitalSigns>();
        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}