using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamonimyWeb.Migrations
{
    /// <inheritdoc />
    public partial class ReAddExamPaperQuestionId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExamPaperQuestionId",
                table: "ExamPaperQuestionComment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ExamPaperQuestionComment_ExamPaperQuestionId",
                table: "ExamPaperQuestionComment",
                column: "ExamPaperQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamPaperQuestionComment_ExamPaperQuestion_ExamPaperQuestionId",
                table: "ExamPaperQuestionComment",
                column: "ExamPaperQuestionId",
                principalTable: "ExamPaperQuestion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamPaperQuestionComment_ExamPaperQuestion_ExamPaperQuestionId",
                table: "ExamPaperQuestionComment");

            migrationBuilder.DropIndex(
                name: "IX_ExamPaperQuestionComment_ExamPaperQuestionId",
                table: "ExamPaperQuestionComment");

            migrationBuilder.DropColumn(
                name: "ExamPaperQuestionId",
                table: "ExamPaperQuestionComment");
        }
    }
}
