using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.EntityModel;

namespace DataAccess
{
	public class EClassbookDbContext : DbContext
	{
		public EClassbookDbContext(DbContextOptions<EClassbookDbContext> options) : base(options) { }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Server=localhost;Database=ElectronicClassbook;Trusted_Connection=True;");
		}

		public DbSet<School> Schools { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Teacher> Teachers { get; set; }
		public DbSet<ClassTeacher> ClassTeachers { get; set; }
		public DbSet<Parent> Parents { get; set; }
		public DbSet<Student> Students { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<Subject> Subjects { get; set; }
		public DbSet<Class> Classes { get; set; }
		public DbSet<Record> Records { get; set; }
		public DbSet<Attendance> Attendances { get; set; }
		public DbSet<AbsentNote> AbsentNotes { get; set; }
		public DbSet<AbsentNoteState> AbsentNoteStates { get; set; }
		public DbSet<Classbook> Classbooks { get; set; }

		public DbSet<Event> Events { get; set; }
		public DbSet<Inspection> Inspections { get; set; }
		public DbSet<Instruction> Instructions { get; set; }
		public DbSet<Homework> Homeworks { get; set; }

		public DbSet<SchoolHomeNote> SchoolHomeNotes { get; set; }
		public DbSet<Account> Accounts { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Teacher>().ToTable("Teacher");
			modelBuilder.Entity<ClassTeacher>().ToTable("ClassTeacher");
			modelBuilder.Entity<Parent>().ToTable("Parent");
			modelBuilder.Entity<Student>().ToTable("Student");
			modelBuilder.Entity<Inspection>().ToTable("Inspection");
			modelBuilder.Entity<Instruction>().ToTable("Instruction");
			modelBuilder.Entity<Homework>().ToTable("Homework");

			modelBuilder.Entity<SchoolHomeNote>()
				.HasOne(s => s.Student)
				.WithMany(s => s.SchoolHomeNotes)
				.HasForeignKey(s => s.StudentId);

			modelBuilder.Entity<SchoolHomeNote>()
				.HasOne(s => s.CreatedBy)
				.WithMany(t => t.SchoolHomeNotes)
				.HasForeignKey(s => s.TeacherId);

			modelBuilder.Entity<SchoolHomeNote>()
				.Property(s => s.ShowParent)
				.HasDefaultValue(true);
			modelBuilder.Entity<SchoolHomeNote>()
				.Property(s => s.ShowStudent)
				.HasDefaultValue(true);

			modelBuilder.Entity<Homework>()
				.Property(s => s.ShowParent)
				.HasDefaultValue(true);
			modelBuilder.Entity<Homework>()
				.Property(s => s.ShowStudent)
				.HasDefaultValue(true);

			modelBuilder.Entity<User>()
				.HasIndex(u => u.Email)
				.IsUnique();

			modelBuilder.Entity<Subject>()
				.HasIndex(s => s.Code)
				.IsUnique();

			modelBuilder.Entity<Subject>()
				.HasMany(s => s.Records)
				.WithOne(r => r.Subject)
				.HasForeignKey(r => r.SubjectId);

			modelBuilder.Entity<Class>()
				.HasOne(c => c.ClassTeacher)
				.WithOne(ct => ct.Class)
				.HasForeignKey<ClassTeacher>(ct => ct.ClassId);

			modelBuilder.Entity<Parent>()
				.HasOne(p => p.Account)
				.WithOne(a => a.Parent)
				.HasForeignKey<Account>(a => a.ParentId);


			modelBuilder.Entity<UserRole>()
				.HasKey(ur => new { ur.UserId, ur.RoleId });
			modelBuilder.Entity<UserRole>()
				.HasOne(ur => ur.User)
				.WithMany(u => u.UserRoles)
				.HasForeignKey(ur => ur.UserId);
			modelBuilder.Entity<UserRole>()
				.HasOne(ur => ur.Role)
				.WithMany(r => r.UserRoles)
				.HasForeignKey(ur => ur.RoleId);


			modelBuilder.Entity<TeacherSubject>()
				.HasKey(ts => new { ts.SubjectId, ts.TeacherId });
			modelBuilder.Entity<TeacherSubject>()
				.HasOne(ts => ts.Teacher)
				.WithMany(t => t.TeacherSubjects)
				.HasForeignKey(ts => ts.TeacherId);
			modelBuilder.Entity<TeacherSubject>()
				.HasOne(ts => ts.Subject)
				.WithMany(s => s.TeacherSubjects)
				.HasForeignKey(ts => ts.SubjectId);


			modelBuilder.Entity<ClassSubject>()
				.HasKey(cs => new { cs.ClassId, cs.SubjectId });
			modelBuilder.Entity<ClassSubject>()
				.HasOne(cs => cs.Subject)
				.WithMany(s => s.ClassSubjects)
				.HasForeignKey(cs => cs.SubjectId);
			modelBuilder.Entity<ClassSubject>()
				.HasOne(cs => cs.Class)
				.WithMany(c => c.ClassSubjects)
				.HasForeignKey(cs => cs.ClassId);


			modelBuilder.Entity<ParentStudent>()
				.HasKey(ps => new { ps.ParentId, ps.StudentId });
			modelBuilder.Entity<ParentStudent>()
				.HasOne(ps => ps.Parent)
				.WithMany(p => p.ParentStudents)
				.HasForeignKey(ps => ps.ParentId);
			modelBuilder.Entity<ParentStudent>()
				.HasOne(ps => ps.Student)
				.WithMany(s => s.ParentStudents)
				.HasForeignKey(ps => ps.StudentId);


			modelBuilder.Entity<Record>()
				.HasMany(r => r.Attendances)
				.WithOne(a => a.Record)
				.HasForeignKey(a => a.RecordId)
				.IsRequired();

			modelBuilder.Entity<Record>()
				.HasMany(r => r.Events)
				.WithOne(e => e.Record)
				.HasForeignKey(e => e.RecordId);

			modelBuilder.Entity<Record>()
				.HasOne(r => r.CreatedBy)
				.WithMany(t => t.Records)
				.HasForeignKey(r => r.TeacherId);

			modelBuilder.Entity<Record>()
				.HasOne(r => r.Classbook)
				.WithMany(c => c.Records)
				.HasForeignKey(r => r.ClassbookId);

			modelBuilder.Entity<Attendance>()
				.HasOne(a => a.Student)
				.WithMany(s => s.Attendances)
				.HasForeignKey(a => a.StudentId);

			modelBuilder.Entity<AbsentNote>()
				.HasMany(a => a.Absences)
				.WithOne(a => a.AbsentNote)
				.HasForeignKey(a => a.AbsentNoteId);

			modelBuilder.Entity<AbsentNote>()
				.HasOne(a => a.Parent)
				.WithMany(p => p.AbsentNotes)
				.HasForeignKey(a => a.ParentId);

			modelBuilder.Entity<AbsentNote>()
				.HasOne(a => a.ClassTeacher)
				.WithMany(c => c.AbsentNotes)
				.HasForeignKey(a => a.ClassTeacherId);

			modelBuilder.Entity<AbsentNote>()
				.HasOne(a => a.Student)
				.WithMany(p => p.AbsentNotes)
				.HasForeignKey(a => a.StudentId);

			modelBuilder.Entity<AbsentNote>()
				.HasOne(a => a.State)
				.WithMany(a => a.AbsentNotes)
				.HasForeignKey(a => a.StateId);

		}

	}
}
