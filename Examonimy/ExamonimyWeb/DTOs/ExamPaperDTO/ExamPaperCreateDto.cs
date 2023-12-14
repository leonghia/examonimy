using System.ComponentModel.DataAnnotations;

namespace ExamonimyWeb.DTOs.ExamPaperDTO
{
    public class ExamPaperCreateDto
    {      

        [Required]
        [StringLength(16)]
        public required string ExamPaperCode { get; set; }
       
        [Required]
        public required int CourseId { get; set; }

        [Required]
        public ICollection<ExamPaperQuestionCreateDto>? ExamPaperQuestions { get; set; }
    }
}
