using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IT_13FinalProject.Models
{
    [Table("health_records")]
    public class HealthRecord
    {
        [Key]
        [Column("record_id")]
        public int RecordId { get; set; }

        [Column("patient_id")]
        public int PatientId { get; set; }

        [Column("doctor_id")]
        public int DoctorId { get; set; }

        [Column("diagnosis", TypeName = "text")]
        public string? Diagnosis { get; set; }

        [Column("treatment", TypeName = "text")]
        public string? Treatment { get; set; }

        [Column("prescription", TypeName = "text")]
        public string? Prescription { get; set; }

        [Column("record_date")]
        public DateTime? RecordDate { get; set; }

        [Column("nurse_id")]
        public int? NurseId { get; set; }

        [Column("subjective_findings", TypeName = "text")]
        public string? SubjectiveFindings { get; set; }

        [Column("objective_findings", TypeName = "text")]
        public string? ObjectiveFindings { get; set; }

        [Column("assessment_diagnosis", TypeName = "text")]
        public string? AssessmentDiagnosis { get; set; }

        [Column("icd10_code")]
        public string? Icd10Code { get; set; }

        [Column("additional_tests_order", TypeName = "text")]
        public string? AdditionalTestsOrder { get; set; }

        [Column("doctor_remarks", TypeName = "text")]
        public string? DoctorRemarks { get; set; }

        [Column("nurse_remarks", TypeName = "text")]
        public string? NurseRemarks { get; set; }

        [Column("recommendations", TypeName = "text")]
        public string? Recommendations { get; set; }

        [Column("medicine_name")]
        public string? MedicineName { get; set; }

        [Column("dosage")]
        public string? Dosage { get; set; }

        [Column("frequency")]
        public string? Frequency { get; set; }

        [Column("duration")]
        public string? Duration { get; set; }

        [Column("treatment_notes", TypeName = "text")]
        public string? TreatmentNotes { get; set; }

        [Column("follow_up_date")]
        public DateTime? FollowUpDate { get; set; }

        [Column("visit_status")]
        public string? VisitStatus { get; set; }

        [Column("doctor_signature")]
        public string? DoctorSignature { get; set; }

        [Column("approval_date")]
        public DateTime? ApprovalDate { get; set; }

        [Column("chief_complaint", TypeName = "text")]
        public string? ChiefComplaint { get; set; }

        [Column("symptom_duration")]
        public string? SymptomDuration { get; set; }

        [Column("visit_type")]
        public string? VisitType { get; set; }

        [Column("department")]
        public string? Department { get; set; }

        [Column("time_in_out")]
        public string? TimeInOut { get; set; }

        // Navigation properties with explicit foreign keys
        [ForeignKey(nameof(PatientId))]
        public virtual Patient? Patient { get; set; }

        [ForeignKey(nameof(DoctorId))]
        public virtual Doctor? Doctor { get; set; }

        [ForeignKey(nameof(NurseId))]
        public virtual Nurse? Nurse { get; set; }

        public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();

        // Presentation / computed properties used by UI (not mapped)
        [NotMapped]
        public int Id => RecordId;

        [NotMapped]
        public string PatientName =>
            Patient != null ? $"{(Patient.FirstName ?? string.Empty).Trim()} {(Patient.LastName ?? string.Empty).Trim()}".Trim() : string.Empty;

        [NotMapped]
        public string? PatientEmail => Patient?.Email;

        [NotMapped]
        public string DoctorName =>
            Doctor != null ? $"{(Doctor.FirstName ?? string.Empty).Trim()} {(Doctor.LastName ?? string.Empty).Trim()}".Trim() : string.Empty;

        [NotMapped]
        public string? DoctorEmail => Doctor?.Email;

        [NotMapped]
        public string NurseName =>
            Nurse != null ? $"{(Nurse.FirstName ?? string.Empty).Trim()} {(Nurse.LastName ?? string.Empty).Trim()}".Trim() : string.Empty;

        [NotMapped]
        public string? NurseEmail => Nurse?.Email;

        [NotMapped]
        public string DateOfCheckup => RecordDate?.ToString("MM/dd/yy") ?? string.Empty;
    }
}