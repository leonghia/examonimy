namespace ExamonimyWeb.DTOs.QuestionDTO
{
    public class MultipleChoiceQuestionWithMultipleCorrectAnswersWithoutAnswerGetDto : QuestionWithoutAnswerGetDto
    {
        public required string ChoiceA { get; set; }
        public required string ChoiceB { get; set; }
        public required string ChoiceC { get; set; }
        public required string ChoiceD { get; set; }
    }
}
