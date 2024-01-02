namespace ExamonimyWeb.DTOs.ExamDTO
{
    public class ExamGetDto
    {
        public required int Id { get; set; }
        public required string MainClassName { get; set; }
        public required string ExamPaperCode { get; set; }
        public required string CourseName { get; set; }
        public required DateTime From { get; set; }
        public required DateTime To { get; set; }
        public required int TimeAllowedInMinutes { get; set; }
    }
}
