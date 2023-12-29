using System.ComponentModel.DataAnnotations;

namespace ExamonimyWeb.Entities
{
    public class ExamPaperQuestionComment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required int ExamPaperQuestionId { get; set; }

        public DateTime CommentedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public required int CommenterId { get; set; }
        public User? Commenter { get; set; }

        [Required]
        public required string Comment { get; set; }
    }
}
