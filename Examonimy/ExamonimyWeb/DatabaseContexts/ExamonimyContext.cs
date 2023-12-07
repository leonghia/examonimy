using ExamonimyWeb.Configurations;
using ExamonimyWeb.Entities;
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
            modelBuilder.ApplyConfiguration<Course>(new CourseConfiguration());
            modelBuilder.ApplyConfiguration<QuestionType>(new QuestionTypeConfiguration());
            modelBuilder.ApplyConfiguration<QuestionLevel>(new QuestionLevelConfiguration());
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

    }
}
