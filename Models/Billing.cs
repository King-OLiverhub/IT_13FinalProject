using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IT_13FinalProject.Models
{
    [Table("billing")]
    public class Billing
    {
        [Key]
        [Column("bill_id")]
        public int BillId { get; set; }

        [Column("patient_id")]
        public int PatientId { get; set; }

        [Column("appointment_id")]
        public int? AppointmentId { get; set; }

        [Column("invoice_number")]
        [StringLength(50)]
        public string? InvoiceNumber { get; set; }

        [Column("total_amount")]
        public decimal TotalAmount { get; set; }

        [Column("discount_amount")]
        public decimal? DiscountAmount { get; set; }

        [Column("tax_amount")]
        public decimal? TaxAmount { get; set; }

        [Column("final_amount")]
        public decimal? FinalAmount { get; set; }

        [Column("billing_date")]
        public DateTime? BillingDate { get; set; }

        [Column("due_date")]
        public DateTime? DueDate { get; set; }

        [Column("payment_due_date")]
        public DateTime? PaymentDueDate { get; set; }

        [Column("status")]
        [StringLength(20)]
        public string? Status { get; set; }

        [Column("payment_method")]
        [StringLength(50)]
        public string? PaymentMethod { get; set; }

        [Column("payment_status")]
        [StringLength(20)]
        public string? PaymentStatus { get; set; }

        [Column("notes")]
        [StringLength(1000)]
        public string? Notes { get; set; }

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [Column("created_by")]
        public int? CreatedBy { get; set; }

        // Navigation properties
        [ForeignKey(nameof(PatientId))]
        public virtual Patient? Patient { get; set; }

        [ForeignKey(nameof(AppointmentId))]
        public virtual Appointment? Appointment { get; set; }

        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}