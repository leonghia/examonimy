using ExamonimyWeb.DTOs.CourseDTO;
using ExamonimyWeb.DTOs.UserDTO;
using System.Text.Json.Serialization;

namespace ExamonimyWeb.DTOs.QuestionDTO
{
    [JsonDerivedType(typeof(MultipleChoiceQuestionWithOneCorrectAnswerWithoutAnswerGetDto))]
    [JsonDerivedType(typeof(MultipleChoiceQuestionWithMultipleCorrectAnswersWithoutAnswerGetDto))]
    [JsonDerivedType(typeof(TrueFalseQuestionWithoutAnswerGetDto))]
    [JsonDerivedType(typeof(ShortAnswerQuestionWithoutAnswerGetDto))]
    [JsonDerivedType(typeof(FillInBlankQuestionWithoutAnswerGetDto))]
    public class QuestionWithoutAnswerGetDto
    {
        public required int Id { get; set; }      
        public required string QuestionContent { get; set; }     
        public required QuestionTypeGetDto QuestionType { get; set; }
    }
}
