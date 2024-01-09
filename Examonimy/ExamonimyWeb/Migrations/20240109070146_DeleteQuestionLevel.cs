using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExamonimyWeb.Migrations
{
    /// <inheritdoc />
    public partial class DeleteQuestionLevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_QuestionLevels_QuestionLevelId",
                table: "Questions");

            migrationBuilder.DropTable(
                name: "QuestionLevels");

            migrationBuilder.DropIndex(
                name: "IX_Questions_QuestionLevelId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "QuestionLevelId",
                table: "Questions");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "ExamPapers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuestionLevelId",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "ExamPapers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "QuestionLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionLevels", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "QuestionLevels",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Nhận biết" },
                    { 2, "Thông hiểu" },
                    { 3, "Vận dụng" },
                    { 4, "Vận dụng cao" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuestionLevelId",
                table: "Questions",
                column: "QuestionLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_QuestionLevels_QuestionLevelId",
                table: "Questions",
                column: "QuestionLevelId",
                principalTable: "QuestionLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
