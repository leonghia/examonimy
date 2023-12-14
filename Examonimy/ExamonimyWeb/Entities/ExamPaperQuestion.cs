namespace ExamonimyWeb.Entities
{
    public class ExamPaperQuestion
    {
        public required int ExamPaperId { get; set; }
        public required int QuestionId { get; set; }
        public required byte Number { get; set; }
    }
}