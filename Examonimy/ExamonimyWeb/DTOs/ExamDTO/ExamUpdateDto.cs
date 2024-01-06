using System.ComponentModel.DataAnnotations;

namespace ExamonimyWeb.DTOs.ExamDTO;

public class ExamUpdateDto
{  

    [Required]
    public required int ExamPaperId { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public required DateTime From { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public required DateTime To { get; set; }
}


