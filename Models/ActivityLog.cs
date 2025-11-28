using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IT_13FinalProject.Models
{
    [Table("activity_logs")]
    public class ActivityLog
    {
        [Key]
        [Column("log_id")]
        public int LogId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("activity_description", TypeName = "text")]
        public string ActivityDescription { get; set; } = string.Empty;

        [Column("activity_type")]
        public string? ActivityType { get; set; }

        [Column("ip_address")]
        public string? IpAddress { get; set; }

        [Column("user_agent", TypeName = "text")]
        public string? UserAgent { get; set; }

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
    }
}