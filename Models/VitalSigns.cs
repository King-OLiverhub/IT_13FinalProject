namespace IT_13FinalProject.Models
{
    public class VitalSigns
    {
        public int VitalSignId { get; set; }
        public int PatientId { get; set; }
        public int? NurseId { get; set; }
        public int? DoctorId { get; set; }
        public string? BloodPressure { get; set; }
        public int? HeartRate { get; set; }
        public int? RespiratoryRate { get; set; }
        public decimal? Temperature { get; set; }
        public decimal? OxygenSaturation { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Height { get; set; }
        public decimal? BodyMassIndex { get; set; }
        public DateTime? RecordedDate { get; set; }

        // Navigation properties
        public virtual Patient? Patient { get; set; }
        public virtual Nurse? Nurse { get; set; }
        public virtual Doctor? Doctor { get; set; }
    }
}