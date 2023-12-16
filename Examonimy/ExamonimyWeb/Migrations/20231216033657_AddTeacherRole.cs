using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamonimyWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddTeacherRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Teacher" });

            //migrationBuilder.UpdateData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 1,
            //    columns: new[] { "Email", "FullName", "NormalizedEmail", "NormalizedUsername", "PasswordHash", "PasswordSalt", "Username" },
            //    values: new object[] { "hoatq@fpt.edu.vn", "Trịnh Quang Hòa", "HOATQ@FPT.EDU.VN", "HOATQ", "A31FFAEBC832702FDDE1D781A41E0159181E919109F1A2B1245D4AECED8F9915673BC1BC9B6BE2C04930E0A8B99931203AF5F377355B898E7D6442C15353BAEA", "5A23F5FEB3E7534A0EEB1B384E7E268A746BD29A499629EE8044CC3AA0EE42DCBACB05FCAC0DAB2A61CD2EB2C838C77E628B2A285B5CADCD70E9352DFD653E39", "hoatq" });

            //migrationBuilder.UpdateData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 2,
            //    columns: new[] { "PasswordHash", "PasswordSalt", "RoleId" },
            //    values: new object[] { "D463B4E5C254476B8246E9671C89E30E3DDE710518275B10368F7F418A52DB958024AECA1B9878083B9DA9EBB8DAC067976B0ED80F19E156B363F8B8ED557DFD", "BC0417983EE5B94E2F30EAFEDF7667E809F23F7E7583E1801A5787CEE5B0FC770DAEA5BD7833ECADEE75674A9B4B4BB9012DA4F9EBAFC67C8F0EF4B0E86D3590", 2 });

            //migrationBuilder.UpdateData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 3,
            //    columns: new[] { "PasswordHash", "PasswordSalt", "RoleId" },
            //    values: new object[] { "BAFA33CF240D100ABA2F10ABDCBE7AF1F5F8E06A473FB5FF9BB43CD91795D2DB57FB23CE2EBA23E96FC5BF8BF963B57D28992A5E62811B373FFDCB9DA1D3A6D7", "509D32D4945971B22954C4FB9775BE8A81419ECAF70F47607570E28EE9E4BCE985785227D78DFAA9E64C471F9D55ADC4F6F6E40720FC05005012F4083B1C3917", 2 });

            //migrationBuilder.UpdateData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 4,
            //    columns: new[] { "PasswordHash", "PasswordSalt" },
            //    values: new object[] { "3EC43588D09F1F93298613B944AC233E60167CBC29B6F65A19197CC8B6D95B78AC97D88CD56A93D90F046565D659F64F41414162DD9834B524DD058E12345DA7", "3C92C41F638A0113334A07E6AD6BDFBAE44A3C0DE0648B9A0397DB8028FC8370CA5C7C5C09BB4BB9C34AF0DB27ED511F3A8C90CD58DAAE0CA64A54ED92FC1ABC" });

            //migrationBuilder.UpdateData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 5,
            //    columns: new[] { "PasswordHash", "PasswordSalt" },
            //    values: new object[] { "182AB729B609186B72428A595DEC1F12B9EE0A450D6E76C32B60FDEF1F7BB62A2F11DB556923C4F2D7F6A5285F089FF8D5674C68B778F713ABF9B92594E90D96", "94817552C291ACBAB7964A9B1D8EBE6C642710ED0C764C62C9E1E78D226F7F09916D2302C727C98C54E4D3ACAB01ED8105D7CDEE9D3730840306626AC048E253" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3);

            //migrationBuilder.UpdateData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 1,
            //    columns: new[] { "Email", "FullName", "NormalizedEmail", "NormalizedUsername", "PasswordHash", "PasswordSalt", "Username" },
            //    values: new object[] { "leonghiacnn@gmail.com", "Lã Trọng Nghĩa", "LEONGHIACNN@GMAIL.COM", "NGHIALA1998", "391A2E69E9329F0A3AB2E3D62C3D64E8244DF930930F39C2622C7754261A4720CB700C29ACDD04E9E89AB16B8EB4E259322AC0301329EB87D5B2BCDE823D1F93", "AC3E6AED4BEC05912F01C3427A387E899E2AB635424DD5014F28F997B4917D761C838A9C94FD732C440ED635342B3BF607076ED621C745726AC44AB5856297F0", "nghiala1998" });

            //migrationBuilder.UpdateData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 2,
            //    columns: new[] { "PasswordHash", "PasswordSalt", "RoleId" },
            //    values: new object[] { "36AE25668DAED2238A99B4720199D450A3346146CB9340F3479F05ED5E0F744A4DB654211C0F36CB121F323AF0FAE4800343C46F1DB84768C638DF5A187B8A13", "536679D3AA38172A5AAF59E081F3F7E9553BC1310ECF50C8373A7AEDBDF14ACD2DB0A3A89CAFD61A682187051CBE6A97FCAD757FBAE0C9EEDE624504EDA9692A", 1 });

            //migrationBuilder.UpdateData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 3,
            //    columns: new[] { "PasswordHash", "PasswordSalt", "RoleId" },
            //    values: new object[] { "6317B1D7F11DA0E61045335C5C83D062112BA0EEE43219061EF2BC1541BCCD2852744022B1C296E714B49F777FDF84E51DB05B909E6502DB304321132A99BA76", "8D16ADB8FBAE825F2D8DA8CCF17FB0DC42DF1D86AFF8BFF00A316007A100381783D81F8C3EF02C3AF90BC328B6A76EEFB855085D20EFB50C896C7584E227A7B7", 1 });

            //migrationBuilder.UpdateData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 4,
            //    columns: new[] { "PasswordHash", "PasswordSalt" },
            //    values: new object[] { "B60B5C3A436010ACA88D81006A2364C14DABADC47B687F126ECA6BEB2CDC54FF836E170E647EE2850305FF73807B3554C3A43F1B398E8979E097286C2C5BBCE9", "583757789C9A553ED1059C48FD33F7D45F96E9C33105223A4E80E5D2BB7294517F566DFAE8FF5EC386C79F98472A89A3E0324E5C5D108CDC97817DCCAD5CC4EB" });

            //migrationBuilder.UpdateData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 5,
            //    columns: new[] { "PasswordHash", "PasswordSalt" },
            //    values: new object[] { "65D2AF359B7FE0FB96EEAB639162FDFE459EFD24EF5CB67A02F2977DCC97591F51EAE398F1944B7D48972B82A5990E4403B94B9E1D396F980D99B299B08CD25E", "992AD4C77398715DE957FBA0729BFBD157B3CD48153B02C7B954C90EE60895ED0A5F8CE79361FB67D18722F98042145D2D4C3FE08B36BD535E5956BAC3D06DCC" });
        }
    }
}
