namespace IT_13FinalProject.Models
{
    public class Inventory
    {
        public int InventoryId { get; set; }
        public int? MedicationId { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public string? ItemType { get; set; }
        public string? Description { get; set; }
        public int? CurrentStock { get; set; }
        public int? ReorderLevel { get; set; }
        public decimal? UnitPrice { get; set; }
        public string? Supplier { get; set; }
        public DateTime? LastRestocked { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public virtual Medication? Medication { get; set; }
        public virtual ICollection<InventoryTransaction> InventoryTransactions { get; set; } = new List<InventoryTransaction>();
    }
}