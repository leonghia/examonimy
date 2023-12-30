using System.ComponentModel.DataAnnotations;

namespace ExamonimyWeb.Entities
{
    public class ExamPaperComment
    {
        [Key]
        public int Id { get; set; }

        public required int ExamPaperId { get; set; }
        public ExamPaper? ExamPaper { get; set; }

        public required int CommenterId { get; set; }
        public User? Commenter { get; set; }

        public required string Comment { get; set; }
        public required DateTime CommentedAt { get; set; }
    }
}
