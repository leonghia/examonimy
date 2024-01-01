using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamonimyWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldCreatedAtForExamPaper : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ExamPapers",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ExamPapers");
        }
    }
}
