using ExamonimyWeb.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExamonimyWeb.Configurations
{
    public class MainClassConfiguration : IEntityTypeConfiguration<MainClass>
    {
        public void Configure(EntityTypeBuilder<MainClass> builder)
        {
            builder.HasData(
            new MainClass
            {
                Id = 1,
                Name = "T2210M",
                TeacherId = 1
            },
            new MainClass
            {
                Id = 2,
                Name = "T2204M",
                TeacherId = 1
            },
            new MainClass
            {
                Id = 3,
                Name = "T2207A",
                TeacherId = 1
            },
            new MainClass
            {
                Id = 4,
                Name = "T2305E",
                TeacherId = 1
            },
            new MainClass
            {
                Id = 5,
                Name = "T2210A",
                TeacherId = 1
            });
        }
    }
}
