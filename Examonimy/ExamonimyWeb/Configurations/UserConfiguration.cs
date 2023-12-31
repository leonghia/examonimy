﻿using ExamonimyWeb.Entities;
using ExamonimyWeb.Helpers;
using ExamonimyWeb.Managers.UserManager;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExamonimyWeb.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                //new User
                //{
                //    Id = 1,
                //    FullName = "Trịnh Quang Hòa",
                //    Username = "hoatq",
                //    Email = "hoatq@fpt.edu.vn",
                //    RoleId = 2,
                //    PasswordHash = PasswordHelper.HashPassword("aptech", out string salt1),
                //    PasswordSalt = salt1,
                //    NormalizedUsername = "hoatq".ToUpperInvariant(),
                //    NormalizedEmail = "hoatq@fpt.edu.vn".ToUpperInvariant()
                //},
                //new User
                //{
                //    Id = 2,
                //    FullName = "Trịnh Văn Phúc",
                //    Username = "phuctrinh2004",
                //    Email = "phuctv1112004@gmail.com",
                //    RoleId = 3,
                //    PasswordHash = PasswordHelper.HashPassword("aptech", out string salt2),
                //    PasswordSalt = salt2,
                //    NormalizedUsername = "phuctrinh2004".ToUpperInvariant(),
                //    NormalizedEmail = "phuctv1112004@gmail.com".ToUpperInvariant()
                //},
                //new User
                //{
                //    Id = 3,
                //    FullName = "Nguyễn Thị Hương",
                //    Username = "huongnguyen2004",
                //    Email = "nguyenhuongg1104@gmail.com",
                //    RoleId = 3,
                //    PasswordHash = PasswordHelper.HashPassword("aptech", out string salt3),
                //    PasswordSalt = salt3,
                //    NormalizedUsername = "huongnguyen2004".ToUpperInvariant(),
                //    NormalizedEmail = "nguyenhuongg1104@gmail.com".ToUpperInvariant()
                //},
                //new User
                //{
                //    Id = 4,
                //    FullName = "Trịnh Đình Quốc",
                //    Username = "quoctrinh2004",
                //    Email = "draogon10a3@gmail.com",
                //    RoleId = 3,
                //    PasswordHash = PasswordHelper.HashPassword("aptech", out string salt4),
                //    PasswordSalt = salt4,
                //    NormalizedUsername = "quoctrinh2004".ToUpperInvariant(),
                //    NormalizedEmail = "draogon10a3@gmail.com".ToUpperInvariant()
                //},
                //new User
                //{
                //    Id = 5,
                //    FullName = "Nguyễn Hữu Hùng",
                //    Username = "hungnguyen1998",
                //    Email = "n2h1706@gmail.com",
                //    RoleId = 3,
                //    PasswordHash = PasswordHelper.HashPassword("aptech", out string salt5),
                //    PasswordSalt = salt5,
                //    NormalizedUsername = "hungnguyen1998".ToUpperInvariant(),
                //    NormalizedEmail = "n2h1706@gmail.com".ToUpperInvariant()
                //}
                //new User
                //{
                //    Id = 6,
                //    FullName = "Đặng Kim Thi",
                //    Username = "thidk",
                //    Email = "thidk@fpt.edu.vn",
                //    RoleId = 2,
                //    PasswordHash = PasswordHelper.HashPassword("aptech", out string salt),
                //    PasswordSalt = salt,
                //    NormalizedUsername = "thidk".ToUpperInvariant(),
                //    NormalizedEmail = "thidk@fpt.edu.vn".ToUpperInvariant()
                //},
                //new User
                //{
                //    Id = 7,
                //    FullName = "Nguyễn Duy Hoàng",
                //    Username = "hoangnd",
                //    Email = "hoangnd@fpt.edu.vn",
                //    RoleId = 2,
                //    PasswordHash = PasswordHelper.HashPassword("aptech", out salt),
                //    PasswordSalt = salt,
                //    NormalizedUsername = "hoangnd".ToUpperInvariant(),
                //    NormalizedEmail = "hoangnd@fpt.edu.vn".ToUpperInvariant()
                //},
                //new User
                //{
                //    Id = 8,
                //    FullName = "Nguyễn Xuân Cường",
                //    Username = "cuongnx",
                //    Email = "cuongnx@fpt.edu.vn",
                //    RoleId = 2,
                //    PasswordHash = PasswordHelper.HashPassword("aptech", out salt),
                //    PasswordSalt = salt,
                //    NormalizedUsername = "cuongnx".ToUpperInvariant(),
                //    NormalizedEmail = "cuongnx@fpt.edu.vn".ToUpperInvariant()
                //},
                //new User
                //{
                //    Id = 9,
                //    FullName = "Vũ Hữu Phương",
                //    Username = "phuongvh",
                //    Email = "phuongvh@fpt.edu.vn",
                //    RoleId = 2,
                //    PasswordHash = PasswordHelper.HashPassword("aptech", out salt),
                //    PasswordSalt = salt,
                //    NormalizedUsername = "phuongvh".ToUpperInvariant(),
                //    NormalizedEmail = "phuongvh@fpt.edu.vn".ToUpperInvariant()
                //},
                //new User
                //{
                //    Id = 10,
                //    FullName = "Trịnh Ngọc Văn",
                //    Username = "vantn",
                //    Email = "vantn@fpt.edu.vn",
                //    RoleId = 2,
                //    PasswordHash = PasswordHelper.HashPassword("aptech", out salt),
                //    PasswordSalt = salt,
                //    NormalizedUsername = "vantn".ToUpperInvariant(),
                //    NormalizedEmail = "vantn@fpt.edu.vn".ToUpperInvariant()
                //},
                //new User
                //{
                //    Id = 11,
                //    FullName = "Nguyễn Tuân",
                //    Username = "tuann",
                //    Email = "tuann@fpt.edu.vn",
                //    RoleId = 2,
                //    PasswordHash = PasswordHelper.HashPassword("aptech", out salt),
                //    PasswordSalt = salt,
                //    NormalizedUsername = "tuann".ToUpperInvariant(),
                //    NormalizedEmail = "tuann@fpt.edu.vn".ToUpperInvariant()
                //},
                //new User
                //{
                //    Id = 12,
                //    FullName = "Trần Mạnh Trường",
                //    Username = "truongtm",
                //    Email = "truongtm@fpt.edu.vn",
                //    RoleId = 2,
                //    PasswordHash = PasswordHelper.HashPassword("aptech", out salt),
                //    PasswordSalt = salt,
                //    NormalizedUsername = "truongtm".ToUpperInvariant(),
                //    NormalizedEmail = "truongtm@fpt.edu.vn".ToUpperInvariant()
                //},
                //new User
                //{
                //    Id = 13,
                //    FullName = "Mai Thành Vinh",
                //    Username = "vinhmt",
                //    Email = "vinhmt@fpt.edu.vn",
                //    RoleId = 2,
                //    PasswordHash = PasswordHelper.HashPassword("aptech", out salt),
                //    PasswordSalt = salt,
                //    NormalizedUsername = "vinhmt".ToUpperInvariant(),
                //    NormalizedEmail = "vinhmt@fpt.edu.vn".ToUpperInvariant()
                //},
                //new User
                //{
                //    Id = 14,
                //    FullName = "Trần Hoàng Anh",
                //    Username = "anhth",
                //    Email = "anhth@fpt.edu.vn",
                //    RoleId = 2,
                //    PasswordHash = PasswordHelper.HashPassword("aptech", out salt),
                //    PasswordSalt = salt,
                //    NormalizedUsername = "anhth".ToUpperInvariant(),
                //    NormalizedEmail = "anhth@fpt.edu.vn".ToUpperInvariant()
                //},
                //new User
                //{
                //    Id = 15,
                //    FullName = "Man Ngọc Lam",
                //    Username = "lammn",
                //    Email = "lammn@fpt.edu.vn",
                //    RoleId = 2,
                //    PasswordHash = PasswordHelper.HashPassword("aptech", out salt),
                //    PasswordSalt = salt,
                //    NormalizedUsername = "lammn".ToUpperInvariant(),
                //    NormalizedEmail = "lammn@fpt.edu.vn"
                //}
                //new User
                //{
                //    Id = 16,
                //    FullName = "Lã Trọng Nghĩa",
                //    Username = "nghiala",
                //    Email = "leonghiacnn@gmail.com",
                //    RoleId = 3,
                //    PasswordHash = PasswordHelper.HashPassword("aptech", out var salt),
                //    PasswordSalt = salt,
                //    NormalizedUsername = "nghiala".ToUpperInvariant(),
                //    NormalizedEmail = "leonghiacnn@gmail.com".ToUpperInvariant()
                //}
                new User
                {
                    Id = 17,
                    FullName = "Admin",
                    Username = "admin",
                    Email = "admin@fpt.edu.vn",
                    RoleId = 1,
                    PasswordHash = PasswordHelper.HashPassword("aptech", out var salt),
                    PasswordSalt = salt,
                    NormalizedUsername = "admin".ToUpperInvariant(),
                    NormalizedEmail = "admin@fpt.edu.vn".ToUpperInvariant()
                }
                );
        }
    }
}
