using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamonimyWeb.Migrations
{
    /// <inheritdoc />
    public partial class SetDefaultValueForStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.UpdateData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 1,
            //    columns: new[] { "PasswordHash", "PasswordSalt" },
            //    values: new object[] { "5D1A9867C3239D1641716A50CD119FD8EF9BDA5957EC33B7C791E1D5F2872DF74128EE9B4B0DE68C1EF04B1022E7238265BA6AF7667D84926C5033D5DF39B047", "919404853EEA9BCBED498C591C26A852BEC486B78713830BB1D8948F7A6264960EFBB37D5A599942874E6CAB534751E47DC08368D01E9895C10A5E45B81FBE17" });

            //migrationBuilder.UpdateData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 2,
            //    columns: new[] { "PasswordHash", "PasswordSalt" },
            //    values: new object[] { "705CFCDB0470A660A42F428B0A6B9A6C7EFFDABC079351E2BA9EBBFB2F612DA883114E538FC5D9EFE88AA81E32C095E897D1D1B1E78B8514B96969EC779E3FA2", "A38D00DE55BECACCE8A58CB78EE10478ABB53F0BCA21DC74681C63948D98DE30BC58D25DD5D7EFC9B30CE20E106420340A68BCD75DEB65963B90FBF87A2F1531" });

            //migrationBuilder.UpdateData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 3,
            //    columns: new[] { "PasswordHash", "PasswordSalt" },
            //    values: new object[] { "983709487C7401CDE242685F28C881116749E5C7A207243AB5E3EA6B39C87229A0B172E6CD0BB1D77ED15E541118C8F77858BB520DDB1947C0191D263A6DCADC", "2A66760E711A0376F56471E7F24C1568E4802E57FC28D8EB8C373ADBCFB4253FDE81E8A7A7A861389118E9C87995C6B85C9B112255F23110FD3037BB0F5738B2" });

            //migrationBuilder.UpdateData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 4,
            //    columns: new[] { "PasswordHash", "PasswordSalt" },
            //    values: new object[] { "64BC3C92602AF9B3D1728A67B798661A2B47EDE7EACB8EBCF188E159568398D1724526CB95A4C9EABF204080D1E46EB1409248BB5ED4245B1FF3C1F1D14123C4", "5C7905717B2B064A4C57A0D95F5FE966DB7DF622A5C39DA476F08DBB238299ADE849796EB5BA342E1285DC906F55AB0AA9C97BF89CE6C8C0687C2FF7F18DDF7D" });

            //migrationBuilder.UpdateData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 5,
            //    columns: new[] { "PasswordHash", "PasswordSalt" },
            //    values: new object[] { "9838FA5F581B388206186C767A7E0B26CEE1B885A1F9A4B06AB9540D9568DC0633CA5B576DC187DD8602C1476F4C8DAA4656BC8983B568325F5014FD72403292", "DB972EDE695F17B5A8ED09E7CD454A8C588A23C48D8ADE49EB8732E5B16E9E103749D17B0670B1A9D379E8E36B1FABC2D85FB594442BE4DF30C6ED5866D50DDE" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.UpdateData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 1,
            //    columns: new[] { "PasswordHash", "PasswordSalt" },
            //    values: new object[] { "EF98F763AFCCB642CE9EA167A07701B84FEA104427E4E42B961FB82DC8A8DEFA0F6989FF69287FC641014623462046F4B8327DA99967A6F0BFFD059AC16C71D6", "E827195B62C05F02839542FF5ACE07240D015847A0F147696201F4ACEECF55CC8343D2240A09EF81A16BE068EE8A2C2B9D1892A2170FC2E655D060283F18B930" });

            //migrationBuilder.UpdateData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 2,
            //    columns: new[] { "PasswordHash", "PasswordSalt" },
            //    values: new object[] { "1610D9FE60125247A4C57AAF514D5888098AF5F9DA1A33ECB809E925E8E4A39A52AD87021F97E2DD3484D013B351C61B0C137530D90C8F7C4DB6850C5AB683AC", "FB741BD2DB6E93EB24CD95A3661FF3C4D73D56BBE2774FA369419EE041CE5F41BB812793D4C77A3B82B18F27019ABDF22C8EE119D51A446ACBB32FAE7FB4B951" });

            //migrationBuilder.UpdateData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 3,
            //    columns: new[] { "PasswordHash", "PasswordSalt" },
            //    values: new object[] { "6CF96FD9D3F289228AC53A190C44DC4A9A5364E160F4C6867D3EDF851132E5C6BE6417453B69954C7B695DBFF052316FEEF087D3C444B4E810E8F158D4458C93", "94156C9478951CAEE75D404AE453FC93B59FA2723AB4E28CBAD92599A5B54F12D99E1D8C4BDE9A1086D688D5B4FA99404CDBD6584A07E30FE1D72E05585CBF72" });

            //migrationBuilder.UpdateData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 4,
            //    columns: new[] { "PasswordHash", "PasswordSalt" },
            //    values: new object[] { "C5ED2680B41E58A870AB9CFF9A19B07C2F184DDED51BC2F8ABEBC0C393FEAF3E48C853BE892E52D462ED7D858AA4241731961B17D91446DABF40FA10B220E4E4", "71FE59CF9232AAA7EBADCE3E9F7E1776D3E3FA094690A50C9BC5F7D54638712B5E6D9DDC2032981456360C5B6EEEB7ABEF280E4D50D48A3536261A714A956D46" });

            //migrationBuilder.UpdateData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 5,
            //    columns: new[] { "PasswordHash", "PasswordSalt" },
            //    values: new object[] { "5B7E15DD3C17EEE49A11113652C2E2B60E3CF96D10CD4ACAE817DAF47BC3AB4EAAF685DCF87CCCA622459230BED196C4BBE91C85C9306B131C2DBB36167C0824", "86C6CC8ACEC97E96D133F3C4A28727BE6249C4D1C8FB6D4B6753586BC944D9F3C80F61B795484B1334043A3C268F1696A24F013A9DDF35DCC0080D1EA843553B" });
        }
    }
}
