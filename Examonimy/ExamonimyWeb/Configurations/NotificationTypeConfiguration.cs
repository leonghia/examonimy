using ExamonimyWeb.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExamonimyWeb.Configurations
{
    public class NotificationTypeConfiguration : IEntityTypeConfiguration<NotificationType>
    {
        public void Configure(EntityTypeBuilder<NotificationType> builder)
        {
            builder.HasData(
                new NotificationType
                {
                    Id = 1,
                    Entity = Utilities.Entity.ExamPaperReviewer,
                    Operation = Utilities.Operation.AskForReviewForExamPaper
                },
                new NotificationType
                {
                    Id = 2,
                    Entity = Utilities.Entity.ExamPaperComment,
                    Operation = Utilities.Operation.CommentExamPaper
                }
                );
        }
    }
}
