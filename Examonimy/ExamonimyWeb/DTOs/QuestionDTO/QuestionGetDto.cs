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
          
        public required CourseGetDto Course { get; set; }
           
        public required QuestionTypeGetDto QuestionType { get; set; }
          
        public required QuestionLevelGetDto QuestionLevel { get; set; }
      
        public required string QuestionContent { get; set; }
             
        public UserGetDto? Author { get; set; }
    }
}
