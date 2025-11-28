namespace IT_13FinalProject.Models
{
    public class Medication
    {
        public int MedicationId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? DosageForm { get; set; }

        // Navigation properties
        public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
        public virtual ICollection<Inventory> InventoryItems { get; set; } = new List<Inventory>();
    }
}