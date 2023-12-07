using ExamonimyWeb.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExamonimyWeb.DTOs.QuestionDTO
{
    public class ShortAnswerQuestionGetDto
    {
        
        public required QuestionGetDto Question { get; set; }

        
        public required string CorrectAnswer { get; set; }
    }
}
