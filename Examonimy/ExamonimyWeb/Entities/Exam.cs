using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamonimyWeb.Entities
{
    public class Exam
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(MainClass))]
        public required int MainClassId { get; set; }
        public MainClass? MainClass { get; set; }

        [Required]
        [ForeignKey(nameof(ExamPaper))]
        public required int ExamPaperId { get; set; }
        public ExamPaper? ExamPaper { get; set; }

        [Required]
        public required DateTime From { get; set; }

        [Required]
        public required DateTime To { get; set; }
    }
}
