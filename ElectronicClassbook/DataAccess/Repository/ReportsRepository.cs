using DataAccess.EntityModel;
using DataAccess.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Repository
{
	public class ReportsRepository : IReportsRepository
	{

		private readonly EClassbookDbContext dbContext;
		private bool disposed = false;

		public ReportsRepository(EClassbookDbContext dbContext)
		{
			this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed && disposing)
			{
				dbContext.Dispose();
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}


		public Parent GetParentByEmail(string email)
		{
			return dbContext.Parents.Where(x => x.Email.Equals(email)).FirstOrDefault();
		}

		public IEnumerable<Student> GetStudentsByParentId(int id)
		{
			return dbContext.Students.Include(x => x.Class).Include(x => x.Attendances).ThenInclude(x => x.Record).Include(x => x.SchoolHomeNotes).ThenInclude(x => x.CreatedBy)
						.Where(x => x.ParentStudents.Select(x => x.ParentId).Contains(id)).ToList();
		}

		public IEnumerable<Attendance> GetAbsenceByStudentId(int id)
		{
			return dbContext.Attendances.Include(x => x.Record).ThenInclude(x => x.Subject).Include(x => x.AbsentNote).ThenInclude(x => x.State)
					.Where(x => x.StudentId.Equals(id))
					.Where(x => x.Present == false)
					.Where(x => x.Record.Classbook.IsActive)
					.OrderByDescending(x => x.Record.Created).ToList();
		}

		public IEnumerable<Homework> GetHomeworksByClassId(int id)
		{
			return dbContext.Homeworks.Include(x => x.Record).ThenInclude(x => x.Subject)
					.Where(x => x.Record.Classbook.IsActive)
					.Where(x => x.Record.Classbook.ClassId.Equals(id))
					.OrderBy(x => x.Deadline).ToList();
		}

		public IEnumerable<SchoolHomeNote> GetSchoolHomeNotesByStudentId(int id)
		{
			return dbContext.SchoolHomeNotes.Include(x => x.CreatedBy)
					.Where(x => x.StudentId.Equals(id))
					.OrderByDescending(x => x.Created).ToList();
		}

		public Class GetClassByStudentId(int id)
		{
			return dbContext.Classes.Where(x => x.Students.Select(x => x.Id).Contains(id)).FirstOrDefault();
		}

		public Homework GetHomeworkById(int id)
		{
			return dbContext.Homeworks.Include(x => x.Record).ThenInclude(x => x.Subject).Where(x => x.Id.Equals(id)).FirstOrDefault();
		}

		public bool UpdateHomework(Homework homework)
		{
			try
			{
				dbContext.Homeworks.Update(homework);
				dbContext.SaveChanges();
				return true;
			}
			catch(Exception ex)
			{
				return false;
			}
		}

		public SchoolHomeNote GetSchoolHomeNoteById(int id)
		{
			return dbContext.SchoolHomeNotes.Include(x => x.CreatedBy).Where(x => x.Id.Equals(id)).FirstOrDefault();
		}

		public bool UpdateSchoolHomeNote(SchoolHomeNote note)
		{
			try
			{
				dbContext.SchoolHomeNotes.Update(note);
				dbContext.SaveChanges();
				return true;
			}
			catch(Exception ex)
			{
				return false;
			}
		}

		public IEnumerable<AbsentNote> GetAbsentNotesByStudentId(int studentId)
		{
			return dbContext.AbsentNotes.Include(x => x.Absences).ThenInclude(x => x.Record).ThenInclude(x => x.Subject)
				.Include(x => x.Parent).Include(x => x.State).Include(x => x.Student).Where(x => x.StudentId.Equals(studentId)).Where(x => x.State.Name.Equals("Odeslána"));

		}

		public Teacher GetTeacherByEmail(string email)
		{
			return dbContext.Teachers.Include(x => x.Records).ThenInclude(x => x.Events).Include(x => x.Records).ThenInclude(x => x.Classbook)
						.ThenInclude(x => x.Class).Where(x => x.Email.Equals(email)).FirstOrDefault();
		}

		public ClassTeacher GetClassTeacherByEmail(string email)
		{
			return dbContext.ClassTeachers.Include(x => x.Class).Where(x => x.Email.Equals(email)).FirstOrDefault();
		}

		public IEnumerable<Student> GetStudentInClass(int classId)
		{
			return dbContext.Students.Include(x => x.Attendances).Include(x => x.Class)
					.Where(x => x.Class.Id.Equals(classId));
		}

		public IEnumerable<AbsentNote> GetNewAbsentNotesByStudentId(int studentId)
		{
			return dbContext.AbsentNotes.Include(x => x.Absences).ThenInclude(x => x.Record).ThenInclude(x => x.Subject).Where(x => x.StudentId.Equals(studentId)).Where(x => x.State.Name.Equals("Odeslána"));
		}

		public Student GetStudentByEmail(string email)
		{
			return dbContext.Students.Where(x => x.Email.Equals(email)).FirstOrDefault();
		}

		public IEnumerable<Class> GetAllClasses()
		{
			return dbContext.Classes;
		}

		public Student GetStudentById(int id)
		{
			return dbContext.Students.Include(x => x.Class)
					.Where(x => x.Id.Equals(id)).FirstOrDefault();
		}
	}
}
