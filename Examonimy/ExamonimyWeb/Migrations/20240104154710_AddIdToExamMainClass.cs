using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamonimyWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddIdToExamMainClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ExamMainClasses",
                table: "ExamMainClasses");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ExamMainClasses",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExamMainClasses",
                table: "ExamMainClasses",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ExamMainClasses_ExamId",
                table: "ExamMainClasses",
                column: "ExamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ExamMainClasses",
                table: "ExamMainClasses");

            migrationBuilder.DropIndex(
                name: "IX_ExamMainClasses_ExamId",
                table: "ExamMainClasses");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ExamMainClasses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExamMainClasses",
                table: "ExamMainClasses",
                columns: new[] { "ExamId", "MainClassId" });
        }
    }
}
