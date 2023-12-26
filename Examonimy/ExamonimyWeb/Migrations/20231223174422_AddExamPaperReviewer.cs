using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamonimyWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddExamPaperReviewer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExamPaperReviewer",
                columns: table => new
                {
                    ExamPaperId = table.Column<int>(type: "int", nullable: false),
                    ReviewersId = table.Column<int>(type: "int", nullable: false),
                    ReviewerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamPaperReviewer", x => new { x.ExamPaperId, x.ReviewersId });
                    table.ForeignKey(
                        name: "FK_ExamPaperReviewer_ExamPapers_ExamPaperId",
                        column: x => x.ExamPaperId,
                        principalTable: "ExamPapers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamPaperReviewer_Users_ReviewersId",
                        column: x => x.ReviewersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "25C4A05A401000EB67E805E8F49E3D460D8F67718D91FF034454CDBBD423D16913F10469FB22EF470C771AC4AAB437E5C1E47D8320AF8DD9F0718ABC970B283F", "5CF20C1370EEBB9235957D691D37352832A7EEA3C7CAF6D1AAD0957D7BF3A8AC1F7018BE1BF80EB0361791335B96CCC94A7E50F3DEFC93373AF0FB1AE5AAD63F" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "DC84980009F1AD7E7020B01AE451783D00D65F9FD6DDDF4EDE5D7CD7C7F791669B71AE24144748318FEC5319CFC7E8BE0DF7CBB9011F7E36C5C2BC09807B45D3", "16D8241490AA803057CFC65E332E8532291BA1B300E6E2DAF860D5B9DB2A2865F01CA312BFD74D8A3FC4ABCFB5E0FB23BF0100E99FB215876A8E2FD30F1B6712" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "E3B9B76300ED1CE7E97B14AFF40879DADF896C20B67DC1943A9597DC17AB0259F92AB7E3830355B061B4535DD6470703030D8A29BB54FBD5E47042763D9F25AC", "32080D3DE169B585829967B39D6A8B026209B8ABB6E51045BAAF9790F45950BCCF38E3101F7605392F03B53110D044B2547FAF6293B68D975016F14CBC1DA2BE" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "955FB5728BE748FEA60EF7BA4167BFF3BF3202C2018D36818CAE2B5642D45BCF67B4987BA4885E2DA7644D9F233F8BC5499085F94F87E352A7AF9B1DEDB050A9", "AF2E1E5706739014B8DAD59BC8DEB6B6ED009EC5F94961B042D65D9080646A58AA01899AE2D664C04F1B121B6FCF34480530C5702E38D11780B7FC33DB82DC31" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "69399730175D3EB09F19E3B607BDD4DBC272886B37E89EF0F9AE3CF7E701FCE3F97AACAF75EE8867F0A0F7C4DC31811186F16F0A08B4C90AA9FB616C08466461", "C572D1B49E5936DCA81A614C93E44DA977C596661D38B1422ED0C5A1665741CFECC9FD284FD8C3726E4D5154A1202745B88615ED2EDFB33B52BD1BD32665D825" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "DFEB56D8376A9B4FE5A1B6F077E9E80C1F2FFC167AB576EE7F083F1A1514E2ACA8D22CA30E15103E9DE7CF2044D361C4483D3AD7D2A73A03591984DFEE912676", "89B1BE6E25138FCC0B1FC6ADE752D5D7416641EFF917909A54C79880B205C8D4D30EF68CE668B8C84C3B5ED32856F458B3A6AD73B854F73C9365C971E8A7B95F" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "17405E85BCECEB113008F038B26B6E3CF465A4573FEA1B89F649E129CF8ED2407361407B7AE2302EA08618227F19290B4030B22FFAD614AFDFC9ED52C7178243", "66AF127C9F9487E35B93E0D455814E63593B4DBEF80851EC5FBD0C37B0BF256183B1836991D08A195B5D02F2E0070D466D6C77B0A65006B0941CE3B20119270C" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "EB7A71BBAC2BE531D75FCA89EB57597BBA1FF0E1C54CB3AC212CF8428150229D2604BCE7CDA1E662A52E397D44A5E08E5CB8D6585F568AAE291E3F084F8C527A", "B1A17649BE6477DFBE69569B9D74C62CE6F1F98532AC99641985D185371E7090C2CDB8D03BF0E981B34151E3B8052AE701966C3BEC7E3EC6EF27209C73B7FEE0" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "017D240304171EF7890A83F8CE9288FEEF7855BF6578BB8FAC801753FB41CB3EBE360EFA93FCCF1A03C2EFEBD9944AD65269506E84E6838C3120FE17C8399341", "C0A4F51912C4BCCDA2FAF624B4104A609914613A7491DC4389F3CC5F5BA91A0C25BFE9A863963652F5BEEC05D435D23C0FD7B136D5A340840CB1AA7134DC50E9" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "55A4DE6C427D623DA9FB8627DAD33CA32027DD0CDD0FCE9A96DC1C148C24AE1FD66BF4024655B465E313D6DCE14E4F2712D3E5B2EEB67E86E52AF07CDB83ADB6", "F37509B56DD1527C585A39E8CD0DBD96364977C1578AE6FE65244D278370E1C8F0AEC7645201C64587D6C56ABA5BA34EED2A4E362840CBA0F4A49E470300CEA8" });

            migrationBuilder.CreateIndex(
                name: "IX_ExamPaperReviewer_ReviewersId",
                table: "ExamPaperReviewer",
                column: "ReviewersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamPaperReviewer");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "8D6357081E261DAD8990FD2674FFB94644437A7ABB307E64CE340ECCA0F8F4BAE3FEBFA8B2DCA0664B0ECE3E03B3B1B2C0447CECD78277D8384A375C6347E4F2", "BE54736A9E5D832ADF0CD95F290B24080BD5233036AE99A72E3AD1435F46F45D92A971085970F6B3CE48031245FC946342669F4652626357ACF73AA215FB392C" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "91D4741E12572FDFF01A9E5D89475A31830A9E3431867A27283B588FA31A285A1D55C1D46D3735E6A79EE35AAA74FC08BDED88DF5467A5533A7792E350A98B5B", "BA970D043628C54C5DCE37C19E0CA099ABCB692D5B7AFABB1518E3F384DF44171B15957C37BE0A9E2DC49E6B8E3FEE103ADF55807BF4B432E86F8B34B14B45BB" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "58EAAB2B8626F8C8BE0AA0E455C5697CCABE2E48E870ABE93AFCD075B150465B7249D2556BB5D241F34F17DD97D841BBF0CD2E00884445EB7B9B04DB13D54335", "0209F9DEE74FCA9250249DA9025EBF28AEC5E71C0B9E031E6DC0AC8E64EDE0429AE2719EC64EBCEA2563B5C76C0F8CEC12D2397FACB2EF727CA23CAB1D445517" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "0145221F31056308B3380B0DDA8539F37DB212E2F6962FE3C197EA8B6CC4518176CECC046485A53EDAA6A8C98BA61F42FAE0F812EEB2CF9237F41F68D18C4D5E", "16B6AB44B905CDFF4C08E6AE5173B75DFFCB4D8E9185F793DF918A86913C7BDBD274CDD48EFE933A909A0C4FB9F7DC1EDB3D70A487964785D9DC1CD5AEE5A189" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "AB91CF12851404133C701FE1F60814AEA9398F13603412B5CCF9C314E490F59ED566F76F0A6D4A5DE5B01908AF4E89C9EA40807197CA9ADC3B78AFC7633AAB95", "B184DAEFAC87F28536A029DF70DB77995E1CF5994FBBD0EF0B631280353CC3DFE8BF6BEB715E31A5103C734F7635C746BD09F98C5197157A117C94D22DAAC378" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "A89C6B9A1200DBA42BCAE6C3BA79484A1183E483E11489C5E22BE146BB624A851309AEB16E24C8EC81553E8E2D4BADFC7C39B613549B8B97337964176BC9FCC0", "01887715D0DFE1C4C6EAC6DEC3FA0D3292175E05BFE1BAA1AD2FB15611D1D974E852557B7F08C6DF963D511FBB62DDF4484284DBAD9F3697AA0B03DB2AA648B9" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "BDE5C218BA1FA459941B18AE9A5D4C428C7B900197286B82CE5AFF6EF4B778036B78E9863B628B5A21EC424ED945AD0652844CC0CDAA57B145DDD77D90443F14", "721E7FAF6A9C4FC848F479CFD84FBB69E579B21D102046A6F6A9C07A34EBAAFF10F657E44ADEED5ED85E2A17E26BBBD728C85963925084D6690961CECD6AEF82" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "059E38C6F6D85EFF100CEB119FDB5CE35C17FF8852F9E0A29747C976289D49B29A9126F2106F514CD69B84EC7A4394EE9CFE0838EEA8CF91C461D5479F89B473", "0D385AA6773071D3117BB2045433F32E2DD4288BFEE8C9F79EF53B84A5F01660BA9E9276EE36CC988DEDEDCB33A9CB90302749EA7FE06CEC7CD652DBFE5EC5DE" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "287B5A2A10F1B089CE6108F35A7D72A6B05FE53F2535838E3C5B51F8E931CD6DD6FE75047E31B8CC6F1049094A780FB7DF4E9B55FC423F630FE3A3D45ADD3F7E", "47ABE2E6E3E25E46976878FBD54E119A32D1B6E7284001CC4805083ECDA8AB16B6C4447E9C3547EA6AB7E0B68685718E18A986DDF8E35DAF4B2F3BFEBD96B799" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "4420EA95AC96787557D324D85491158A7AC2503D2B36DB71E7CEBAFE14B20267B79A57360148A64EA94C52D3ED2F41FDF544ABE77CF9B73A7AC7D9DAC9F39493", "B8F5E08A4763DD4587674C411912C126B2DA94B21798FA3B588D8A0C348B31365148D5999C46C2F10440EDC5D87D478E936A99329DEECDBF965C21EF5C117911" });
        }
    }
}
