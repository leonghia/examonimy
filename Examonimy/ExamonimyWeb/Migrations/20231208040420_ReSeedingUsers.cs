using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExamonimyWeb.Migrations
{
    /// <inheritdoc />
    public partial class ReSeedingUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateOfBirth", "Email", "FullName", "NormalizedEmail", "NormalizedUsername", "PasswordHash", "PasswordSalt", "ProfilePicture", "RefreshToken", "RefreshTokenExpiryTime", "RoleId", "Username" },
                values: new object[,]
                {
                    { 1, null, "leonghiacnn@gmail.com", "Lã Trọng Nghĩa", "LEONGHIACNN@GMAIL.COM", "NGHIALA1998", "30788035CE4CECC9FD30989EF0BB1C8A6EE4117A5E81A58C8279741F11658972FF1DA418C9E47246FE1D28842CD2C2BCC97FF6DEBFE0810B41BC83A60B69B61C", "F750F943225284F0B66F486AE04EB7B99D23D8D0D906D185CB5E8C8416EF9AD6E36600C07B50F7183F6B78739DB4850E592BCABCEE250A7F0F69FAB9ECA9E1B4", "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg", null, null, 1, "nghiala1998" },
                    { 2, null, "phuctv1112004@gmail.com", "Trịnh Văn Phúc", "PHUCTV1112004@GMAIL.COM", "PHUCTRINH2004", "F42A342003065EA36D774F8CE053972E292522BE088BBE36B152D8BAFEC5B053171122C95E219EE9010C210D8695E3810D61BD2E1089ED0A5C12D8B901B6D03A", "75ED3CF3BB4815298063ED88862B527F5A5D269D32F038D7A714EA5A3B9E79207391FC65828E13C477628F802C69A5E907518FAC6348800970CA71F07053813E", "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg", null, null, 1, "phuctrinh2004" },
                    { 3, null, "nguyenhuongg1104@gmail.com", "Nguyễn Thị Hương", "NGUYENHUONGG1104@GMAIL.COM", "HUONGNGUYEN2004", "EA7077287A97C210B9EF6B067F421FD88ACDEA915E0137E85D1FBA0DC4367625972747A2C3C19C478C1EDE855703C71475EE122F33175326280E16ED8E8A9660", "731F6C70880009E5B4EE9A8E4B07A69FE48BA7D4B66A92A5380A18CFFC6B46C3B174398F5F6F159BB2BF4D93C39B12A8E19EB227157A7B59EC7E8A19C1722AAF", "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg", null, null, 1, "huongnguyen2004" },
                    { 4, null, "draogon10a3@gmail.com", "Trịnh Đình Quốc", "DRAOGON10A3@GMAIL.COM", "QUOCTRINH2004", "79F948A651438160390F7A4297BA7D9792FE574B6BA60CB63982E5BBEB83F40182C6B2289BF28E116BA2E0BEC4AC50B4983748BC543C9B2A562346C0DD805370", "18EE72B7DE82C308DC2BDBE183FA1BAC1BC86A843E3B270413F4A56F549C71F5897F12ED95EE45B942E2B658F451545A204853C149A655C2767E1D24F0100B7B", "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg", null, null, 2, "quoctrinh2004" },
                    { 5, null, "n2h1706@gmail.com", "Nguyễn Hữu Hùng", "N2H1706@GMAIL.COM", "HUNGNGUYEN1998", "402121B18374D3D1FE2E8E52D78C78DD4EA0CE8817F7CDBF952577017ECC0AC7E96492CA59FD5A54AA36982545C325FF5D3B188768DBCCF71FF55725C2A18C78", "C0DC676AF5C8C104A8039C6DCB9F6DE3E68AE152F41F9E05A88B3D817A2DC89BA30BE5D67CB38C32B211A62F770AB87E38AA2E5C8307E16463A8FB9E57160BD4", "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg", null, null, 2, "hungnguyen1998" }
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
