using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamonimyWeb.Entities
{

    public class NotificationType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required Entity Entity { get; set; }

        [Required]
        public required Operation Operation { get; set; }
    }

    public class Notification
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(NotificationType))]
        public required int NotificationTypeId { get; set; }
        public NotificationType? NotificationType { get; set; }

        [Required]
        public required int EntityId { get; set; }

        public string? Href { get; set; }

        [Required]
        public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        [ForeignKey(nameof(Actor))]
        public required int ActorId { get; set; }
        public User? Actor { get; set; }

        public ICollection<User>? Receivers { get; set; }
        public ICollection<NotificationReceiver>? NotificationReceivers { get; set; }

    }

    public class NotificationReceiver
    {
        public required int NotificationId { get; set; } 

        public required int ReceiverId { get; set; }

        public Notification? Notification { get; set; }
        public User? Receiver { get; set; }
    }



    public enum Entity
    {
        ExamPaperReviewer,
        ExamPaperComment
    }

    public enum Operation
    {
        AskForReviewForExamPaper,
        CommentExamPaper,
        ApproveExamPaper,
        RejectExamPaper
    }

    public readonly struct AskForReviewForExamPaperNotiMessage
    {
        public AskForReviewForExamPaperNotiMessage(string authorFullName, string courseName)
        {
            AuthorFullName = authorFullName;
            CourseName = courseName;
        }

        public string AuthorFullName { get; }
        public string CourseName { get; }

        public override string ToString()
        {
            return $"{AuthorFullName} vừa tạo một đề thi mới cho môn học {CourseName} và muốn nhờ bạn review.";
        }
    }

    public readonly struct CommentExamPaperNotiMessage
    {
        public CommentExamPaperNotiMessage(string commenter, string examPaperCode)
        {
            Commenter = commenter;
            ExamPaperCode = examPaperCode;
        }

        public string Commenter { get; }
        public string ExamPaperCode { get; }

        public override string ToString()
        {
            return $"{Commenter} vừa để lại một nhận xét cho đề thi mã {ExamPaperCode} của bạn.";
        }
    }

    public readonly struct ApproveExamPaperNotiMessage
    {
        public string ReviewerFullName { get; }
        public string ExamPaperCode { get; }

        public ApproveExamPaperNotiMessage(string reviewerFullName, string examPaperCode)
        {
            ReviewerFullName = reviewerFullName;
            ExamPaperCode = examPaperCode;
        }

        public override string ToString()
        {
            return $"{ReviewerFullName} đã đồng ý phê duyệt đề thi mã {ExamPaperCode} của bạn.";
        }
    }

    public readonly struct RejectExamPaperNotiMessage
    {
        public string ReviewerFullName { get; }
        public string ExamPaperCode { get; }

        public RejectExamPaperNotiMessage(string reviewerFullName, string examPaperCode)
        {
            ReviewerFullName = reviewerFullName;
            ExamPaperCode = examPaperCode;
        }

        public override string ToString()
        {
            return $"{ReviewerFullName} đã từ chối phê duyệt đề thi mã {ExamPaperCode} của bạn.";
        }
    }
}
