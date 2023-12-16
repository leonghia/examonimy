using ExamonimyWeb.Entities;
using ExamonimyWeb.Managers.UserManager;
using ExamonimyWeb.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExamonimyWeb.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User
                {
                    Id = 1,
                    FullName = "Trịnh Quang Hòa",
                    Username = "hoatq",
                    Email = "hoatq@fpt.edu.vn",
                    RoleId = 2,
                    PasswordHash = PasswordHelper.HashPassword("aptech", out string salt1),
                    PasswordSalt = salt1,
                    NormalizedUsername = "hoatq".ToUpperInvariant(),
                    NormalizedEmail = "hoatq@fpt.edu.vn".ToUpperInvariant()
                },
                new User
                {
                    Id = 2,
                    FullName = "Trịnh Văn Phúc",
                    Username = "phuctrinh2004",
                    Email = "phuctv1112004@gmail.com",
                    RoleId = 3,
                    PasswordHash = PasswordHelper.HashPassword("aptech", out string salt2),
                    PasswordSalt = salt2,
                    NormalizedUsername = "phuctrinh2004".ToUpperInvariant(),
                    NormalizedEmail = "phuctv1112004@gmail.com".ToUpperInvariant()
                },
                new User
                {
                    Id = 3,
                    FullName = "Nguyễn Thị Hương",
                    Username = "huongnguyen2004",
                    Email = "nguyenhuongg1104@gmail.com",
                    RoleId = 3,
                    PasswordHash = PasswordHelper.HashPassword("aptech", out string salt3),
                    PasswordSalt = salt3,
                    NormalizedUsername = "huongnguyen2004".ToUpperInvariant(),
                    NormalizedEmail = "nguyenhuongg1104@gmail.com".ToUpperInvariant()
                },
                new User
                {
                    Id = 4,
                    FullName = "Trịnh Đình Quốc",
                    Username = "quoctrinh2004",
                    Email = "draogon10a3@gmail.com",
                    RoleId = 3,
                    PasswordHash = PasswordHelper.HashPassword("aptech", out string salt4),
                    PasswordSalt = salt4,
                    NormalizedUsername = "quoctrinh2004".ToUpperInvariant(),
                    NormalizedEmail = "draogon10a3@gmail.com".ToUpperInvariant()
                },
                new User
                {
                    Id = 5,
                    FullName = "Nguyễn Hữu Hùng",
                    Username = "hungnguyen1998",
                    Email = "n2h1706@gmail.com",
                    RoleId = 3,
                    PasswordHash = PasswordHelper.HashPassword("aptech", out string salt5),
                    PasswordSalt = salt5,
                    NormalizedUsername = "hungnguyen1998".ToUpperInvariant(),
                    NormalizedEmail = "n2h1706@gmail.com".ToUpperInvariant()
                }
                );
        }
    }
}
