using ExamonimyWeb.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamonimyWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusFieldToExamPaper : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "ExamPapers",
                type: "tinyint",
                nullable: true,
                defaultValue: ExamPaperStatus.Pending);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "ExamPapers");

            //migrationBuilder.UpdateData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 1,
            //    columns: new[] { "PasswordHash", "PasswordSalt" },
            //    values: new object[] { "3DE4B9595AB74AD7812DE0A961E12034C27DE2389246507D66972BB68E4C4AF5C7ACFFEE31F2BB4964F4D744E9D730AC47C6DFB53BE9D2C5863D07BEDF49207C", "8CEEBAC27561894CBEC7BD790F66F070433EB7D67815E7DD79E70C23D0CECA4F50872701E70C52B085D8CF2EA13B13C7CE77EC005F45EE44884E14180EAB6101" });

            //migrationBuilder.UpdateData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 2,
            //    columns: new[] { "PasswordHash", "PasswordSalt" },
            //    values: new object[] { "FFE89A4976DADBB643B988DD320A89FBA90186E67185C65014674150312E2EF142DC70128500EBAAF193E6DBBABC30BDF2C13B468AE04B70CC573AEA93EA1CBC", "478823C8496A845A84F7169983F2274768410B044B534624C5455847E59B4DEA4BC30B3E1214278A88D57ED8D587E7933BC3E272560E50E9BF79374BEA5E10A7" });

            //migrationBuilder.UpdateData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 3,
            //    columns: new[] { "PasswordHash", "PasswordSalt" },
            //    values: new object[] { "3F454F2A5B1DFC75E9857FBC161248D0B253DF34D6A6A0438AB5221A9BCC9ABB7CE694B45844335996E566A3099DCB95D49A026ACF3DE8B4BF5861B82437964E", "B63C8F247FBDA298C5ACDDD4D4A04CFC3443B68E0C24125152171A8FDDEDF7D267A32F5673B7A4C35D586A9C9249D53BA2903365FADA567C81F3C7D167417D25" });

            //migrationBuilder.UpdateData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 4,
            //    columns: new[] { "PasswordHash", "PasswordSalt" },
            //    values: new object[] { "B625F65BF141FD751A825DDD5F30191BC991E151080FFD936CB5AF068C8E139B39C7790116EA28A1D2607860026020417B5940203B33547B8AE5D14C3C40EE5E", "D176E4990D830E111546DDDAB84BB4BA162E0E9330FFF7C68DB58B75B662EA10B03ADB5A48EF644E2ED514FE669EF7D4049731016B01914FDF463D3147CCF3B4" });

            //migrationBuilder.UpdateData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 5,
            //    columns: new[] { "PasswordHash", "PasswordSalt" },
            //    values: new object[] { "63D07A66A6A2E4E21DD596E7CDB3BF41A98AB650D126A711DA3A0964BA4F2E5E44D6E2BDE5E7CFCF8E8EC3E140AE696F7B78EE8ED1E8CDC1A71ABCDFC7E74718", "72DD616EFAB58FBF1CD612E190B2D1BBC3B7D70D7E75872DFA07C47E7A01884A0063F3418CF4483C8BBC86DF2C617B057BBA6187B3E5148D9540F6D170BE4823" });
        }
    }
}
