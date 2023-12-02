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
        }
        public required DbSet<User> Users { get; init; }
        public required DbSet<Role> Roles { get; init; }
        public required DbSet<Course> Courses { get; init; }
        public required DbSet<Exam> Exams { get; init; }
    }
}
