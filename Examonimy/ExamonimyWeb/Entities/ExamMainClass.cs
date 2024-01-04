using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamonimyWeb.Entities;

public class ExamMainClass
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(Exam))]
    [Required]
    public required int ExamId { get; set; }

    [ForeignKey(nameof(MainClass))]
    [Required]
    public required int MainClassId { get; set; }

    public Exam? Exam { get; set; }
    public MainClass? MainClass { get; set; }
}
