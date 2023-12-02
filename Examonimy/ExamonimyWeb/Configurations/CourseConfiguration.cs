using ExamonimyWeb.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExamonimyWeb.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasData(
                new Course
                {
                    Id = 1,
                    Name = "Xây dựng logic và lập trình cơ bản",
                    CourseCode = "LBEP"
                },
                new Course
                {
                    Id = 2,
                    Name = "Angular",
                    CourseCode = "AJS"
                },
                new Course
                {
                    Id = 3,
                    Name = "Thiết kế & Phát triển cơ sở dữ liệu (NCC module)",
                    CourseCode = "DDD"
                },
                new Course
                {
                    Id = 4,
                    Name = "HTML5, CSS, Javascript",
                    CourseCode = "HCJS"
                },
                new Course
                {
                    Id = 5,
                    Name = "Quản lý cơ sở dữ liệu với SQL Server",
                    CourseCode = "DMS"
                },
                new Course
                {   Id = 6,
                    Name = "Lập trình Java I",
                    CourseCode = "JP1"
                },
                new Course
                {
                    Id = 7,
                    Name = "Lập trình Java II",
                    CourseCode = "JP2"
                },
                new Course
                {
                    Id = 8,
                    Name = "Lập trình C#",
                    CourseCode = "APC#"
                },
                new Course
                {
                    Id = 9,
                    Name = "Lập trình PHP với framework Laravel",
                    CourseCode = "PDLF"
                },
                new Course
                {
                    Id = 10,
                    Name = "Các vấn đề chuyên môn về CNTT (NCC)",
                    CourseCode = "PIIT"
                },
                new Course
                {
                    Id = 11,
                    Name = "Phân tích hệ thống thông tin",
                    CourseCode = "ISA"
                },
                new Course
                {
                    Id = 12,
                    Name = "Ngôn ngữ markup và JSON",
                    CourseCode = "MLJ"
                },
                new Course
                {
                    Id = 13,
                    Name = "Những điều cơ bản của NodeJS",
                    CourseCode = "ENJS"
                },
                new Course
                {
                    Id = 14,
                    Name = "Lập trình Web với ASP.NET MVC",
                    CourseCode = "WDA"
                },
                new Course
                {
                    Id = 15,
                    Name = "An ninh mạng và mật mã (NCC)",
                    CourseCode = "NSC"
                },
                new Course
                {
                    Id = 16,
                    Name = "Phát triển Microsoft Azure và Web services",
                    CourseCode = "DMAWS"
                },
                new Course
                {
                    Id = 17,
                    Name = "Lĩnh vực việc làm mới nổi-SMAC",
                    CourseCode = "EJA"
                },
                new Course
                {
                    Id = 18,
                    Name = "Phát triển Agile (NCC)",
                    CourseCode = "AD"
                },
                new Course
                {
                    Id = 19,
                    Name = "Lập trình Web với Java",
                    CourseCode = "WCD"
                },
                new Course
                {
                    Id = 20,
                    Name = "Tích hợp ứng dụng sử dụng Spring Framework",
                    CourseCode = "IASF"
                },
                new Course
                {
                    Id = 21,
                    Name = "Phát triển ứng dụng doanh nghiệp bằng Java EE",
                    CourseCode = "EAD"
                },
                new Course
                {
                    Id = 22,
                    Name = "Tạo dịch vụ cho Web",
                    CourseCode = "CSW"
                },
                new Course
                {
                    Id = 23,
                    Name = "Giới thiệu về lập trình Dart",
                    CourseCode = "IDP"
                },
                new Course
                {
                    Id = 24,
                    Name = "Phát triển ứng dụng bằng Flutter và Dart",
                    CourseCode = "ADFD"
                },
                new Course
                {
                    Id = 25,
                    Name = "Dự án máy tính",
                    CourseCode = "CP"
                }
                );
        }
    }
}
