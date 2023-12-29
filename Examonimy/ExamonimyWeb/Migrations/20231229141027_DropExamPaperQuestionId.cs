using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamonimyWeb.Migrations
{
    /// <inheritdoc />
    public partial class DropExamPaperQuestionId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamPaperQuestionComment_ExamPaperQuestion_ExamPaperQuestionId",
                table: "ExamPaperQuestionComment");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamPaperQuestionComment_ExamPaperQuestion_ExamPaperQuestionId1",
                table: "ExamPaperQuestionComment");

            migrationBuilder.DropIndex(
                name: "IX_ExamPaperQuestionComment_ExamPaperQuestionId",
                table: "ExamPaperQuestionComment");

            migrationBuilder.DropIndex(
                name: "IX_ExamPaperQuestionComment_ExamPaperQuestionId1",
                table: "ExamPaperQuestionComment");

            migrationBuilder.DropColumn(
                name: "ExamPaperQuestionId",
                table: "ExamPaperQuestionComment");

            migrationBuilder.DropColumn(
                name: "ExamPaperQuestionId1",
                table: "ExamPaperQuestionComment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExamPaperQuestionId",
                table: "ExamPaperQuestionComment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExamPaperQuestionId1",
                table: "ExamPaperQuestionComment",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExamPaperQuestionComment_ExamPaperQuestionId",
                table: "ExamPaperQuestionComment",
                column: "ExamPaperQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamPaperQuestionComment_ExamPaperQuestionId1",
                table: "ExamPaperQuestionComment",
                column: "ExamPaperQuestionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamPaperQuestionComment_ExamPaperQuestion_ExamPaperQuestionId",
                table: "ExamPaperQuestionComment",
                column: "ExamPaperQuestionId",
                principalTable: "ExamPaperQuestion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamPaperQuestionComment_ExamPaperQuestion_ExamPaperQuestionId1",
                table: "ExamPaperQuestionComment",
                column: "ExamPaperQuestionId1",
                principalTable: "ExamPaperQuestion",
                principalColumn: "Id");
        }
    }
}
