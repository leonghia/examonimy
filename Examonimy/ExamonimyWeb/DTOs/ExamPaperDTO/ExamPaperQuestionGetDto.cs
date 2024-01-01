using ExamonimyWeb.DTOs.QuestionDTO;

namespace ExamonimyWeb.DTOs.ExamPaperDTO
{
    public class ExamPaperQuestionGetDto
    {
        public required int Id { get; set; }
        public required byte Number { get; set; }
        public required QuestionGetDto Question { get; set; }        
    }
}
