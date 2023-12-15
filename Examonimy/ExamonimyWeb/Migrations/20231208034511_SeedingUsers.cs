using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExamonimyWeb.Migrations
{
    /// <inheritdoc />
    public partial class SeedingUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateOfBirth", "Email", "FullName", "NormalizedEmail", "NormalizedUsername", "PasswordHash", "PasswordSalt", "RefreshToken", "RefreshTokenExpiryTime", "RoleId", "Username" },
                values: new object[,]
                {
                    { 1, null, "hoatq@fpt.edu.vn", "Trịnh Quang Hòa", "HOATQ@FPT.EDU.VN", "HOATQ", "46DF1A8022585F518C69A8E4FCDF04ECBCC6BD66E08A210F4859D74B7934917940304972C77D443798C06D28317602191791AF4C3A5FEDA57BFCCC6FC8FD8D8F", "A9CCC8FBA5860DCB36C12DE8DE19114456A960E2EAE54E8D9F716F9E0ABC3DC39A298CC7416AB032EBB7659887E829B41F233AFDE207D36675C6669B0BBFD597", null, null, 1, "nghiala1998" },
                    { 2, null, "phuctv1112004@gmail.com", "Trịnh Văn Phúc", "PHUCTV1112004@GMAIL.COM", "PHUCTRINH2004", "B824C8AA3CC70E588FB01B11D792A2A810AAA336A4BDB65867DEFFED95A4C3E48C21A2EB8B2BF9974A584BE707BBDF5C835CA821925CAA6B1CC3624D7B2348EB", "560964AD1D6AAB3F502FE75C0F56B94A588A7E6D62038B3B6BD715573491098A0EC925538EEC9AD6E1513F9FFA12FE83EDF40CCDF3B475894AF1316A0FE54B38", null, null, 2, "phuctrinh2004" },
                    { 3, null, "nguyenhuongg1104@gmail.com", "Nguyễn Thị Hương", "NGUYENHUONGG1104@GMAIL.COM", "HUONGNGUYEN2004", "AE18A73E0598675A443FD63A338398D341092F9BC9D544D9D7B827DC2D39E5004C43C52D33796132B5D4696219085B90FE339B2A5A8F871D02F795A95012973D", "1D35379B9FA8D681578D3E6237457CC4AA2810454DE6187A6E21D3E0AFAF6D00446D37EB893E5B58756FDC0D3F75094A50C49D3C05300806D3E3C7BF4F94AFC3", null, null, 2, "huongnguyen2004" },
                    { 4, null, "draogon10a3@gmail.com", "Trịnh Đình Quốc", "DRAOGON10A3@GMAIL.COM", "QUOCTRINH2004", "DA42D81363B8CB8896EFEAE07631C45172C96FB678971643BC5A139FC828E0F1CC1991CC3A9A1B4868AFEA3C7AFBCF999A6C5E29D8C4D1C1F82C3C16BA4DB2DC", "FA9EA39B25CCBEF11C04F6BC113C5DDC683339D738FDDE221FFD1A4DF82481FD8925C8F3A5CE62DC8C4A84FE9A4CDDA1E24332A8E7FEFF74E433855712E0AEA1", null, null, 2, "quoctrinh2004" },
                    { 5, null, "n2h1706@gmail.com", "Nguyễn Hữu Hùng", "N2H1706@GMAIL.COM", "HUNGNGUYEN1998", "6491089DD64109AC35B3B73E637F3475999508AE1F1C0A3DFC721C11DA9897779E08A2666D1576BFF7159D556AC9CB337C13196C66A69EC11A5648A1405A1AF9", "BCC0254B7A0C3B5995019D8ED439C90090AD13F1B17C555182CEEE6CF2255B52B146C470FF5C53578B50878E2894ACE810A5AA7EF58AA8F50DD9B8F2B7A41137", null, null, 2, "hungnguyen1998" }
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
