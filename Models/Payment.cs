namespace IT_13FinalProject.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int BillId { get; set; }
        public string? PaymentMethod { get; set; }
        public decimal? AmountPaid { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? ReceiptNumber { get; set; }
        public string? PaymentStatus { get; set; }

        // Navigation properties
        public virtual Billing? Billing { get; set; }
    }
}