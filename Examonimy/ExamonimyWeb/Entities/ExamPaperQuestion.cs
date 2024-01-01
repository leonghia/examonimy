using System.ComponentModel.DataAnnotations;

namespace ExamonimyWeb.Entities
{
    public class ExamPaperQuestion
    {
        [Key]
        public int Id { get; set; }
        public required int ExamPaperId { get; set; }
        public required int QuestionId { get; set; }
        public required byte Number { get; set; }
        public ICollection<ExamPaperQuestionComment>? ExamPaperQuestionComments { get; set; }
    }
}