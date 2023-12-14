namespace ExamonimyWeb.DTOs.QuestionDTO
{
    public class FillInBlankQuestionGetDto : QuestionGetDto
    {                 
        public required string CorrectAnswers { get; set; }
    }
}
