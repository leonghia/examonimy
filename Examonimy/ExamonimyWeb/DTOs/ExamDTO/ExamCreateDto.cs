using System.ComponentModel.DataAnnotations;

namespace ExamonimyWeb.DTOs.ExamDTO
{
    public class ExamCreateDto
    {

        [Required]
        [StringLength(256)]
        public required string Name { get; set; }

        [Required]
        public required int CourseId { get; set; }

        [Required]
        public required int AuthorId { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public required int TimeAllowed { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public required DateTime From { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public required DateTime To { get; set; }

        [StringLength(512)]
        public string? Description { get; set; }
    }
}
