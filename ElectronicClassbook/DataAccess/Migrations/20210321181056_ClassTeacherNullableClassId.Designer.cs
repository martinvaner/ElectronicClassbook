﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccess.Migrations
{
    [DbContext(typeof(EClassbookDbContext))]
    [Migration("20210321181056_ClassTeacherNullableClassId")]
    partial class ClassTeacherNullableClassId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("DataAccess.EntityModel.AbsentNote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("ClassTeacherId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<int>("StateId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClassTeacherId");

                    b.HasIndex("ParentId");

                    b.HasIndex("StateId");

                    b.HasIndex("StudentId");

                    b.ToTable("AbsentNotes");
                });

            modelBuilder.Entity("DataAccess.EntityModel.AbsentNoteState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("AbsentNoteStates");
                });

            modelBuilder.Entity("DataAccess.EntityModel.Attendance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AbsenceInterval")
                        .HasColumnType("int");

                    b.Property<int?>("AbsentNoteId")
                        .HasColumnType("int");

                    b.Property<bool>("IsExcused")
                        .HasColumnType("bit");

                    b.Property<bool>("LateArrival")
                        .HasColumnType("bit");

                    b.Property<bool>("Present")
                        .HasColumnType("bit");

                    b.Property<int>("RecordId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AbsentNoteId");

                    b.HasIndex("RecordId");

                    b.HasIndex("StudentId");

                    b.ToTable("Attendances");
                });

            modelBuilder.Entity("DataAccess.EntityModel.Class", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("DataAccess.EntityModel.ClassSubject", b =>
                {
                    b.Property<int>("ClassId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.HasKey("ClassId", "SubjectId");

                    b.HasIndex("SubjectId");

                    b.ToTable("ClassSubject");
                });

            modelBuilder.Entity("DataAccess.EntityModel.Classbook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("ClassId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("SchoolYear")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("ClassId");

                    b.ToTable("Classbooks");
                });

            modelBuilder.Entity("DataAccess.EntityModel.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("RecordId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RecordId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("DataAccess.EntityModel.ParentStudent", b =>
                {
                    b.Property<int>("ParentId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("ParentId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("ParentStudent");
                });

            modelBuilder.Entity("DataAccess.EntityModel.Record", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("ClassbookId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsSubstituted")
                        .HasColumnType("bit");

                    b.Property<int>("SerialNumber")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.Property<string>("Topic")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("ClassbookId");

                    b.HasIndex("SubjectId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Records");
                });

            modelBuilder.Entity("DataAccess.EntityModel.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("DataAccess.EntityModel.School", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CIN")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Schools");
                });

            modelBuilder.Entity("DataAccess.EntityModel.SchoolHomeNote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ShowParent")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<bool>("ShowStudent")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.HasIndex("TeacherId");

                    b.ToTable("SchoolHomeNotes");
                });

            modelBuilder.Entity("DataAccess.EntityModel.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("DataAccess.EntityModel.TeacherSubject", b =>
                {
                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("SubjectId", "TeacherId");

                    b.HasIndex("TeacherId");

                    b.ToTable("TeacherSubject");
                });

            modelBuilder.Entity("DataAccess.EntityModel.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("BirthLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Citizenship")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool?>("Gender")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("PersonalIdentificationNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResetToken")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime?>("ResetTokenTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("TitleAfter")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleBefore")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DataAccess.EntityModel.UserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("DataAccess.EntityModel.Homework", b =>
                {
                    b.HasBaseType("DataAccess.EntityModel.Event");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("datetime2");

                    b.Property<bool>("ShowParent")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<bool>("ShowStudent")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.ToTable("Homework");
                });

            modelBuilder.Entity("DataAccess.EntityModel.Inspection", b =>
                {
                    b.HasBaseType("DataAccess.EntityModel.Event");

                    b.Property<string>("Auditor")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Inspection");
                });

            modelBuilder.Entity("DataAccess.EntityModel.Instruction", b =>
                {
                    b.HasBaseType("DataAccess.EntityModel.Event");

                    b.Property<int?>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasIndex("AuthorId");

                    b.ToTable("Instruction");
                });

            modelBuilder.Entity("DataAccess.EntityModel.Parent", b =>
                {
                    b.HasBaseType("DataAccess.EntityModel.User");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.ToTable("Parent");
                });

            modelBuilder.Entity("DataAccess.EntityModel.Student", b =>
                {
                    b.HasBaseType("DataAccess.EntityModel.User");

                    b.Property<int?>("ClassId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CompletionDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EnrollmentDate")
                        .HasColumnType("datetime2");

                    b.HasIndex("ClassId");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("DataAccess.EntityModel.Teacher", b =>
                {
                    b.HasBaseType("DataAccess.EntityModel.User");

                    b.Property<DateTime>("HireDate")
                        .HasColumnType("datetime2");

                    b.ToTable("Teacher");
                });

            modelBuilder.Entity("DataAccess.EntityModel.ClassTeacher", b =>
                {
                    b.HasBaseType("DataAccess.EntityModel.Teacher");

                    b.Property<int?>("ClassId")
                        .HasColumnType("int");

                    b.HasIndex("ClassId")
                        .IsUnique()
                        .HasFilter("[ClassId] IS NOT NULL");

                    b.ToTable("ClassTeacher");
                });

            modelBuilder.Entity("DataAccess.EntityModel.AbsentNote", b =>
                {
                    b.HasOne("DataAccess.EntityModel.ClassTeacher", "ClassTeacher")
                        .WithMany("AbsentNotes")
                        .HasForeignKey("ClassTeacherId");

                    b.HasOne("DataAccess.EntityModel.Parent", "Parent")
                        .WithMany("AbsentNotes")
                        .HasForeignKey("ParentId");

                    b.HasOne("DataAccess.EntityModel.AbsentNoteState", "State")
                        .WithMany("AbsentNotes")
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.EntityModel.Student", "Student")
                        .WithMany("AbsentNotes")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClassTeacher");

                    b.Navigation("Parent");

                    b.Navigation("State");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("DataAccess.EntityModel.Attendance", b =>
                {
                    b.HasOne("DataAccess.EntityModel.AbsentNote", "AbsentNote")
                        .WithMany("Absences")
                        .HasForeignKey("AbsentNoteId");

                    b.HasOne("DataAccess.EntityModel.Record", "Record")
                        .WithMany("Attendances")
                        .HasForeignKey("RecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.EntityModel.Student", "Student")
                        .WithMany("Attendances")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AbsentNote");

                    b.Navigation("Record");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("DataAccess.EntityModel.ClassSubject", b =>
                {
                    b.HasOne("DataAccess.EntityModel.Class", "Class")
                        .WithMany("ClassSubjects")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.EntityModel.Subject", "Subject")
                        .WithMany("ClassSubjects")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("DataAccess.EntityModel.Classbook", b =>
                {
                    b.HasOne("DataAccess.EntityModel.Class", "Class")
                        .WithMany()
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");
                });

            modelBuilder.Entity("DataAccess.EntityModel.Event", b =>
                {
                    b.HasOne("DataAccess.EntityModel.Record", "Record")
                        .WithMany("Events")
                        .HasForeignKey("RecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Record");
                });

            modelBuilder.Entity("DataAccess.EntityModel.ParentStudent", b =>
                {
                    b.HasOne("DataAccess.EntityModel.Parent", "Parent")
                        .WithMany("ParentStudents")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.EntityModel.Student", "Student")
                        .WithMany("ParentStudents")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parent");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("DataAccess.EntityModel.Record", b =>
                {
                    b.HasOne("DataAccess.EntityModel.Classbook", "Classbook")
                        .WithMany("Records")
                        .HasForeignKey("ClassbookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.EntityModel.Subject", "Subject")
                        .WithMany("Records")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.EntityModel.Teacher", "CreatedBy")
                        .WithMany("Records")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Classbook");

                    b.Navigation("CreatedBy");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("DataAccess.EntityModel.SchoolHomeNote", b =>
                {
                    b.HasOne("DataAccess.EntityModel.Student", "Student")
                        .WithMany("SchoolHomeNotes")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.EntityModel.Teacher", "CreatedBy")
                        .WithMany("SchoolHomeNotes")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("DataAccess.EntityModel.TeacherSubject", b =>
                {
                    b.HasOne("DataAccess.EntityModel.Subject", "Subject")
                        .WithMany("TeacherSubjects")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.EntityModel.Teacher", "Teacher")
                        .WithMany("TeacherSubjects")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subject");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("DataAccess.EntityModel.UserRole", b =>
                {
                    b.HasOne("DataAccess.EntityModel.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.EntityModel.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataAccess.EntityModel.Homework", b =>
                {
                    b.HasOne("DataAccess.EntityModel.Event", null)
                        .WithOne()
                        .HasForeignKey("DataAccess.EntityModel.Homework", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccess.EntityModel.Inspection", b =>
                {
                    b.HasOne("DataAccess.EntityModel.Event", null)
                        .WithOne()
                        .HasForeignKey("DataAccess.EntityModel.Inspection", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccess.EntityModel.Instruction", b =>
                {
                    b.HasOne("DataAccess.EntityModel.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");

                    b.HasOne("DataAccess.EntityModel.Event", null)
                        .WithOne()
                        .HasForeignKey("DataAccess.EntityModel.Instruction", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("DataAccess.EntityModel.Parent", b =>
                {
                    b.HasOne("DataAccess.EntityModel.User", null)
                        .WithOne()
                        .HasForeignKey("DataAccess.EntityModel.Parent", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccess.EntityModel.Student", b =>
                {
                    b.HasOne("DataAccess.EntityModel.Class", "Class")
                        .WithMany("Students")
                        .HasForeignKey("ClassId");

                    b.HasOne("DataAccess.EntityModel.User", null)
                        .WithOne()
                        .HasForeignKey("DataAccess.EntityModel.Student", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Class");
                });

            modelBuilder.Entity("DataAccess.EntityModel.Teacher", b =>
                {
                    b.HasOne("DataAccess.EntityModel.User", null)
                        .WithOne()
                        .HasForeignKey("DataAccess.EntityModel.Teacher", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccess.EntityModel.ClassTeacher", b =>
                {
                    b.HasOne("DataAccess.EntityModel.Class", "Class")
                        .WithOne("ClassTeacher")
                        .HasForeignKey("DataAccess.EntityModel.ClassTeacher", "ClassId");

                    b.HasOne("DataAccess.EntityModel.Teacher", null)
                        .WithOne()
                        .HasForeignKey("DataAccess.EntityModel.ClassTeacher", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Class");
                });

            modelBuilder.Entity("DataAccess.EntityModel.AbsentNote", b =>
                {
                    b.Navigation("Absences");
                });

            modelBuilder.Entity("DataAccess.EntityModel.AbsentNoteState", b =>
                {
                    b.Navigation("AbsentNotes");
                });

            modelBuilder.Entity("DataAccess.EntityModel.Class", b =>
                {
                    b.Navigation("ClassSubjects");

                    b.Navigation("ClassTeacher");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("DataAccess.EntityModel.Classbook", b =>
                {
                    b.Navigation("Records");
                });

            modelBuilder.Entity("DataAccess.EntityModel.Record", b =>
                {
                    b.Navigation("Attendances");

                    b.Navigation("Events");
                });

            modelBuilder.Entity("DataAccess.EntityModel.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("DataAccess.EntityModel.Subject", b =>
                {
                    b.Navigation("ClassSubjects");

                    b.Navigation("Records");

                    b.Navigation("TeacherSubjects");
                });

            modelBuilder.Entity("DataAccess.EntityModel.User", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("DataAccess.EntityModel.Parent", b =>
                {
                    b.Navigation("AbsentNotes");

                    b.Navigation("ParentStudents");
                });

            modelBuilder.Entity("DataAccess.EntityModel.Student", b =>
                {
                    b.Navigation("AbsentNotes");

                    b.Navigation("Attendances");

                    b.Navigation("ParentStudents");

                    b.Navigation("SchoolHomeNotes");
                });

            modelBuilder.Entity("DataAccess.EntityModel.Teacher", b =>
                {
                    b.Navigation("Records");

                    b.Navigation("SchoolHomeNotes");

                    b.Navigation("TeacherSubjects");
                });

            modelBuilder.Entity("DataAccess.EntityModel.ClassTeacher", b =>
                {
                    b.Navigation("AbsentNotes");
                });
#pragma warning restore 612, 618
        }
    }
}
