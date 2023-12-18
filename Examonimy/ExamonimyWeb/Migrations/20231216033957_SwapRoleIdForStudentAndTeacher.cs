using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExamonimyWeb.Migrations
{
    /// <inheritdoc />
    public partial class SwapRoleIdForStudentAndTeacher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Teacher");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Student");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Student");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Teacher");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateOfBirth", "Email", "FullName", "NormalizedEmail", "NormalizedUsername", "PasswordHash", "PasswordSalt", "ProfilePicture", "RefreshToken", "RefreshTokenExpiryTime", "RoleId", "Username" },
                values: new object[,]
                {
                    { 1, null, "hoatq@fpt.edu.vn", "Trịnh Quang Hòa", "HOATQ@FPT.EDU.VN", "HOATQ", "A31FFAEBC832702FDDE1D781A41E0159181E919109F1A2B1245D4AECED8F9915673BC1BC9B6BE2C04930E0A8B99931203AF5F377355B898E7D6442C15353BAEA", "5A23F5FEB3E7534A0EEB1B384E7E268A746BD29A499629EE8044CC3AA0EE42DCBACB05FCAC0DAB2A61CD2EB2C838C77E628B2A285B5CADCD70E9352DFD653E39", "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg", null, null, 1, "hoatq" },
                    { 2, null, "phuctv1112004@gmail.com", "Trịnh Văn Phúc", "PHUCTV1112004@GMAIL.COM", "PHUCTRINH2004", "D463B4E5C254476B8246E9671C89E30E3DDE710518275B10368F7F418A52DB958024AECA1B9878083B9DA9EBB8DAC067976B0ED80F19E156B363F8B8ED557DFD", "BC0417983EE5B94E2F30EAFEDF7667E809F23F7E7583E1801A5787CEE5B0FC770DAEA5BD7833ECADEE75674A9B4B4BB9012DA4F9EBAFC67C8F0EF4B0E86D3590", "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg", null, null, 2, "phuctrinh2004" },
                    { 3, null, "nguyenhuongg1104@gmail.com", "Nguyễn Thị Hương", "NGUYENHUONGG1104@GMAIL.COM", "HUONGNGUYEN2004", "BAFA33CF240D100ABA2F10ABDCBE7AF1F5F8E06A473FB5FF9BB43CD91795D2DB57FB23CE2EBA23E96FC5BF8BF963B57D28992A5E62811B373FFDCB9DA1D3A6D7", "509D32D4945971B22954C4FB9775BE8A81419ECAF70F47607570E28EE9E4BCE985785227D78DFAA9E64C471F9D55ADC4F6F6E40720FC05005012F4083B1C3917", "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg", null, null, 2, "huongnguyen2004" },
                    { 4, null, "draogon10a3@gmail.com", "Trịnh Đình Quốc", "DRAOGON10A3@GMAIL.COM", "QUOCTRINH2004", "3EC43588D09F1F93298613B944AC233E60167CBC29B6F65A19197CC8B6D95B78AC97D88CD56A93D90F046565D659F64F41414162DD9834B524DD058E12345DA7", "3C92C41F638A0113334A07E6AD6BDFBAE44A3C0DE0648B9A0397DB8028FC8370CA5C7C5C09BB4BB9C34AF0DB27ED511F3A8C90CD58DAAE0CA64A54ED92FC1ABC", "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg", null, null, 2, "quoctrinh2004" },
                    { 5, null, "n2h1706@gmail.com", "Nguyễn Hữu Hùng", "N2H1706@GMAIL.COM", "HUNGNGUYEN1998", "182AB729B609186B72428A595DEC1F12B9EE0A450D6E76C32B60FDEF1F7BB62A2F11DB556923C4F2D7F6A5285F089FF8D5674C68B778F713ABF9B92594E90D96", "94817552C291ACBAB7964A9B1D8EBE6C642710ED0C764C62C9E1E78D226F7F09916D2302C727C98C54E4D3ACAB01ED8105D7CDEE9D3730840306626AC048E253", "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg", null, null, 2, "hungnguyen1998" }
                });
        }
    }
}
