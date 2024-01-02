using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamonimyWeb.Migrations
{
    /// <inheritdoc />
    public partial class CreateExam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MainClassId = table.Column<int>(type: "int", nullable: false),
                    ExamPaperId = table.Column<int>(type: "int", nullable: false),
                    From = table.Column<DateTime>(type: "datetime2", nullable: false),
                    To = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exams_ExamPapers_ExamPaperId",
                        column: x => x.ExamPaperId,
                        principalTable: "ExamPapers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Exams_MainClasses_MainClassId",
                        column: x => x.MainClassId,
                        principalTable: "MainClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exams_ExamPaperId",
                table: "Exams",
                column: "ExamPaperId");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_MainClassId",
                table: "Exams",
                column: "MainClassId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exams");
        }
    }
}
