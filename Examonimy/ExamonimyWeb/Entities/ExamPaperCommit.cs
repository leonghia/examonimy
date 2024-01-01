using System.ComponentModel.DataAnnotations;

namespace ExamonimyWeb.Entities
{
    public class ExamPaperCommit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required int ExamPaperId { get; set; }
        public ExamPaper? ExamPaper { get; set; }

        [Required]
        public required string Message { get; set; }

        [Required]
        public DateTime CommitedAt { get; set; } = DateTime.UtcNow;
    }
}
