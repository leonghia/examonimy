namespace ExamonimyWeb.Entities
{
    public class ExamPaperReviewer
    {
        public required int ExamPaperId { get; set; }
        public required int ReviewerId { get; set; }
        public ExamPaper? ExamPaper { get; set; }
        public User? Reviewer { get; set; }
    }
}
