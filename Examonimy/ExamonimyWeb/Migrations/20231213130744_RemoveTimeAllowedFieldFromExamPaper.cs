using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamonimyWeb.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTimeAllowedFieldFromExamPaper : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeAllowedInMinutes",
                table: "ExamPapers");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "TimeAllowedInMinutes",
                table: "ExamPapers",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            //migrationBuilder.UpdateData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 1,
            //    columns: new[] { "PasswordHash", "PasswordSalt" },
            //    values: new object[] { "30788035CE4CECC9FD30989EF0BB1C8A6EE4117A5E81A58C8279741F11658972FF1DA418C9E47246FE1D28842CD2C2BCC97FF6DEBFE0810B41BC83A60B69B61C", "F750F943225284F0B66F486AE04EB7B99D23D8D0D906D185CB5E8C8416EF9AD6E36600C07B50F7183F6B78739DB4850E592BCABCEE250A7F0F69FAB9ECA9E1B4" });

            //migrationBuilder.UpdateData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 2,
            //    columns: new[] { "PasswordHash", "PasswordSalt" },
            //    values: new object[] { "F42A342003065EA36D774F8CE053972E292522BE088BBE36B152D8BAFEC5B053171122C95E219EE9010C210D8695E3810D61BD2E1089ED0A5C12D8B901B6D03A", "75ED3CF3BB4815298063ED88862B527F5A5D269D32F038D7A714EA5A3B9E79207391FC65828E13C477628F802C69A5E907518FAC6348800970CA71F07053813E" });

            //migrationBuilder.UpdateData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 3,
            //    columns: new[] { "PasswordHash", "PasswordSalt" },
            //    values: new object[] { "EA7077287A97C210B9EF6B067F421FD88ACDEA915E0137E85D1FBA0DC4367625972747A2C3C19C478C1EDE855703C71475EE122F33175326280E16ED8E8A9660", "731F6C70880009E5B4EE9A8E4B07A69FE48BA7D4B66A92A5380A18CFFC6B46C3B174398F5F6F159BB2BF4D93C39B12A8E19EB227157A7B59EC7E8A19C1722AAF" });

            //migrationBuilder.UpdateData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 4,
            //    columns: new[] { "PasswordHash", "PasswordSalt" },
            //    values: new object[] { "79F948A651438160390F7A4297BA7D9792FE574B6BA60CB63982E5BBEB83F40182C6B2289BF28E116BA2E0BEC4AC50B4983748BC543C9B2A562346C0DD805370", "18EE72B7DE82C308DC2BDBE183FA1BAC1BC86A843E3B270413F4A56F549C71F5897F12ED95EE45B942E2B658F451545A204853C149A655C2767E1D24F0100B7B" });

            //migrationBuilder.UpdateData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 5,
            //    columns: new[] { "PasswordHash", "PasswordSalt" },
            //    values: new object[] { "402121B18374D3D1FE2E8E52D78C78DD4EA0CE8817F7CDBF952577017ECC0AC7E96492CA59FD5A54AA36982545C325FF5D3B188768DBCCF71FF55725C2A18C78", "C0DC676AF5C8C104A8039C6DCB9F6DE3E68AE152F41F9E05A88B3D817A2DC89BA30BE5D67CB38C32B211A62F770AB87E38AA2E5C8307E16463A8FB9E57160BD4" });
        }
    }
}
