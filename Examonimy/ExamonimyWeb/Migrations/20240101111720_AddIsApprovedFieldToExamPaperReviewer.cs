using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamonimyWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddIsApprovedFieldToExamPaperReviewer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "ExamPaperReviewer",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "ExamPaperReviewer");
        }
    }
}
