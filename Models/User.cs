using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IT_13FinalProject.Models
{
    [Table("users")] // Explicit table name
    public class User
    {
        [Key]
        [Column("user_id")] // Map to correct column name
        public int UserId { get; set; }

        [Required]
        [Column("username")] // Map to correct column name
        [StringLength(100)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [Column("password_hash")] // Map to correct column name
        [StringLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [Column("role")] // Map to correct column name
        [StringLength(20)]
        public string Role { get; set; } = string.Empty;

        [Column("created_at")] // Map to correct column name
        public DateTime? CreatedAt { get; set; }

        [Column("email")] // Map to correct column name
        [StringLength(100)]
        public string? Email { get; set; }

        [Column("full_name")] // Map to correct column name
        [StringLength(200)]
        public string? FullName { get; set; }

        [Column("is_active")] // Map to correct column name
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public virtual Patient? Patient { get; set; }
        public virtual Doctor? Doctor { get; set; }
        public virtual Nurse? Nurse { get; set; }
        public virtual BillingStaff? BillingStaff { get; set; }
        public virtual InventoryStaff? InventoryStaff { get; set; }
    }
}