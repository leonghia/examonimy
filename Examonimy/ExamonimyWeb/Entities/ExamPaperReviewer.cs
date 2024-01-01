using ExamonimyWeb.Enums;
using System.ComponentModel.DataAnnotations;

namespace ExamonimyWeb.Entities
{
    public class ExamPaperReviewer
    {
        [Key]
        public int Id { get; set; }
        public required int ExamPaperId { get; set; }
        public required int ReviewerId { get; set; }
        public ExamPaper? ExamPaper { get; set; }
        public User? Reviewer { get; set; }
        public ExamPaperStatus ReviewStatus { get; set; } = ExamPaperStatus.Pending;
    }
}
