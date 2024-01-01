using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamonimyWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddReviewStatusToExamPaperReviewer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "ExamPaperReviewer");

            migrationBuilder.AddColumn<int>(
                name: "ReviewStatus",
                table: "ExamPaperReviewer",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReviewStatus",
                table: "ExamPaperReviewer");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "ExamPaperReviewer",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
