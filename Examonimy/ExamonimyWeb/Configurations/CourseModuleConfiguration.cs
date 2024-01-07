using ExamonimyWeb.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExamonimyWeb.Configurations
{
    public class CourseModuleConfiguration : IEntityTypeConfiguration<CourseModule>
    {
        public void Configure(EntityTypeBuilder<CourseModule> builder)
        {
            builder.HasData(
                new CourseModule
                {
                    Id = 1,
                    Name = "Getting Starte with C#",
                    CourseId = 8
                },
                new CourseModule
                {
                    Id = 2,
                    Name = "Variables and Data Types in C#",
                    CourseId = 8
                },
                new CourseModule
                {
                    Id = 3,
                    Name = "Statements and Operators",
                    CourseId = 8
                },
                new CourseModule
                {
                    Id = 4,
                    Name = "Programming Constructs and Arrays",
                    CourseId = 8
                },
                new CourseModule
                {
                    Id = 5,
                    Name = "Classes and Methods",
                    CourseId = 8
                },
                new CourseModule
                {
                    Id = 6,
                    Name = "Inheritance and Polymorphism",
                    CourseId = 8
                },
                new CourseModule
                {
                    Id = 7,
                    Name = "Abstract Classes and Interfaces",
                    CourseId = 8
                },
                new CourseModule
                {
                    Id = 8,
                    Name = "Properties, Indexers and Record Types",
                    CourseId = 8
                },
                new CourseModule
                {
                    Id = 9,
                    Name = "Namespaces and Exception Handling",
                    CourseId = 8
                },
                new CourseModule
                {
                    Id = 10,
                    Name = "Events, Delegates and Collections",
                    CourseId = 8
                },
                new CourseModule
                {
                    Id = 11,
                    Name = "Generics and Iterators",
                    CourseId = 8
                },
                new CourseModule
                {
                    Id  = 12,
                    Name = "Advanced Concepts in C#",
                    CourseId = 8
                },
                new CourseModule
                {
                    Id = 13,
                    Name = "New Features of C#",
                    CourseId = 8
                },
                new CourseModule
                {
                    Id = 14,
                    Name = ".NET Core Development and the Future",
                    CourseId = 8
                },
                new CourseModule
                {
                    Id = 15,
                    Name = "GUI/Desktop Apps with C#",
                    CourseId = 8
                }
                );
        }
    }
}
