using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamonimyWeb.Migrations
{
    /// <inheritdoc />
    public partial class CreateExamPaperQuestionComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ExamPaperQuestion",
                table: "ExamPaperQuestion");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ExamPaperQuestion",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExamPaperQuestion",
                table: "ExamPaperQuestion",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ExamPaperQuestionComment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamPaperQuestionId = table.Column<int>(type: "int", nullable: false),
                    CommentedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CommenterId = table.Column<int>(type: "int", nullable: false),
                    ExamPaperQuestionId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamPaperQuestionComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamPaperQuestionComment_ExamPaperQuestion_ExamPaperQuestionId",
                        column: x => x.ExamPaperQuestionId,
                        principalTable: "ExamPaperQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamPaperQuestionComment_ExamPaperQuestion_ExamPaperQuestionId1",
                        column: x => x.ExamPaperQuestionId1,
                        principalTable: "ExamPaperQuestion",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExamPaperQuestionComment_Users_CommenterId",
                        column: x => x.CommenterId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExamPaperQuestion_ExamPaperId",
                table: "ExamPaperQuestion",
                column: "ExamPaperId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamPaperQuestionComment_CommenterId",
                table: "ExamPaperQuestionComment",
                column: "CommenterId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamPaperQuestionComment_ExamPaperQuestionId",
                table: "ExamPaperQuestionComment",
                column: "ExamPaperQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamPaperQuestionComment_ExamPaperQuestionId1",
                table: "ExamPaperQuestionComment",
                column: "ExamPaperQuestionId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamPaperQuestionComment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExamPaperQuestion",
                table: "ExamPaperQuestion");

            migrationBuilder.DropIndex(
                name: "IX_ExamPaperQuestion_ExamPaperId",
                table: "ExamPaperQuestion");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ExamPaperQuestion");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExamPaperQuestion",
                table: "ExamPaperQuestion",
                columns: new[] { "ExamPaperId", "QuestionId" });
        }
    }
}
