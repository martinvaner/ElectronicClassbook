using DataAccess.EntityModel;
using DataAccess.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Repository
{
	public class AttendanceRepository : IAttendanceRepository
	{
		private readonly EClassbookDbContext dbContext;
		public AttendanceRepository(EClassbookDbContext dbContext)
		{
			this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
		}

		public Parent GetParentByEmail(string email)
		{
			return dbContext.Parents.Where(x => x.Email.Equals(email)).FirstOrDefault();
		}

		public IEnumerable<Attendance> GetAbsenceByStudentId(int id)
		{
			return dbContext.Attendances.Include(x => x.Record).ThenInclude(x => x.Subject).Include(x => x.AbsentNote).ThenInclude(x => x.State).Where(x => x.StudentId.Equals(id))
						.Where(x => x.Present == false).OrderByDescending(x => x.Record.Created).ToList();
		}

		public IEnumerable<Student> GetStudentsByParentId(int id)
		{
			return dbContext.Students.Include(x => x.Class).Include(x => x.Attendances).ThenInclude(x => x.Record).Include(x => x.SchoolHomeNotes).ThenInclude(x => x.CreatedBy)
						.Where(x => x.ParentStudents.Select(x => x.ParentId).Contains(id)).ToList();
		}


		public AbsentNoteState GetAbsentNoteStateByName(string name)
		{
			return dbContext.AbsentNoteStates.Where(x => x.Name.Equals(name)).FirstOrDefault();
		}

		public Attendance GetAttendanceById(int id)
		{
			return dbContext.Attendances.Include(x => x.Record).Include(x => x.Student).Where(x => x.Id.Equals(id)).FirstOrDefault();
		}

		public bool CreateAbsentNote(AbsentNote note)
		{
			try
			{
				dbContext.AbsentNotes.Add(note);
				dbContext.SaveChanges();
				return true;
			}
			catch(Exception ex)
			{
				return false;
			}
		}

		public Student GetStudentById(int id)
		{
			return dbContext.Students.Where(x => x.Id.Equals(id)).FirstOrDefault();
		}

		public ClassTeacher GetClassTeacherByEmail(string email)
		{
			return dbContext.ClassTeachers.Include(x => x.Class).Where(x => x.Email.Equals(email)).FirstOrDefault();
		}

		public IEnumerable<Student> GetStudentInClass(int classId)
		{
			return dbContext.Students.Include(x => x.Attendances).Where(x => x.Class.Id.Equals(classId));
		}

		public IEnumerable<AbsentNote> GetNewAbsentNotesByStudentId(int studentId)
		{
			return dbContext.AbsentNotes.Include(x => x.Absences).ThenInclude(x => x.Record).ThenInclude(x => x.Subject).Where(x => x.StudentId.Equals(studentId)).Where(x => x.State.Name.Equals("Odeslána"));
		}

		public AbsentNote GetAbsentNoteById(int id)
		{
			return dbContext.AbsentNotes.Include(x => x.Absences).Where(x => x.Id.Equals(id)).FirstOrDefault();
		}

		public bool UpdateAbsentNote(AbsentNote note)
		{
			try
			{
				dbContext.AbsentNotes.Update(note);
				dbContext.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
	}
}
