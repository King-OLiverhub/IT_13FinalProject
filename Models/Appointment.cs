using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IT_13FinalProject.Models
{
    [Table("appointments")]
    public class Appointment
    {
        [Key]
        [Column("appointment_id")]
        public int AppointmentId { get; set; }

        [Column("patient_id")]
        public int PatientId { get; set; }

        [Column("doctor_id")]
        public int DoctorId { get; set; }

        [Column("appointment_date")]
        public DateTime AppointmentDate { get; set; }

        [Column("status")]
        [StringLength(20)]
        public string? Status { get; set; }

        [Column("notes")]
        [StringLength(1000)]
        public string? Notes { get; set; }

        [Column("duration_minutes")]
        public int? DurationMinutes { get; set; }

        [Column("appointment_type")]
        [StringLength(50)]
        public string? AppointmentType { get; set; }

        [Column("department_id")]
        public int? DepartmentId { get; set; }

        [Column("reason_for_visit")]
        [StringLength(500)]
        public string? ReasonForVisit { get; set; }

        [Column("symptoms")]
        [StringLength(1000)]
        public string? Symptoms { get; set; }

        [Column("is_urgent")]
        public bool IsUrgent { get; set; } = false;

        [Column("payment_status")]
        [StringLength(20)]
        public string? PaymentStatus { get; set; }

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public virtual Patient? Patient { get; set; }
        public virtual Doctor? Doctor { get; set; }
        public virtual Department? Department { get; set; }
        public virtual ICollection<Billing> Billings { get; set; } = new List<Billing>();
        public virtual ICollection<HealthRecord> HealthRecords { get; set; } = new List<HealthRecord>();
        public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
    }
}