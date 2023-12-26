using ExamonimyWeb.Entities;
using ExamonimyWeb.Enums;
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
                    Entity = Entity.ExamPaperReviewer,
                    Operation = Operation.AskForReviewForExamPaper
                },
                new NotificationType
                {
                    Id = 2,
                    Entity = Entity.ExamPaperComment,
                    Operation = Operation.CommentExamPaper
                }
                );
        }
    }
}
