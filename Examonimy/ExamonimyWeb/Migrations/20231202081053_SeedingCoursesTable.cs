using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExamonimyWeb.Migrations
{
    /// <inheritdoc />
    public partial class SeedingCoursesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CourseCode", "Name" },
                values: new object[,]
                {
                    { 1, "LBEP", "Xây dựng logic và lập trình cơ bản" },
                    { 2, "AJS", "Angular" },
                    { 3, "DDD", "Thiết kế & Phát triển cơ sở dữ liệu (NCC module)" },
                    { 4, "HCJS", "HTML5, CSS, Javascript" },
                    { 5, "DMS", "Quản lý cơ sở dữ liệu với SQL Server" },
                    { 6, "JP1", "Lập trình Java I" },
                    { 7, "JP2", "Lập trình Java II" },
                    { 8, "APC#", "Lập trình C#" },
                    { 9, "PDLF", "Lập trình PHP với framework Laravel" },
                    { 10, "PIIT", "Các vấn đề chuyên môn về CNTT (NCC)" },
                    { 11, "ISA", "Phân tích hệ thống thông tin" },
                    { 12, "MLJ", "Ngôn ngữ markup và JSON" },
                    { 13, "ENJS", "Những điều cơ bản của NodeJS" },
                    { 14, "WDA", "Lập trình Web với ASP.NET MVC" },
                    { 15, "NSC", "An ninh mạng và mật mã (NCC)" },
                    { 16, "DMAWS", "Phát triển Microsoft Azure và Web services" },
                    { 17, "EJA", "Lĩnh vực việc làm mới nổi-SMAC" },
                    { 18, "AD", "Phát triển Agile (NCC)" },
                    { 19, "WCD", "Lập trình Web với Java" },
                    { 20, "IASF", "Tích hợp ứng dụng sử dụng Spring Framework" },
                    { 21, "EAD", "Phát triển ứng dụng doanh nghiệp bằng Java EE" },
                    { 22, "CSW", "Tạo dịch vụ cho Web" },
                    { 23, "IDP", "Giới thiệu về lập trình Dart" },
                    { 24, "ADFD", "Phát triển ứng dụng bằng Flutter và Dart" },
                    { 25, "CP", "Dự án máy tính" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 25);
        }
    }
}
