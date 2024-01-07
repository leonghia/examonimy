using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamonimyWeb.Migrations
{
    /// <inheritdoc />
    public partial class CreateCourseModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 17);

            migrationBuilder.AddColumn<int>(
                name: "CourseModuleId",
                table: "Questions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CourseModules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseModules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseModules_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Admin");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_CourseModuleId",
                table: "Questions",
                column: "CourseModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseModules_CourseId",
                table: "CourseModules",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_CourseModules_CourseModuleId",
                table: "Questions",
                column: "CourseModuleId",
                principalTable: "CourseModules",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_CourseModules_CourseModuleId",
                table: "Questions");

            migrationBuilder.DropTable(
                name: "CourseModules");

            migrationBuilder.DropIndex(
                name: "IX_Questions_CourseModuleId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "CourseModuleId",
                table: "Questions");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Administrator");

            //migrationBuilder.InsertData(
            //    table: "Users",
            //    columns: new[] { "Id", "DateOfBirth", "Email", "FullName", "NormalizedEmail", "NormalizedUsername", "PasswordHash", "PasswordSalt", "ProfilePicture", "RefreshToken", "RefreshTokenExpiryTime", "RoleId", "Username" },
            //    values: new object[] { 17, null, "admin@fpt.edu.vn", "Admin", "ADMIN@FPT.EDU.VN", "ADMIN", "967D69A15416A26C6B70DA4D3EB13462FF0B0D146454056BFF8370A837F74DE3213D20072F5451A1E891E9D5EA2AA5789AC77BA8BDCC67935D8272344EC5C795", "BB8CFE541DDA2A377E5CF38D9B5E4D706FB52E1E2C30E72F4D659F55F90EB2380DAD2D1D93F1D3A6F31DBF4A132FDD7CD68B35ECDB74C4220860C8826D9B987E", "/images/examonimy-default-pfp.jpg", null, null, 1, "admin" });
        }
    }
}
