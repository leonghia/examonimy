using ExamonimyWeb.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExamonimyWeb.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role { Id = 1, Name = "Administrator" },
                new Role { Id = 2, Name = "Teacher" },
                new Role { Id = 3, Name = "Student" }
                );
        }
    }
}
