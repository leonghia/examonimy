﻿using ExamonimyWeb.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamonimyWeb.Entities
{
    public class ExamPaper
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(16)]
        public required string ExamPaperCode { get; set; }

        [ForeignKey("Course")]
        [Required]
        public required int CourseId { get; set; }
        public Course? Course { get; set; }        

        [ForeignKey(nameof(Author))]
        [Required]
        public required int AuthorId { get; set; }
        
        public User? Author { get; set; }
        
        public ICollection<Question>? Questions { get; set; }
        public ICollection<ExamPaperQuestion>? ExamPaperQuestions { get; set; }

        public ExamPaperStatus? Status { get; set; } = ExamPaperStatus.Pending;

        public ICollection<User>? Reviewers { get; set; }
        public ICollection<ExamPaperReviewer>? ExamPaperReviewers { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
