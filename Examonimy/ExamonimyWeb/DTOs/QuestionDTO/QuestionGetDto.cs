using ExamonimyWeb.DTOs.CourseDTO;
using ExamonimyWeb.DTOs.UserDTO;
using System.Text.Json.Serialization;

namespace ExamonimyWeb.DTOs.QuestionDTO
{
    [JsonDerivedType(typeof(MultipleChoiceQuestionWithOneCorrectAnswerGetDto))]
    [JsonDerivedType(typeof(MultipleChoiceQuestionWithMultipleCorrectAnswersGetDto))]
    [JsonDerivedType(typeof(TrueFalseQuestionGetDto))]
    [JsonDerivedType(typeof(ShortAnswerQuestionGetDto))]
    [JsonDerivedType(typeof(FillInBlankQuestionGetDto))]
    public class QuestionGetDto
    {
        
        public required int Id { get; set; }
          
        public required string Course { get; set; }
           
        public required string QuestionType { get; set; } 
      
        public required string QuestionContent { get; set; }
             
        public required string Author { get; set; }
    }
}
