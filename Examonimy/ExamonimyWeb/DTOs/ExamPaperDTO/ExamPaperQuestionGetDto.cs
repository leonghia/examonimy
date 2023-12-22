using ExamonimyWeb.DTOs.QuestionDTO;

namespace ExamonimyWeb.DTOs.ExamPaperDTO
{
    public class ExamPaperQuestionGetDto
    {
        public required byte Number { get; set; }
        public required QuestionWithoutAnswerGetDto Question { get; set; }
    }
}
