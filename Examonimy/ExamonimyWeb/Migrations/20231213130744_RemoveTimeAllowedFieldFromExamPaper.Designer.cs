﻿// <auto-generated />
using System;
using ExamonimyWeb.DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ExamonimyWeb.Migrations
{
    [DbContext(typeof(ExamonimyContext))]
    [Migration("20231213130744_RemoveTimeAllowedFieldFromExamPaper")]
    partial class RemoveTimeAllowedFieldFromExamPaper
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ExamonimyWeb.Entities.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CourseCode")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CourseCode = "LBEP",
                            Name = "Xây dựng logic và lập trình cơ bản"
                        },
                        new
                        {
                            Id = 2,
                            CourseCode = "AJS",
                            Name = "Angular"
                        },
                        new
                        {
                            Id = 3,
                            CourseCode = "DDD",
                            Name = "Thiết kế & Phát triển cơ sở dữ liệu (NCC module)"
                        },
                        new
                        {
                            Id = 4,
                            CourseCode = "HCJS",
                            Name = "HTML5, CSS, Javascript"
                        },
                        new
                        {
                            Id = 5,
                            CourseCode = "DMS",
                            Name = "Quản lý cơ sở dữ liệu với SQL Server"
                        },
                        new
                        {
                            Id = 6,
                            CourseCode = "JP1",
                            Name = "Lập trình Java I"
                        },
                        new
                        {
                            Id = 7,
                            CourseCode = "JP2",
                            Name = "Lập trình Java II"
                        },
                        new
                        {
                            Id = 8,
                            CourseCode = "APC#",
                            Name = "Lập trình C#"
                        },
                        new
                        {
                            Id = 9,
                            CourseCode = "PDLF",
                            Name = "Lập trình PHP với framework Laravel"
                        },
                        new
                        {
                            Id = 10,
                            CourseCode = "PIIT",
                            Name = "Các vấn đề chuyên môn về CNTT (NCC)"
                        },
                        new
                        {
                            Id = 11,
                            CourseCode = "ISA",
                            Name = "Phân tích hệ thống thông tin"
                        },
                        new
                        {
                            Id = 12,
                            CourseCode = "MLJ",
                            Name = "Ngôn ngữ markup và JSON"
                        },
                        new
                        {
                            Id = 13,
                            CourseCode = "ENJS",
                            Name = "Những điều cơ bản của NodeJS"
                        },
                        new
                        {
                            Id = 14,
                            CourseCode = "WDA",
                            Name = "Lập trình Web với ASP.NET MVC"
                        },
                        new
                        {
                            Id = 15,
                            CourseCode = "NSC",
                            Name = "An ninh mạng và mật mã (NCC)"
                        },
                        new
                        {
                            Id = 16,
                            CourseCode = "DMAWS",
                            Name = "Phát triển Microsoft Azure và Web services"
                        },
                        new
                        {
                            Id = 17,
                            CourseCode = "EJA",
                            Name = "Lĩnh vực việc làm mới nổi-SMAC"
                        },
                        new
                        {
                            Id = 18,
                            CourseCode = "AD",
                            Name = "Phát triển Agile (NCC)"
                        },
                        new
                        {
                            Id = 19,
                            CourseCode = "WCD",
                            Name = "Lập trình Web với Java"
                        },
                        new
                        {
                            Id = 20,
                            CourseCode = "IASF",
                            Name = "Tích hợp ứng dụng sử dụng Spring Framework"
                        },
                        new
                        {
                            Id = 21,
                            CourseCode = "EAD",
                            Name = "Phát triển ứng dụng doanh nghiệp bằng Java EE"
                        },
                        new
                        {
                            Id = 22,
                            CourseCode = "CSW",
                            Name = "Tạo dịch vụ cho Web"
                        },
                        new
                        {
                            Id = 23,
                            CourseCode = "IDP",
                            Name = "Giới thiệu về lập trình Dart"
                        },
                        new
                        {
                            Id = 24,
                            CourseCode = "ADFD",
                            Name = "Phát triển ứng dụng bằng Flutter và Dart"
                        },
                        new
                        {
                            Id = 25,
                            CourseCode = "CP",
                            Name = "Dự án máy tính"
                        });
                });

            modelBuilder.Entity("ExamonimyWeb.Entities.ExamPaper", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("ExamPaperCode")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("CourseId");

                    b.ToTable("ExamPapers");
                });

            modelBuilder.Entity("ExamonimyWeb.Entities.ExamPaperQuestion", b =>
                {
                    b.Property<int>("ExamPaperId")
                        .HasColumnType("int");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<byte>("Number")
                        .HasColumnType("tinyint");

                    b.Property<float>("Point")
                        .HasColumnType("real");

                    b.HasKey("ExamPaperId", "QuestionId");

                    b.HasIndex("QuestionId");

                    b.ToTable("ExamPaperQuestion");
                });

            modelBuilder.Entity("ExamonimyWeb.Entities.FillInBlankQuestion", b =>
                {
                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<string>("CorrectAnswers")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("QuestionId");

                    b.ToTable("FillInBlankQuestions");
                });

            modelBuilder.Entity("ExamonimyWeb.Entities.MultipleChoiceQuestionWithMultipleCorrectAnswers", b =>
                {
                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<string>("ChoiceA")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChoiceB")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChoiceC")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChoiceD")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CorrectAnswers")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)");

                    b.HasKey("QuestionId");

                    b.ToTable("MultipleChoiceQuestionsWithMultipleCorrectAnswers");
                });

            modelBuilder.Entity("ExamonimyWeb.Entities.MultipleChoiceQuestionWithOneCorrectAnswer", b =>
                {
                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<string>("ChoiceA")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChoiceB")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChoiceC")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChoiceD")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("CorrectAnswer")
                        .HasColumnType("tinyint");

                    b.HasKey("QuestionId");

                    b.ToTable("MultipleChoiceQuestionsWithOneCorrectAnswer");
                });

            modelBuilder.Entity("ExamonimyWeb.Entities.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("QuestionContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuestionLevelId")
                        .HasColumnType("int");

                    b.Property<int>("QuestionTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("CourseId");

                    b.HasIndex("QuestionLevelId");

                    b.HasIndex("QuestionTypeId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("ExamonimyWeb.Entities.QuestionLevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.ToTable("QuestionLevels");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Nhận biết"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Thông hiểu"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Vận dụng"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Vận dụng cao"
                        });
                });

            modelBuilder.Entity("ExamonimyWeb.Entities.QuestionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.ToTable("QuestionTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Trắc nghiệm một đáp án đúng"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Trắc nghiệm nhiều đáp án đúng"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Đúng - Sai"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Trả lời ngắn"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Điền vào chỗ trống"
                        });
                });

            modelBuilder.Entity("ExamonimyWeb.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Administrator"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Student"
                        });
                });

            modelBuilder.Entity("ExamonimyWeb.Entities.ShortAnswerQuestion", b =>
                {
                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<string>("CorrectAnswer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("QuestionId");

                    b.ToTable("ShortAnswerQuestions");
                });

            modelBuilder.Entity("ExamonimyWeb.Entities.TrueFalseQuestion", b =>
                {
                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<bool>("CorrectAnswer")
                        .HasColumnType("bit");

                    b.HasKey("QuestionId");

                    b.ToTable("TrueFalseQuestions");
                });

            modelBuilder.Entity("ExamonimyWeb.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(320)
                        .HasColumnType("nvarchar(320)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("NormalizedEmail")
                        .IsRequired()
                        .HasMaxLength(320)
                        .HasColumnType("nvarchar(320)");

                    b.Property<string>("NormalizedUsername")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime?>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("RoleId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "leonghiacnn@gmail.com",
                            FullName = "Lã Trọng Nghĩa",
                            NormalizedEmail = "LEONGHIACNN@GMAIL.COM",
                            NormalizedUsername = "NGHIALA1998",
                            PasswordHash = "EA1B050B74CB060DD00D9D2B36DDBC83CB8C3D4C7B9EBC71066F55B7A7FBB47AB7399A988A7119459A00CE92D7FF8FF5FA032E00028A6CE96ED20316B1EA1C41",
                            PasswordSalt = "3D86133280376FA16F3AABC820E766F3CB4103558FF1C3C079182D4B6D99590AA185241E9A7038D43C741464C7F3935E7AC4647BD7A914BA02012DCF723721AD",
                            ProfilePicture = "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg",
                            RoleId = 1,
                            Username = "nghiala1998"
                        },
                        new
                        {
                            Id = 2,
                            Email = "phuctv1112004@gmail.com",
                            FullName = "Trịnh Văn Phúc",
                            NormalizedEmail = "PHUCTV1112004@GMAIL.COM",
                            NormalizedUsername = "PHUCTRINH2004",
                            PasswordHash = "EC5D2BA102F11EDA38DAE5B32BB089670850850CFBA596C775CBCC2D549670DA66E0BE6C7CC42010AE329732F439B7731B83C923F4733842FC836DCED08F5119",
                            PasswordSalt = "B31025E03A52C0DBEE0A59137336F18FC1AD2BF15AFD7E6B820D11049537CDD52303CDE78B8D9EE5E443B7980AC5FB0434B64581EC24EFDE5064F90527BAA02F",
                            ProfilePicture = "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg",
                            RoleId = 1,
                            Username = "phuctrinh2004"
                        },
                        new
                        {
                            Id = 3,
                            Email = "nguyenhuongg1104@gmail.com",
                            FullName = "Nguyễn Thị Hương",
                            NormalizedEmail = "NGUYENHUONGG1104@GMAIL.COM",
                            NormalizedUsername = "HUONGNGUYEN2004",
                            PasswordHash = "13EC295D1987A41F92866C29F66E9A35B75C642862E12416F7DF8BAF344C829F7C536C94D4220C8E86463E46E8189358AA31FF23B1D4D2B69562C856F99D6980",
                            PasswordSalt = "CD1A789F06A8386394F823E0D1648FE00C7990761B10612D6E0347272E0AA57254CDA8389B1F59B5AF27EE7C874D4D9D5BAD0EE1A1B7CD4EB9AC1B3E694571D9",
                            ProfilePicture = "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg",
                            RoleId = 1,
                            Username = "huongnguyen2004"
                        },
                        new
                        {
                            Id = 4,
                            Email = "draogon10a3@gmail.com",
                            FullName = "Trịnh Đình Quốc",
                            NormalizedEmail = "DRAOGON10A3@GMAIL.COM",
                            NormalizedUsername = "QUOCTRINH2004",
                            PasswordHash = "BA332EC1C25BE2CA96EB569940C20E28F51930404D1EB24F7C8CEA49464209E3955B85B87EB9F0902FEC750535647A1B3E15E7534CBB1B705290EBA6875C93A8",
                            PasswordSalt = "B2FFFE4BCBDC51F98864A95EA7F4FD076B53DF9C89F761094EF1A96A3A6B13E027DB8ACAE5624DB68075465BEE117D9E4ABC84B6344C041582B72857D7BEF440",
                            ProfilePicture = "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg",
                            RoleId = 2,
                            Username = "quoctrinh2004"
                        },
                        new
                        {
                            Id = 5,
                            Email = "n2h1706@gmail.com",
                            FullName = "Nguyễn Hữu Hùng",
                            NormalizedEmail = "N2H1706@GMAIL.COM",
                            NormalizedUsername = "HUNGNGUYEN1998",
                            PasswordHash = "85003C11C9DB8C44D3AB69389F0C9FA22B501A4229ADEBCD824E7DE835CB6B056F4C5FB905403370099F001E132C5F33C53110A08D5F1EED3B99B37320E1227E",
                            PasswordSalt = "8F794F05716A60EB26E9DE6D59ADF428F29FF2B891824580AAD21C8E06922B40BEC810BC34021E039AFD899CE4F42069A90F093D178FD4F9A026A7FE0367BDC9",
                            ProfilePicture = "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg",
                            RoleId = 2,
                            Username = "hungnguyen1998"
                        });
                });

            modelBuilder.Entity("ExamonimyWeb.Entities.ExamPaper", b =>
                {
                    b.HasOne("ExamonimyWeb.Entities.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExamonimyWeb.Entities.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Course");
                });

            modelBuilder.Entity("ExamonimyWeb.Entities.ExamPaperQuestion", b =>
                {
                    b.HasOne("ExamonimyWeb.Entities.ExamPaper", null)
                        .WithMany("ExamPaperQuestions")
                        .HasForeignKey("ExamPaperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExamonimyWeb.Entities.Question", null)
                        .WithMany("ExamPaperQuestions")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ExamonimyWeb.Entities.FillInBlankQuestion", b =>
                {
                    b.HasOne("ExamonimyWeb.Entities.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("ExamonimyWeb.Entities.MultipleChoiceQuestionWithMultipleCorrectAnswers", b =>
                {
                    b.HasOne("ExamonimyWeb.Entities.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("ExamonimyWeb.Entities.MultipleChoiceQuestionWithOneCorrectAnswer", b =>
                {
                    b.HasOne("ExamonimyWeb.Entities.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("ExamonimyWeb.Entities.Question", b =>
                {
                    b.HasOne("ExamonimyWeb.Entities.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExamonimyWeb.Entities.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExamonimyWeb.Entities.QuestionLevel", "QuestionLevel")
                        .WithMany()
                        .HasForeignKey("QuestionLevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExamonimyWeb.Entities.QuestionType", "QuestionType")
                        .WithMany()
                        .HasForeignKey("QuestionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Course");

                    b.Navigation("QuestionLevel");

                    b.Navigation("QuestionType");
                });

            modelBuilder.Entity("ExamonimyWeb.Entities.ShortAnswerQuestion", b =>
                {
                    b.HasOne("ExamonimyWeb.Entities.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("ExamonimyWeb.Entities.TrueFalseQuestion", b =>
                {
                    b.HasOne("ExamonimyWeb.Entities.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("ExamonimyWeb.Entities.User", b =>
                {
                    b.HasOne("ExamonimyWeb.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("ExamonimyWeb.Entities.ExamPaper", b =>
                {
                    b.Navigation("ExamPaperQuestions");
                });

            modelBuilder.Entity("ExamonimyWeb.Entities.Question", b =>
                {
                    b.Navigation("ExamPaperQuestions");
                });

            modelBuilder.Entity("ExamonimyWeb.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
