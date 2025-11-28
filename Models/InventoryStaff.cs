namespace IT_13FinalProject.Models
{
    public class InventoryStaff
    {
        public int InventoryStaffId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Email { get; set; }

        // Navigation properties
        public virtual User? User { get; set; }
    }
}