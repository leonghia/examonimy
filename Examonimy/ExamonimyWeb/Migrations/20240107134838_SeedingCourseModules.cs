using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExamonimyWeb.Migrations
{
    /// <inheritdoc />
    public partial class SeedingCourseModules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CourseModules",
                columns: new[] { "Id", "CourseId", "Name" },
                values: new object[,]
                {
                    { 1, 8, "Getting Starte with C#" },
                    { 2, 8, "Variables and Data Types in C#" },
                    { 3, 8, "Statements and Operators" },
                    { 4, 8, "Programming Constructs and Arrays" },
                    { 5, 8, "Classes and Methods" },
                    { 6, 8, "Inheritance and Polymorphism" },
                    { 7, 8, "Abstract Classes and Interfaces" },
                    { 8, 8, "Properties, Indexers and Record Types" },
                    { 9, 8, "Namespaces and Exception Handling" },
                    { 10, 8, "Events, Delegates and Collections" },
                    { 11, 8, "Generics and Iterators" },
                    { 12, 8, "Advanced Concepts in C#" },
                    { 13, 8, "New Features of C#" },
                    { 14, 8, ".NET Core Development and the Future" },
                    { 15, 8, "GUI/Desktop Apps with C#" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "CourseModules",
                keyColumn: "Id",
                keyValue: 15);
        }
    }
}
