namespace IT_13FinalProject.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public int? UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string? NotificationType { get; set; }
        public bool? IsRead { get; set; }
        public string? RelatedEntityType { get; set; }
        public int? RelatedEntityId { get; set; }
        public DateTime? CreatedAt { get; set; }

        // Navigation properties
        public virtual User? User { get; set; }
    }
}