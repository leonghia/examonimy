namespace ExamonimyWeb.DTOs.QuestionDTO
{
    public class TrueFalseQuestionGetDto
    {
        
        
        public required QuestionGetDto Question { get; set; }
        
        public required bool CorrectAnswer { get; set; }
    }
}
