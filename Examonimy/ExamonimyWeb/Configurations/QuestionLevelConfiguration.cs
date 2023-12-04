using ExamonimyWeb.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExamonimyWeb.Configurations
{
    public class QuestionLevelConfiguration : IEntityTypeConfiguration<QuestionLevel>
    {
        public void Configure(EntityTypeBuilder<QuestionLevel> builder)
        {
            builder.HasData(
                new QuestionLevel { Id = 1, Name = "Nhận biết" },
                new QuestionLevel { Id = 2, Name = "Thông hiểu" },
                new QuestionLevel { Id = 3, Name = "Vận dụng" },
                new QuestionLevel { Id = 4, Name = "Vận dụng cao" }
                );
        }
    }
}
