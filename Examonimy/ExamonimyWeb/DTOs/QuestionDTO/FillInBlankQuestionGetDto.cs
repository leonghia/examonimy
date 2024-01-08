namespace ExamonimyWeb.DTOs.QuestionDTO
{
    public class FillInBlankQuestionGetDto : QuestionGetDto
    {                 
        public required IList<string> CorrectAnswers { get; set; }
    }
}
