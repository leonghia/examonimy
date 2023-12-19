using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExamonimyWeb.Migrations
{
    /// <inheritdoc />
    public partial class ChangeStatusTypeToEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 1);

            //migrationBuilder.DeleteData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 2);

            //migrationBuilder.DeleteData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 3);

            //migrationBuilder.DeleteData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 4);

            //migrationBuilder.DeleteData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 5);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "ExamPapers",
                type: "int",
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Status",
                table: "ExamPapers",
                type: "tinyint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            //migrationBuilder.InsertData(
            //    table: "Users",
            //    columns: new[] { "Id", "DateOfBirth", "Email", "FullName", "NormalizedEmail", "NormalizedUsername", "PasswordHash", "PasswordSalt", "ProfilePicture", "RefreshToken", "RefreshTokenExpiryTime", "RoleId", "Username" },
            //    values: new object[,]
            //    {
            //        { 1, null, "hoatq@fpt.edu.vn", "Trịnh Quang Hòa", "HOATQ@FPT.EDU.VN", "HOATQ", "5D1A9867C3239D1641716A50CD119FD8EF9BDA5957EC33B7C791E1D5F2872DF74128EE9B4B0DE68C1EF04B1022E7238265BA6AF7667D84926C5033D5DF39B047", "919404853EEA9BCBED498C591C26A852BEC486B78713830BB1D8948F7A6264960EFBB37D5A599942874E6CAB534751E47DC08368D01E9895C10A5E45B81FBE17", "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg", null, null, 2, "hoatq" },
            //        { 2, null, "phuctv1112004@gmail.com", "Trịnh Văn Phúc", "PHUCTV1112004@GMAIL.COM", "PHUCTRINH2004", "705CFCDB0470A660A42F428B0A6B9A6C7EFFDABC079351E2BA9EBBFB2F612DA883114E538FC5D9EFE88AA81E32C095E897D1D1B1E78B8514B96969EC779E3FA2", "A38D00DE55BECACCE8A58CB78EE10478ABB53F0BCA21DC74681C63948D98DE30BC58D25DD5D7EFC9B30CE20E106420340A68BCD75DEB65963B90FBF87A2F1531", "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg", null, null, 3, "phuctrinh2004" },
            //        { 3, null, "nguyenhuongg1104@gmail.com", "Nguyễn Thị Hương", "NGUYENHUONGG1104@GMAIL.COM", "HUONGNGUYEN2004", "983709487C7401CDE242685F28C881116749E5C7A207243AB5E3EA6B39C87229A0B172E6CD0BB1D77ED15E541118C8F77858BB520DDB1947C0191D263A6DCADC", "2A66760E711A0376F56471E7F24C1568E4802E57FC28D8EB8C373ADBCFB4253FDE81E8A7A7A861389118E9C87995C6B85C9B112255F23110FD3037BB0F5738B2", "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg", null, null, 3, "huongnguyen2004" },
            //        { 4, null, "draogon10a3@gmail.com", "Trịnh Đình Quốc", "DRAOGON10A3@GMAIL.COM", "QUOCTRINH2004", "64BC3C92602AF9B3D1728A67B798661A2B47EDE7EACB8EBCF188E159568398D1724526CB95A4C9EABF204080D1E46EB1409248BB5ED4245B1FF3C1F1D14123C4", "5C7905717B2B064A4C57A0D95F5FE966DB7DF622A5C39DA476F08DBB238299ADE849796EB5BA342E1285DC906F55AB0AA9C97BF89CE6C8C0687C2FF7F18DDF7D", "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg", null, null, 3, "quoctrinh2004" },
            //        { 5, null, "n2h1706@gmail.com", "Nguyễn Hữu Hùng", "N2H1706@GMAIL.COM", "HUNGNGUYEN1998", "9838FA5F581B388206186C767A7E0B26CEE1B885A1F9A4B06AB9540D9568DC0633CA5B576DC187DD8602C1476F4C8DAA4656BC8983B568325F5014FD72403292", "DB972EDE695F17B5A8ED09E7CD454A8C588A23C48D8ADE49EB8732E5B16E9E103749D17B0670B1A9D379E8E36B1FABC2D85FB594442BE4DF30C6ED5866D50DDE", "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg", null, null, 3, "hungnguyen1998" }
            //    });
        }
    }
}
