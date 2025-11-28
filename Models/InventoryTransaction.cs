using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IT_13FinalProject.Models
{
    [Table("inventory_transactions")]
    public class InventoryTransaction
    {
        [Key]
        [Column("transaction_id")]
        public int TransactionId { get; set; }

        [Column("inventory_id")]
        public int InventoryId { get; set; }

        [Column("transaction_type")]
        public string? TransactionType { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("reference_type")]
        public string? ReferenceType { get; set; }

        [Column("reference_id")]
        public int? ReferenceId { get; set; }

        [Column("notes", TypeName = "text")]
        public string? Notes { get; set; }

        [Column("transaction_date")]
        public DateTime? TransactionDate { get; set; }

        [Column("performed_by")]
        public int? PerformedBy { get; set; }

        // Navigation properties
        [ForeignKey(nameof(InventoryId))]
        public virtual Inventory? Inventory { get; set; }

        [ForeignKey(nameof(PerformedBy))]
        public virtual User? PerformedByUser { get; set; }
    }
}