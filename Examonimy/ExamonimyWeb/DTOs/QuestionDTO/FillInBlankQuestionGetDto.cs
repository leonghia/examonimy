namespace ExamonimyWeb.DTOs.QuestionDTO
{
    public class FillInBlankQuestionGetDto
    {
        
        
        public required QuestionGetDto Question { get; set; }
        
        public required string CorrectAnswers { get; set; }
    }
}
