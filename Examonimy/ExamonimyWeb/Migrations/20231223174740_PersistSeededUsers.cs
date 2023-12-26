using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExamonimyWeb.Migrations
{
    /// <inheritdoc />
    public partial class PersistSeededUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 6);

            //migrationBuilder.DeleteData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 7);

            //migrationBuilder.DeleteData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 8);

            //migrationBuilder.DeleteData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 9);

            //migrationBuilder.DeleteData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 10);

            //migrationBuilder.DeleteData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 11);

            //migrationBuilder.DeleteData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 12);

            //migrationBuilder.DeleteData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 13);

            //migrationBuilder.DeleteData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 14);

            //migrationBuilder.DeleteData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: 15);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.InsertData(
            //    table: "Users",
            //    columns: new[] { "Id", "DateOfBirth", "Email", "FullName", "NormalizedEmail", "NormalizedUsername", "PasswordHash", "PasswordSalt", "ProfilePicture", "RefreshToken", "RefreshTokenExpiryTime", "RoleId", "Username" },
            //    values: new object[,]
            //    {
            //        { 6, null, "thidk@fpt.edu.vn", "Đặng Kim Thi", "THIDK@FPT.EDU.VN", "THIDK", "25C4A05A401000EB67E805E8F49E3D460D8F67718D91FF034454CDBBD423D16913F10469FB22EF470C771AC4AAB437E5C1E47D8320AF8DD9F0718ABC970B283F", "5CF20C1370EEBB9235957D691D37352832A7EEA3C7CAF6D1AAD0957D7BF3A8AC1F7018BE1BF80EB0361791335B96CCC94A7E50F3DEFC93373AF0FB1AE5AAD63F", "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg", null, null, 2, "thidk" },
            //        { 7, null, "hoangnd@fpt.edu.vn", "Nguyễn Duy Hoàng", "HOANGND@FPT.EDU.VN", "HOANGND", "DC84980009F1AD7E7020B01AE451783D00D65F9FD6DDDF4EDE5D7CD7C7F791669B71AE24144748318FEC5319CFC7E8BE0DF7CBB9011F7E36C5C2BC09807B45D3", "16D8241490AA803057CFC65E332E8532291BA1B300E6E2DAF860D5B9DB2A2865F01CA312BFD74D8A3FC4ABCFB5E0FB23BF0100E99FB215876A8E2FD30F1B6712", "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg", null, null, 2, "hoangnd" },
            //        { 8, null, "cuongnx@fpt.edu.vn", "Nguyễn Xuân Cường", "CUONGNX@FPT.EDU.VN", "CUONGNX", "E3B9B76300ED1CE7E97B14AFF40879DADF896C20B67DC1943A9597DC17AB0259F92AB7E3830355B061B4535DD6470703030D8A29BB54FBD5E47042763D9F25AC", "32080D3DE169B585829967B39D6A8B026209B8ABB6E51045BAAF9790F45950BCCF38E3101F7605392F03B53110D044B2547FAF6293B68D975016F14CBC1DA2BE", "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg", null, null, 2, "cuongnx" },
            //        { 9, null, "phuongvh@fpt.edu.vn", "Vũ Hữu Phương", "PHUONGVH@FPT.EDU.VN", "PHUONGVH", "955FB5728BE748FEA60EF7BA4167BFF3BF3202C2018D36818CAE2B5642D45BCF67B4987BA4885E2DA7644D9F233F8BC5499085F94F87E352A7AF9B1DEDB050A9", "AF2E1E5706739014B8DAD59BC8DEB6B6ED009EC5F94961B042D65D9080646A58AA01899AE2D664C04F1B121B6FCF34480530C5702E38D11780B7FC33DB82DC31", "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg", null, null, 2, "phuongvh" },
            //        { 10, null, "vantn@fpt.edu.vn", "Trịnh Ngọc Văn", "VANTN@FPT.EDU.VN", "VANTN", "69399730175D3EB09F19E3B607BDD4DBC272886B37E89EF0F9AE3CF7E701FCE3F97AACAF75EE8867F0A0F7C4DC31811186F16F0A08B4C90AA9FB616C08466461", "C572D1B49E5936DCA81A614C93E44DA977C596661D38B1422ED0C5A1665741CFECC9FD284FD8C3726E4D5154A1202745B88615ED2EDFB33B52BD1BD32665D825", "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg", null, null, 2, "vantn" },
            //        { 11, null, "tuann@fpt.edu.vn", "Nguyễn Tuân", "TUANN@FPT.EDU.VN", "TUANN", "DFEB56D8376A9B4FE5A1B6F077E9E80C1F2FFC167AB576EE7F083F1A1514E2ACA8D22CA30E15103E9DE7CF2044D361C4483D3AD7D2A73A03591984DFEE912676", "89B1BE6E25138FCC0B1FC6ADE752D5D7416641EFF917909A54C79880B205C8D4D30EF68CE668B8C84C3B5ED32856F458B3A6AD73B854F73C9365C971E8A7B95F", "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg", null, null, 2, "tuann" },
            //        { 12, null, "truongtm@fpt.edu.vn", "Trần Mạnh Trường", "TRUONGTM@FPT.EDU.VN", "TRUONGTM", "17405E85BCECEB113008F038B26B6E3CF465A4573FEA1B89F649E129CF8ED2407361407B7AE2302EA08618227F19290B4030B22FFAD614AFDFC9ED52C7178243", "66AF127C9F9487E35B93E0D455814E63593B4DBEF80851EC5FBD0C37B0BF256183B1836991D08A195B5D02F2E0070D466D6C77B0A65006B0941CE3B20119270C", "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg", null, null, 2, "truongtm" },
            //        { 13, null, "vinhmt@fpt.edu.vn", "Mai Thành Vinh", "VINHMT@FPT.EDU.VN", "VINHMT", "EB7A71BBAC2BE531D75FCA89EB57597BBA1FF0E1C54CB3AC212CF8428150229D2604BCE7CDA1E662A52E397D44A5E08E5CB8D6585F568AAE291E3F084F8C527A", "B1A17649BE6477DFBE69569B9D74C62CE6F1F98532AC99641985D185371E7090C2CDB8D03BF0E981B34151E3B8052AE701966C3BEC7E3EC6EF27209C73B7FEE0", "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg", null, null, 2, "vinhmt" },
            //        { 14, null, "anhth@fpt.edu.vn", "Trần Hoàng Anh", "ANHTH@FPT.EDU.VN", "ANHTH", "017D240304171EF7890A83F8CE9288FEEF7855BF6578BB8FAC801753FB41CB3EBE360EFA93FCCF1A03C2EFEBD9944AD65269506E84E6838C3120FE17C8399341", "C0A4F51912C4BCCDA2FAF624B4104A609914613A7491DC4389F3CC5F5BA91A0C25BFE9A863963652F5BEEC05D435D23C0FD7B136D5A340840CB1AA7134DC50E9", "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg", null, null, 2, "anhth" },
            //        { 15, null, "lammn@fpt.edu.vn", "Man Ngọc Lam", "lammn@fpt.edu.vn", "LAMMN", "55A4DE6C427D623DA9FB8627DAD33CA32027DD0CDD0FCE9A96DC1C148C24AE1FD66BF4024655B465E313D6DCE14E4F2712D3E5B2EEB67E86E52AF07CDB83ADB6", "F37509B56DD1527C585A39E8CD0DBD96364977C1578AE6FE65244D278370E1C8F0AEC7645201C64587D6C56ABA5BA34EED2A4E362840CBA0F4A49E470300CEA8", "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg", null, null, 2, "lammn" }
            //    });
        }
    }
}
