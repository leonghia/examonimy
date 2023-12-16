namespace ExamonimyWeb.DTOs.QuestionDTO
{
    public class TrueFalseQuestionGetDto : QuestionGetDto
    {               
        public required char CorrectAnswer { get; set; }
    }
}
