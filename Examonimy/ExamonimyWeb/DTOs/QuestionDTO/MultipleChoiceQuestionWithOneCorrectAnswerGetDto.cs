namespace ExamonimyWeb.DTOs.QuestionDTO
{
    public class MultipleChoiceQuestionWithOneCorrectAnswerGetDto : QuestionGetDto
    {         
        public required string ChoiceA { get; set; }

        
        public required string ChoiceB { get; set; }

        
        public required string ChoiceC { get; set; }

        
        public required string ChoiceD { get; set; }

        
        public required char CorrectAnswer { get; set; }
    }
}
