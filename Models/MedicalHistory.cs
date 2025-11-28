namespace IT_13FinalProject.Models
{
    public class MedicalHistory
    {
        public int HistoryId { get; set; }
        public int PatientId { get; set; }
        public string? Allergies { get; set; }
        public string? PastIllnesses { get; set; }
        public string? PastSurgeries { get; set; }
        public string? CurrentMedications { get; set; }
        public string? FamilyHistory { get; set; }
        public string? ImmunizationHistory { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public virtual Patient? Patient { get; set; }
    }
}