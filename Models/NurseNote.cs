using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IT_13FinalProject.Models
{
    [Table("nurse_notes")]
    public class NurseNote
    {
        [Key]
        [Column("nurse_note_id")]
        public int NurseNoteId { get; set; }

        [Column("patient_id")]
        public int PatientId { get; set; }

        [Column("category")]
        [StringLength(100)]
        public string? Category { get; set; }

        [Column("details")]
        [StringLength(2000)]
        public string? Details { get; set; }

        [Column("pain_scale")]
        [StringLength(10)]
        public string? PainScale { get; set; }

        [Column("mood")]
        [StringLength(50)]
        public string? Mood { get; set; }

        [Column("activity")]
        [StringLength(50)]
        public string? Activity { get; set; }

        [Column("nutrition")]
        [StringLength(50)]
        public string? Nutrition { get; set; }

        [Column("nurse_name")]
        [StringLength(100)]
        public string? NurseName { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [Column("is_archived")]
        public bool IsArchived { get; set; } = false;

        [Column("archived_at")]
        public DateTime? ArchivedAt { get; set; }

        [ForeignKey("PatientId")]
        public virtual Patient? Patient { get; set; }
    }
}
