using ExamonimyWeb.DTOs.QuestionDTO;

namespace ExamonimyWeb.DTOs.ExamPaperDTO
{
    public class ExamPaperQuestionGetDto
    {
        public required byte Number { get; set; }
        public required QuestionGetDto Question { get; set; }
    }
}
