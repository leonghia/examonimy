namespace ExamonimyWeb.DTOs.QuestionDTO
{
    public class MultipleChoiceQuestionWithMultipleCorrectAnswersGetDto
    {
        
        
        public required QuestionGetDto Question { get; set; }

       
        public required string ChoiceA { get; set; }

        
        public required string ChoiceB { get; set; }

        
        public required string ChoiceC { get; set; }

       
        public required string ChoiceD { get; set; }

        
        public required string CorrectAnswers { get; set; }
    }
}
