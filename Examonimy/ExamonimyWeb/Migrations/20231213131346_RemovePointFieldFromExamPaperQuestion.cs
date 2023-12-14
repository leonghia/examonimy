using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamonimyWeb.Migrations
{
    /// <inheritdoc />
    public partial class RemovePointFieldFromExamPaperQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Point",
                table: "ExamPaperQuestion");

            //migrationBuilder.UpdateData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 1,
            //    columns: new[] { "PasswordHash", "PasswordSalt" },
            //    values: new object[] { "391A2E69E9329F0A3AB2E3D62C3D64E8244DF930930F39C2622C7754261A4720CB700C29ACDD04E9E89AB16B8EB4E259322AC0301329EB87D5B2BCDE823D1F93", "AC3E6AED4BEC05912F01C3427A387E899E2AB635424DD5014F28F997B4917D761C838A9C94FD732C440ED635342B3BF607076ED621C745726AC44AB5856297F0" });

            //migrationBuilder.UpdateData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 2,
            //    columns: new[] { "PasswordHash", "PasswordSalt" },
            //    values: new object[] { "36AE25668DAED2238A99B4720199D450A3346146CB9340F3479F05ED5E0F744A4DB654211C0F36CB121F323AF0FAE4800343C46F1DB84768C638DF5A187B8A13", "536679D3AA38172A5AAF59E081F3F7E9553BC1310ECF50C8373A7AEDBDF14ACD2DB0A3A89CAFD61A682187051CBE6A97FCAD757FBAE0C9EEDE624504EDA9692A" });

            //migrationBuilder.UpdateData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 3,
            //    columns: new[] { "PasswordHash", "PasswordSalt" },
            //    values: new object[] { "6317B1D7F11DA0E61045335C5C83D062112BA0EEE43219061EF2BC1541BCCD2852744022B1C296E714B49F777FDF84E51DB05B909E6502DB304321132A99BA76", "8D16ADB8FBAE825F2D8DA8CCF17FB0DC42DF1D86AFF8BFF00A316007A100381783D81F8C3EF02C3AF90BC328B6A76EEFB855085D20EFB50C896C7584E227A7B7" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Point",
                table: "ExamPaperQuestion",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            //migrationBuilder.UpdateData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 1,
            //    columns: new[] { "PasswordHash", "PasswordSalt" },
            //    values: new object[] { "EA1B050B74CB060DD00D9D2B36DDBC83CB8C3D4C7B9EBC71066F55B7A7FBB47AB7399A988A7119459A00CE92D7FF8FF5FA032E00028A6CE96ED20316B1EA1C41", "3D86133280376FA16F3AABC820E766F3CB4103558FF1C3C079182D4B6D99590AA185241E9A7038D43C741464C7F3935E7AC4647BD7A914BA02012DCF723721AD" });

            //migrationBuilder.UpdateData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 2,
            //    columns: new[] { "PasswordHash", "PasswordSalt" },
            //    values: new object[] { "EC5D2BA102F11EDA38DAE5B32BB089670850850CFBA596C775CBCC2D549670DA66E0BE6C7CC42010AE329732F439B7731B83C923F4733842FC836DCED08F5119", "B31025E03A52C0DBEE0A59137336F18FC1AD2BF15AFD7E6B820D11049537CDD52303CDE78B8D9EE5E443B7980AC5FB0434B64581EC24EFDE5064F90527BAA02F" });

            //migrationBuilder.UpdateData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 3,
            //    columns: new[] { "PasswordHash", "PasswordSalt" },
            //    values: new object[] { "13EC295D1987A41F92866C29F66E9A35B75C642862E12416F7DF8BAF344C829F7C536C94D4220C8E86463E46E8189358AA31FF23B1D4D2B69562C856F99D6980", "CD1A789F06A8386394F823E0D1648FE00C7990761B10612D6E0347272E0AA57254CDA8389B1F59B5AF27EE7C874D4D9D5BAD0EE1A1B7CD4EB9AC1B3E694571D9" });

            //migrationBuilder.UpdateData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 4,
            //    columns: new[] { "PasswordHash", "PasswordSalt" },
            //    values: new object[] { "BA332EC1C25BE2CA96EB569940C20E28F51930404D1EB24F7C8CEA49464209E3955B85B87EB9F0902FEC750535647A1B3E15E7534CBB1B705290EBA6875C93A8", "B2FFFE4BCBDC51F98864A95EA7F4FD076B53DF9C89F761094EF1A96A3A6B13E027DB8ACAE5624DB68075465BEE117D9E4ABC84B6344C041582B72857D7BEF440" });

            //migrationBuilder.UpdateData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 5,
            //    columns: new[] { "PasswordHash", "PasswordSalt" },
            //    values: new object[] { "85003C11C9DB8C44D3AB69389F0C9FA22B501A4229ADEBCD824E7DE835CB6B056F4C5FB905403370099F001E132C5F33C53110A08D5F1EED3B99B37320E1227E", "8F794F05716A60EB26E9DE6D59ADF428F29FF2B891824580AAD21C8E06922B40BEC810BC34021E039AFD899CE4F42069A90F093D178FD4F9A026A7FE0367BDC9" });
        }
    }
}
