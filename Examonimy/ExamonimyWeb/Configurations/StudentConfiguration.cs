using ExamonimyWeb.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExamonimyWeb.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasData(
                new Student
                {
                    UserId = 2,
                    RollNumber = "TH2209059",
                    MainClassId = 1
                },
                new Student
                {
                    UserId = 3,
                    RollNumber = "TH2209079",
                    MainClassId = 1
                },
                new Student
                {
                    UserId = 4,
                    RollNumber = "TH2209080",
                    MainClassId = 1
                },
                new Student
                {
                    UserId = 5,
                    RollNumber = "TH2209065",
                    MainClassId = 1
                },
                new Student
                {
                    UserId = 16,
                    RollNumber = "TH2209053",
                    MainClassId = 1
                }
                );
        }
    }
}
