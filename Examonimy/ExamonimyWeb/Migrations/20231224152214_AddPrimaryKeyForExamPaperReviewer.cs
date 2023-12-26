using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamonimyWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddPrimaryKeyForExamPaperReviewer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ExamPaperReviewer",
                table: "ExamPaperReviewer");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ExamPaperReviewer",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExamPaperReviewer",
                table: "ExamPaperReviewer",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ExamPaperReviewer_ExamPaperId",
                table: "ExamPaperReviewer",
                column: "ExamPaperId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ExamPaperReviewer",
                table: "ExamPaperReviewer");

            migrationBuilder.DropIndex(
                name: "IX_ExamPaperReviewer_ExamPaperId",
                table: "ExamPaperReviewer");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ExamPaperReviewer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExamPaperReviewer",
                table: "ExamPaperReviewer",
                columns: new[] { "ExamPaperId", "ReviewerId" });
        }
    }
}
