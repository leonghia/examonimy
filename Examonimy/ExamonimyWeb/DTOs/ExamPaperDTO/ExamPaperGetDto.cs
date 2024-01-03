namespace ExamonimyWeb.DTOs.ExamPaperDTO
{
    public class ExamPaperGetDto
    {
        public required int Id { get; set; }
        public required string ExamPaperCode { get; set; }
        public required string AuthorName { get; set; }
    }
}
