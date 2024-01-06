namespace ExamonimyWeb.DTOs.ExamDTO
{
    public class ExamForStudentGetDto
    {
        public required int Id { get; set; }       
        public required string CourseName { get; set; }
        public required DateTime From { get; set; }
        public required DateTime To { get; set; }
        public required int TimeAllowedInMinutes { get; set; }
    }
}
