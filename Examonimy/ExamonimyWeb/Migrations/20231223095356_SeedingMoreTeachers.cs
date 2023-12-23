using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExamonimyWeb.Migrations
{
    /// <inheritdoc />
    public partial class SeedingMoreTeachers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateOfBirth", "Email", "FullName", "NormalizedEmail", "NormalizedUsername", "PasswordHash", "PasswordSalt", "ProfilePicture", "RefreshToken", "RefreshTokenExpiryTime", "RoleId", "Username" },
                values: new object[,]
                {
                    { 6, null, "thidk@fpt.edu.vn", "Đặng Kim Thi", "THIDK@FPT.EDU.VN", "THIDK", "8D6357081E261DAD8990FD2674FFB94644437A7ABB307E64CE340ECCA0F8F4BAE3FEBFA8B2DCA0664B0ECE3E03B3B1B2C0447CECD78277D8384A375C6347E4F2", "BE54736A9E5D832ADF0CD95F290B24080BD5233036AE99A72E3AD1435F46F45D92A971085970F6B3CE48031245FC946342669F4652626357ACF73AA215FB392C", "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg", null, null, 2, "thidk" },
                    { 7, null, "hoangnd@fpt.edu.vn", "Nguyễn Duy Hoàng", "HOANGND@FPT.EDU.VN", "HOANGND", "91D4741E12572FDFF01A9E5D89475A31830A9E3431867A27283B588FA31A285A1D55C1D46D3735E6A79EE35AAA74FC08BDED88DF5467A5533A7792E350A98B5B", "BA970D043628C54C5DCE37C19E0CA099ABCB692D5B7AFABB1518E3F384DF44171B15957C37BE0A9E2DC49E6B8E3FEE103ADF55807BF4B432E86F8B34B14B45BB", "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg", null, null, 2, "hoangnd" },
                    { 8, null, "cuongnx@fpt.edu.vn", "Nguyễn Xuân Cường", "CUONGNX@FPT.EDU.VN", "CUONGNX", "58EAAB2B8626F8C8BE0AA0E455C5697CCABE2E48E870ABE93AFCD075B150465B7249D2556BB5D241F34F17DD97D841BBF0CD2E00884445EB7B9B04DB13D54335", "0209F9DEE74FCA9250249DA9025EBF28AEC5E71C0B9E031E6DC0AC8E64EDE0429AE2719EC64EBCEA2563B5C76C0F8CEC12D2397FACB2EF727CA23CAB1D445517", "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg", null, null, 2, "cuongnx" },
                    { 9, null, "phuongvh@fpt.edu.vn", "Vũ Hữu Phương", "PHUONGVH@FPT.EDU.VN", "PHUONGVH", "0145221F31056308B3380B0DDA8539F37DB212E2F6962FE3C197EA8B6CC4518176CECC046485A53EDAA6A8C98BA61F42FAE0F812EEB2CF9237F41F68D18C4D5E", "16B6AB44B905CDFF4C08E6AE5173B75DFFCB4D8E9185F793DF918A86913C7BDBD274CDD48EFE933A909A0C4FB9F7DC1EDB3D70A487964785D9DC1CD5AEE5A189", "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg", null, null, 2, "phuongvh" },
                    { 10, null, "vantn@fpt.edu.vn", "Trịnh Ngọc Văn", "VANTN@FPT.EDU.VN", "VANTN", "AB91CF12851404133C701FE1F60814AEA9398F13603412B5CCF9C314E490F59ED566F76F0A6D4A5DE5B01908AF4E89C9EA40807197CA9ADC3B78AFC7633AAB95", "B184DAEFAC87F28536A029DF70DB77995E1CF5994FBBD0EF0B631280353CC3DFE8BF6BEB715E31A5103C734F7635C746BD09F98C5197157A117C94D22DAAC378", "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg", null, null, 2, "vantn" },
                    { 11, null, "tuann@fpt.edu.vn", "Nguyễn Tuân", "TUANN@FPT.EDU.VN", "TUANN", "A89C6B9A1200DBA42BCAE6C3BA79484A1183E483E11489C5E22BE146BB624A851309AEB16E24C8EC81553E8E2D4BADFC7C39B613549B8B97337964176BC9FCC0", "01887715D0DFE1C4C6EAC6DEC3FA0D3292175E05BFE1BAA1AD2FB15611D1D974E852557B7F08C6DF963D511FBB62DDF4484284DBAD9F3697AA0B03DB2AA648B9", "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg", null, null, 2, "tuann" },
                    { 12, null, "truongtm@fpt.edu.vn", "Trần Mạnh Trường", "TRUONGTM@FPT.EDU.VN", "TRUONGTM", "BDE5C218BA1FA459941B18AE9A5D4C428C7B900197286B82CE5AFF6EF4B778036B78E9863B628B5A21EC424ED945AD0652844CC0CDAA57B145DDD77D90443F14", "721E7FAF6A9C4FC848F479CFD84FBB69E579B21D102046A6F6A9C07A34EBAAFF10F657E44ADEED5ED85E2A17E26BBBD728C85963925084D6690961CECD6AEF82", "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg", null, null, 2, "truongtm" },
                    { 13, null, "vinhmt@fpt.edu.vn", "Mai Thành Vinh", "VINHMT@FPT.EDU.VN", "VINHMT", "059E38C6F6D85EFF100CEB119FDB5CE35C17FF8852F9E0A29747C976289D49B29A9126F2106F514CD69B84EC7A4394EE9CFE0838EEA8CF91C461D5479F89B473", "0D385AA6773071D3117BB2045433F32E2DD4288BFEE8C9F79EF53B84A5F01660BA9E9276EE36CC988DEDEDCB33A9CB90302749EA7FE06CEC7CD652DBFE5EC5DE", "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg", null, null, 2, "vinhmt" },
                    { 14, null, "anhth@fpt.edu.vn", "Trần Hoàng Anh", "ANHTH@FPT.EDU.VN", "ANHTH", "287B5A2A10F1B089CE6108F35A7D72A6B05FE53F2535838E3C5B51F8E931CD6DD6FE75047E31B8CC6F1049094A780FB7DF4E9B55FC423F630FE3A3D45ADD3F7E", "47ABE2E6E3E25E46976878FBD54E119A32D1B6E7284001CC4805083ECDA8AB16B6C4447E9C3547EA6AB7E0B68685718E18A986DDF8E35DAF4B2F3BFEBD96B799", "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg", null, null, 2, "anhth" },
                    { 15, null, "lammn@fpt.edu.vn", "Man Ngọc Lam", "lammn@fpt.edu.vn", "LAMMN", "4420EA95AC96787557D324D85491158A7AC2503D2B36DB71E7CEBAFE14B20267B79A57360148A64EA94C52D3ED2F41FDF544ABE77CF9B73A7AC7D9DAC9F39493", "B8F5E08A4763DD4587674C411912C126B2DA94B21798FA3B588D8A0C348B31365148D5999C46C2F10440EDC5D87D478E936A99329DEECDBF965C21EF5C117911", "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg", null, null, 2, "lammn" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 15);
        }
    }
}
