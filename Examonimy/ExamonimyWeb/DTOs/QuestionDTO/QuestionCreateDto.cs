using System.ComponentModel.DataAnnotations;

namespace ExamonimyWeb.DTOs.QuestionDTO;

public class QuestionCreateDto
{
    [Required]
    public required int CourseId { get; set; }

    [Required]
    public required int QuestionTypeId { get; set; }

    [Required]
    public required int CourseModuleId { get; set; }

    [Required]
    public required string QuestionContent { get; set; }    
}
