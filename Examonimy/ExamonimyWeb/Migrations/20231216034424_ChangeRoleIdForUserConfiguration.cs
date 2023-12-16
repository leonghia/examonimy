using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExamonimyWeb.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRoleIdForUserConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateOfBirth", "Email", "FullName", "NormalizedEmail", "NormalizedUsername", "PasswordHash", "PasswordSalt", "ProfilePicture", "RefreshToken", "RefreshTokenExpiryTime", "RoleId", "Username" },
                values: new object[,]
                {
                    { 1, null, "hoatq@fpt.edu.vn", "Trịnh Quang Hòa", "HOATQ@FPT.EDU.VN", "HOATQ", "3DE4B9595AB74AD7812DE0A961E12034C27DE2389246507D66972BB68E4C4AF5C7ACFFEE31F2BB4964F4D744E9D730AC47C6DFB53BE9D2C5863D07BEDF49207C", "8CEEBAC27561894CBEC7BD790F66F070433EB7D67815E7DD79E70C23D0CECA4F50872701E70C52B085D8CF2EA13B13C7CE77EC005F45EE44884E14180EAB6101", "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg", null, null, 2, "hoatq" },
                    { 2, null, "phuctv1112004@gmail.com", "Trịnh Văn Phúc", "PHUCTV1112004@GMAIL.COM", "PHUCTRINH2004", "FFE89A4976DADBB643B988DD320A89FBA90186E67185C65014674150312E2EF142DC70128500EBAAF193E6DBBABC30BDF2C13B468AE04B70CC573AEA93EA1CBC", "478823C8496A845A84F7169983F2274768410B044B534624C5455847E59B4DEA4BC30B3E1214278A88D57ED8D587E7933BC3E272560E50E9BF79374BEA5E10A7", "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg", null, null, 3, "phuctrinh2004" },
                    { 3, null, "nguyenhuongg1104@gmail.com", "Nguyễn Thị Hương", "NGUYENHUONGG1104@GMAIL.COM", "HUONGNGUYEN2004", "3F454F2A5B1DFC75E9857FBC161248D0B253DF34D6A6A0438AB5221A9BCC9ABB7CE694B45844335996E566A3099DCB95D49A026ACF3DE8B4BF5861B82437964E", "B63C8F247FBDA298C5ACDDD4D4A04CFC3443B68E0C24125152171A8FDDEDF7D267A32F5673B7A4C35D586A9C9249D53BA2903365FADA567C81F3C7D167417D25", "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg", null, null, 3, "huongnguyen2004" },
                    { 4, null, "draogon10a3@gmail.com", "Trịnh Đình Quốc", "DRAOGON10A3@GMAIL.COM", "QUOCTRINH2004", "B625F65BF141FD751A825DDD5F30191BC991E151080FFD936CB5AF068C8E139B39C7790116EA28A1D2607860026020417B5940203B33547B8AE5D14C3C40EE5E", "D176E4990D830E111546DDDAB84BB4BA162E0E9330FFF7C68DB58B75B662EA10B03ADB5A48EF644E2ED514FE669EF7D4049731016B01914FDF463D3147CCF3B4", "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg", null, null, 3, "quoctrinh2004" },
                    { 5, null, "n2h1706@gmail.com", "Nguyễn Hữu Hùng", "N2H1706@GMAIL.COM", "HUNGNGUYEN1998", "63D07A66A6A2E4E21DD596E7CDB3BF41A98AB650D126A711DA3A0964BA4F2E5E44D6E2BDE5E7CFCF8E8EC3E140AE696F7B78EE8ED1E8CDC1A71ABCDFC7E74718", "72DD616EFAB58FBF1CD612E190B2D1BBC3B7D70D7E75872DFA07C47E7A01884A0063F3418CF4483C8BBC86DF2C617B057BBA6187B3E5148D9540F6D170BE4823", "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg", null, null, 3, "hungnguyen1998" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
