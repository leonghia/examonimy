using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExamonimyWeb.Migrations
{
    /// <inheritdoc />
    public partial class SeedingStudents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 16);

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "UserId", "MainClassId", "RollNumber" },
                values: new object[,]
                {
                    { 2, 1, "TH2209059" },
                    { 3, 1, "TH2209079" },
                    { 4, 1, "TH2209080" },
                    { 5, 1, "TH2209065" },
                    { 16, 1, "TH2209053" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "UserId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "UserId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "UserId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "UserId",
                keyValue: 16);

            //migrationBuilder.InsertData(
            //    table: "Users",
            //    columns: new[] { "Id", "DateOfBirth", "Email", "FullName", "NormalizedEmail", "NormalizedUsername", "PasswordHash", "PasswordSalt", "ProfilePicture", "RefreshToken", "RefreshTokenExpiryTime", "RoleId", "Username" },
            //    values: new object[] { 16, null, "leonghiacnn@gmail.com", "Lã Trọng Nghĩa", "LEONGHIACNN@GMAIL.COM", "NGHIALA", "113A985434C47C1313E94842A4B438ED1111431C177C347B03A876D7C6D90390BEBCBEF4EAB0A1EE3E54A0F2A02170B221056BA7D4321C334CB9AC63517203FC", "5C785F369C7EDBD2AE6934EE0B14FF9F3E9CF56B82093F8F6C77A507FFE71A36C4C7EEACBEC3DB1C06ACF4484BC0DF03D5D6D6D4C8E337EABFD15BA6B0BC4FB0", "~/images/examonimy-default-pfp.jpg", null, null, 3, "nghiala" });
        }
    }
}
