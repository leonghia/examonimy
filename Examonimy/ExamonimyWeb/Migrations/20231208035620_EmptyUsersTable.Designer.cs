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
    [Migration("20231208035620_EmptyUsersTable")]
    partial class EmptyUsersTable
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

                    b.Property<byte>("TimeAllowedInMinutes")
                        .HasColumnType("tinyint");

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
