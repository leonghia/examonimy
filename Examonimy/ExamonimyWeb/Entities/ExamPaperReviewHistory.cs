using System.ComponentModel.DataAnnotations;

namespace ExamonimyWeb.Entities
{
    public class ExamPaperReviewHistory
    {
        [Key]
        public int Id { get; set; }

        public required int ExamPaperId { get; set; }
        public ExamPaper? ExamPaper { get; set; }

        public required int ActorId { get; set; }
        public User? Actor { get; set; }

        public required int OperationId { get; set; }

        public required int EntityId { get; set; }

        public required DateTime CreatedAt { get; set; }
    }
}
