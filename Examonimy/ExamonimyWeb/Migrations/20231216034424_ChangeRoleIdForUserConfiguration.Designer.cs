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
    [Migration("20231216034424_ChangeRoleIdForUserConfiguration")]
    partial class ChangeRoleIdForUserConfiguration
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
                            Name = "Teacher"
                        },
                        new
                        {
                            Id = 3,
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
                            Email = "hoatq@fpt.edu.vn",
                            FullName = "Trịnh Quang Hòa",
                            NormalizedEmail = "HOATQ@FPT.EDU.VN",
                            NormalizedUsername = "HOATQ",
                            PasswordHash = "3DE4B9595AB74AD7812DE0A961E12034C27DE2389246507D66972BB68E4C4AF5C7ACFFEE31F2BB4964F4D744E9D730AC47C6DFB53BE9D2C5863D07BEDF49207C",
                            PasswordSalt = "8CEEBAC27561894CBEC7BD790F66F070433EB7D67815E7DD79E70C23D0CECA4F50872701E70C52B085D8CF2EA13B13C7CE77EC005F45EE44884E14180EAB6101",
                            ProfilePicture = "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg",
                            RoleId = 2,
                            Username = "hoatq"
                        },
                        new
                        {
                            Id = 2,
                            Email = "phuctv1112004@gmail.com",
                            FullName = "Trịnh Văn Phúc",
                            NormalizedEmail = "PHUCTV1112004@GMAIL.COM",
                            NormalizedUsername = "PHUCTRINH2004",
                            PasswordHash = "FFE89A4976DADBB643B988DD320A89FBA90186E67185C65014674150312E2EF142DC70128500EBAAF193E6DBBABC30BDF2C13B468AE04B70CC573AEA93EA1CBC",
                            PasswordSalt = "478823C8496A845A84F7169983F2274768410B044B534624C5455847E59B4DEA4BC30B3E1214278A88D57ED8D587E7933BC3E272560E50E9BF79374BEA5E10A7",
                            ProfilePicture = "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg",
                            RoleId = 3,
                            Username = "phuctrinh2004"
                        },
                        new
                        {
                            Id = 3,
                            Email = "nguyenhuongg1104@gmail.com",
                            FullName = "Nguyễn Thị Hương",
                            NormalizedEmail = "NGUYENHUONGG1104@GMAIL.COM",
                            NormalizedUsername = "HUONGNGUYEN2004",
                            PasswordHash = "3F454F2A5B1DFC75E9857FBC161248D0B253DF34D6A6A0438AB5221A9BCC9ABB7CE694B45844335996E566A3099DCB95D49A026ACF3DE8B4BF5861B82437964E",
                            PasswordSalt = "B63C8F247FBDA298C5ACDDD4D4A04CFC3443B68E0C24125152171A8FDDEDF7D267A32F5673B7A4C35D586A9C9249D53BA2903365FADA567C81F3C7D167417D25",
                            ProfilePicture = "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg",
                            RoleId = 3,
                            Username = "huongnguyen2004"
                        },
                        new
                        {
                            Id = 4,
                            Email = "draogon10a3@gmail.com",
                            FullName = "Trịnh Đình Quốc",
                            NormalizedEmail = "DRAOGON10A3@GMAIL.COM",
                            NormalizedUsername = "QUOCTRINH2004",
                            PasswordHash = "B625F65BF141FD751A825DDD5F30191BC991E151080FFD936CB5AF068C8E139B39C7790116EA28A1D2607860026020417B5940203B33547B8AE5D14C3C40EE5E",
                            PasswordSalt = "D176E4990D830E111546DDDAB84BB4BA162E0E9330FFF7C68DB58B75B662EA10B03ADB5A48EF644E2ED514FE669EF7D4049731016B01914FDF463D3147CCF3B4",
                            ProfilePicture = "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg",
                            RoleId = 3,
                            Username = "quoctrinh2004"
                        },
                        new
                        {
                            Id = 5,
                            Email = "n2h1706@gmail.com",
                            FullName = "Nguyễn Hữu Hùng",
                            NormalizedEmail = "N2H1706@GMAIL.COM",
                            NormalizedUsername = "HUNGNGUYEN1998",
                            PasswordHash = "63D07A66A6A2E4E21DD596E7CDB3BF41A98AB650D126A711DA3A0964BA4F2E5E44D6E2BDE5E7CFCF8E8EC3E140AE696F7B78EE8ED1E8CDC1A71ABCDFC7E74718",
                            PasswordSalt = "72DD616EFAB58FBF1CD612E190B2D1BBC3B7D70D7E75872DFA07C47E7A01884A0063F3418CF4483C8BBC86DF2C617B057BBA6187B3E5148D9540F6D170BE4823",
                            ProfilePicture = "https://nghia.b-cdn.net/examonimy/images/examonimy-default-pfp.jpg",
                            RoleId = 3,
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