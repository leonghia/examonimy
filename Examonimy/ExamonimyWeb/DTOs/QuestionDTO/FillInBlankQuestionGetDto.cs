namespace ExamonimyWeb.DTOs.QuestionDTO
{
    public class FillInBlankQuestionGetDto : QuestionGetDto
    {                 
        public required IEnumerable<string> CorrectAnswers { get; set; }
    }
}
