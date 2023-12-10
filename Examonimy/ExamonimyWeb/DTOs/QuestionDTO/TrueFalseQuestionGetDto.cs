namespace ExamonimyWeb.DTOs.QuestionDTO
{
    public class TrueFalseQuestionGetDto : QuestionGetDto
    {
              
        
        public required bool CorrectAnswer { get; set; }
    }
}
