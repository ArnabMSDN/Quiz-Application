﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Quiz_Application.Services;

namespace Quiz_Application.Services.Migrations
{
    [DbContext(typeof(QuizDBContext))]
    partial class QuizDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Quiz_Application.Services.Entities.Answer", b =>
                {
                    b.Property<int>("Sl_No")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ChoiceID")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("DisplayText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime");

                    b.Property<int>("QuestionID")
                        .HasColumnType("int");

                    b.HasKey("Sl_No");

                    b.ToTable("Answer");
                });

            modelBuilder.Entity("Quiz_Application.Services.Entities.Candidate", b =>
                {
                    b.Property<int>("Sl_No")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Candidate_ID")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("ImgFile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Password")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Phone")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Roles")
                        .HasColumnType("varchar(100)");

                    b.HasKey("Sl_No");

                    b.ToTable("Candidate");
                });

            modelBuilder.Entity("Quiz_Application.Services.Entities.Choice", b =>
                {
                    b.Property<int>("ChoiceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("DisplayText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime");

                    b.Property<int>("QuestionID")
                        .HasColumnType("int");

                    b.HasKey("ChoiceID");

                    b.ToTable("Choice");
                });

            modelBuilder.Entity("Quiz_Application.Services.Entities.Exam", b =>
                {
                    b.Property<int>("ExamID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<decimal>("Duration")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("FullMarks")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(1000)");

                    b.HasKey("ExamID");

                    b.ToTable("Exam");
                });

            modelBuilder.Entity("Quiz_Application.Services.Entities.Question", b =>
                {
                    b.Property<int>("QuestionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("DisplayText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ExamID")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime");

                    b.Property<int>("QuestionType")
                        .HasColumnType("int");

                    b.HasKey("QuestionID");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("Quiz_Application.Services.Entities.Result", b =>
                {
                    b.Property<int>("Sl_No")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AnswerID")
                        .HasColumnType("int");

                    b.Property<int>("CandidateID")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<int>("ExamID")
                        .HasColumnType("int");

                    b.Property<bool>("IsCorrent")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime");

                    b.Property<int>("QuestionID")
                        .HasColumnType("int");

                    b.Property<int>("SelectedOptionID")
                        .HasColumnType("int");

                    b.Property<string>("SessionID")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Sl_No");

                    b.ToTable("Result");
                });

            modelBuilder.Entity("Quiz_Application.Services.Entities.Root", b =>
                {
                    b.Property<string>("Exam")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ExamID")
                        .HasColumnType("int");

                    b.Property<string>("QuizDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Score")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SessionID")
                        .HasColumnType("nvarchar(max)");
                });
#pragma warning restore 612, 618
        }
    }
}
