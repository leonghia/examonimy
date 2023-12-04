using ExamonimyWeb.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExamonimyWeb.Configurations
{
    public class QuestionTypeConfiguration : IEntityTypeConfiguration<QuestionType>
    {
        public void Configure(EntityTypeBuilder<QuestionType> builder)
        {
            builder.HasData(
                new QuestionType { Id = 1, Name = "Trắc nghiệm một đáp án đúng" },
                new QuestionType { Id = 2, Name = "Trắc nghiệm nhiều đáp án đúng" },
                new QuestionType { Id = 3, Name = "Đúng - Sai" },
                new QuestionType { Id = 4, Name = "Trả lời ngắn" },
                new QuestionType { Id = 5, Name = "Điền vào chỗ trống" }
                );
        }
    }
}
