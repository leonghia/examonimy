﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamonimyWeb.Migrations
{
    /// <inheritdoc />
    public partial class CreateExamMainClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_MainClasses_MainClassId",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_Exams_MainClassId",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "MainClassId",
                table: "Exams");

            migrationBuilder.CreateTable(
                name: "ExamMainClasses",
                columns: table => new
                {
                    ExamId = table.Column<int>(type: "int", nullable: false),
                    MainClassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamMainClasses", x => new { x.ExamId, x.MainClassId });
                    table.ForeignKey(
                        name: "FK_ExamMainClasses_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamMainClasses_MainClasses_MainClassId",
                        column: x => x.MainClassId,
                        principalTable: "MainClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExamMainClasses_MainClassId",
                table: "ExamMainClasses",
                column: "MainClassId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamMainClasses");

            migrationBuilder.AddColumn<int>(
                name: "MainClassId",
                table: "Exams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Exams_MainClassId",
                table: "Exams",
                column: "MainClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_MainClasses_MainClassId",
                table: "Exams",
                column: "MainClassId",
                principalTable: "MainClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
