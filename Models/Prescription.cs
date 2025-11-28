namespace IT_13FinalProject.Models
{
    public class Prescription
    {
        public int PrescriptionId { get; set; }
        public int RecordId { get; set; }
        public int MedicationId { get; set; }
        public string? Dosage { get; set; }
        public string? Frequency { get; set; }
        public string? Duration { get; set; }

        // Navigation properties
        public virtual HealthRecord? HealthRecord { get; set; }
        public virtual Medication? Medication { get; set; }
    }
}