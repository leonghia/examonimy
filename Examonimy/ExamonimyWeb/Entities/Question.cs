using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamonimyWeb.Entities
{
    public class Question
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Course")]
        [Required]
        public required int CourseId { get; set; }
        public Course? Course { get; set; }


        [ForeignKey("QuestionType")]
        [Required]
        public required int QuestionTypeId { get; set; }
        public QuestionType? QuestionType { get; set; }

        [ForeignKey("QuestionLevel")]
        [Required]
        public required int QuestionLevelId { get; set; }
        public QuestionLevel? QuestionLevel { get; set; }

        [Required]
        public required string QuestionContent { get; set; }

        [ForeignKey("Author")]
        [Required]
        public required int AuthorId { get; set; }
        public User? Author { get; set; }
    }
}
