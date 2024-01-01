using ExamonimyWeb.Configurations;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Managers.UserManager;
using Microsoft.EntityFrameworkCore;

namespace ExamonimyWeb.DatabaseContexts
{
    public class ExamonimyContext : DbContext
    {


        public ExamonimyContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration<Role>(new RoleConfiguration());
            //modelBuilder.ApplyConfiguration<User>(new UserConfiguration());
            modelBuilder.ApplyConfiguration<Course>(new CourseConfiguration());
            modelBuilder.ApplyConfiguration<QuestionType>(new QuestionTypeConfiguration());
            modelBuilder.ApplyConfiguration<QuestionLevel>(new QuestionLevelConfiguration());
            modelBuilder.ApplyConfiguration<NotificationType>(new NotificationTypeConfiguration());

            modelBuilder.Entity<ExamPaper>()
                .HasMany(eP => eP.Questions)
                .WithMany(q => q.ExamPapers)
                .UsingEntity<ExamPaperQuestion>();

            modelBuilder.Entity<ExamPaper>()
                .HasOne(eP => eP.Author)
                .WithMany(a => a.ExamPapersCreated)
                .HasForeignKey(eP => eP.AuthorId)
                .IsRequired(true);

            modelBuilder.Entity<ExamPaper>()
                .HasMany(eP => eP.Reviewers)
                .WithMany(r => r.ExamPapersToReview)
                .UsingEntity<ExamPaperReviewer>(
                l => l.HasOne(ePR => ePR.Reviewer).WithMany(r => r.ExamPaperReviewers).HasForeignKey(ePR => ePR.ReviewerId),
                r => r.HasOne(ePR => ePR.ExamPaper).WithMany(eP => eP.ExamPaperReviewers).HasForeignKey(ePR => ePR.ExamPaperId)
                );

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Actor)
                .WithMany(a => a.NotificationsTriggered)
                .HasForeignKey(n => n.ActorId)
                .IsRequired(true);

            modelBuilder.Entity<Notification>()
                .HasMany(n => n.Receivers)
                .WithMany(r => r.Notifications)
                .UsingEntity<NotificationReceiver>(
                l => l.HasOne(nR => nR.Receiver).WithMany(r => r.NotificationReceivers).HasForeignKey(nR => nR.ReceiverId),
                r => r.HasOne(nR => nR.Notification).WithMany(n => n.NotificationReceivers).HasForeignKey(nR => nR.NotificationId)
                );

            modelBuilder.Entity<ExamPaperQuestionComment>()
                .HasOne<ExamPaperQuestion>()
                .WithMany(ePQ => ePQ.ExamPaperQuestionComments)
                .HasForeignKey(ePQC => ePQC.ExamPaperQuestionId)
                .IsRequired(true);

            modelBuilder.Entity<ExamPaperQuestionComment>()
                .HasOne(ePQC => ePQC.Commenter)
                .WithMany()
                .HasForeignKey(ePQC => ePQC.CommenterId)
                .IsRequired(true);

            modelBuilder.Entity<Notification>()
                .Property(n => n.CreatedAt)
                .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            modelBuilder.Entity<ExamPaperQuestionComment>()
                .Property(epqc => epqc.CommentedAt)
                .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            modelBuilder.Entity<ExamPaperComment>()
                .HasOne(c => c.ExamPaper)
                .WithMany()
                .HasForeignKey(c => c.ExamPaperId)
                .IsRequired(true);

            modelBuilder.Entity<ExamPaperComment>()
                .HasOne(c => c.Commenter)
                .WithMany()
                .HasForeignKey(c => c.CommenterId)
                .IsRequired(true);

            modelBuilder.Entity<ExamPaperComment>()
                .Property(c => c.CommentedAt)
                .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            modelBuilder.Entity<ExamPaperReviewHistory>()
                .HasOne(h => h.ExamPaper)
                .WithMany()
                .HasForeignKey(h => h.ExamPaperId)
                .IsRequired(true);

            modelBuilder.Entity<ExamPaperReviewHistory>()
                .HasOne(h => h.Actor)
                .WithMany()
                .HasForeignKey(h => h.ActorId)
                .IsRequired(true);

            modelBuilder.Entity<ExamPaperReviewHistory>()
                .Property(h => h.CreatedAt)
                .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            modelBuilder.Entity<ExamPaperCommit>()
                .HasOne(epc => epc.ExamPaper)
                .WithMany()
                .HasForeignKey(epc => epc.ExamPaperId)
                .IsRequired(true);

            modelBuilder.Entity<ExamPaperCommit>()
                .Property(epc => epc.CommitedAt)
                .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        }
        public required DbSet<User> Users { get; init; }
        public required DbSet<Role> Roles { get; init; }
        public required DbSet<Course> Courses { get; init; }
        public required DbSet<QuestionType> QuestionTypes { get; init; }
        public required DbSet<QuestionLevel> QuestionLevels { get; init; }
        public required DbSet<Question> Questions { get; init; }
        public required DbSet<MultipleChoiceQuestionWithOneCorrectAnswer> MultipleChoiceQuestionsWithOneCorrectAnswer { get; init; }
        public required DbSet<MultipleChoiceQuestionWithMultipleCorrectAnswers> MultipleChoiceQuestionsWithMultipleCorrectAnswers { get; init; }
        public required DbSet<TrueFalseQuestion> TrueFalseQuestions { get; init; }
        public required DbSet<ShortAnswerQuestion> ShortAnswerQuestions { get; init; }
        public required DbSet<FillInBlankQuestion> FillInBlankQuestions { get; init; }

        public required DbSet<ExamPaper> ExamPapers { get; init; }

        public required DbSet<Notification> Notifications { get; init; }
        
        public required DbSet<NotificationType> NotificationTypes { get; init; }
        public required DbSet<ExamPaperComment> ExamPaperComments { get; init; }
        public required DbSet<ExamPaperReviewHistory> ExamPaperReviewHistory { get; init; }
        public required DbSet<ExamPaperCommit> ExamPaperCommits { get; init; }
    }
}
