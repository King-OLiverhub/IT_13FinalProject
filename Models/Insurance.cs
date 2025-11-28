namespace IT_13FinalProject.Models
{
    public class Insurance
    {
        public int InsuranceId { get; set; }
        public int PatientId { get; set; }
        public string? ProviderName { get; set; }
        public string? PolicyNumber { get; set; }
        public decimal? CoveragePercent { get; set; }

        // Navigation properties
        public virtual Patient? Patient { get; set; }
    }
}