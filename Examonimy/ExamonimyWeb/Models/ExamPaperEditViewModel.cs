namespace ExamonimyWeb.Models
{
    public class ExamPaperEditViewModel : AuthorizedViewModel
    {
        public required int ExamPaperId { get; set; }
        public required string ExamPaperCode { get; set; }
        public required string CourseName { get; set; }
        public required string AuthorName { get; set; }
        public required int CourseId { get; set; }
    }
}
