using DataAccess.EntityModel;
using DataAccess.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Repository
{
	public class ClassbookRepository : IClassbookRepository

	{
		private readonly EClassbookDbContext dbContext;

		public ClassbookRepository(EClassbookDbContext dbContext)
		{
			this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
		}

		public School GetSchool()
		{
			return dbContext.Schools.FirstOrDefault();
		}

		public User GetPrincipal()
		{
			return dbContext.Users.Where(x => x.UserRoles.Select(x => x.Role.Name).Contains("Ředitel")).FirstOrDefault();
		}

		public IEnumerable<Classbook> GetAllClassbooks()
		{
			return dbContext.Classbooks.Include(c => c.Class).ThenInclude(c => c.Students).Include(c => c.Records).Include(x => x.Class).ThenInclude(x => x.ClassTeacher)
					.ToList();
		}

		public IEnumerable<Class> GetAllClasses()
		{
			return dbContext.Classes.Include(c => c.Students).ToList();
		}

		public bool CreateClassbook(DataAccess.EntityModel.Classbook c)
		{
			try
			{
				dbContext.Classbooks.Add(c);
				dbContext.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
		public bool UpdateClassbook(DataAccess.EntityModel.Classbook c)
		{
			try
			{
				dbContext.Classbooks.Update(c);
				dbContext.SaveChanges();
				return true;
			}
			catch(Exception ex)
			{
				return false;
			}
		}

		public bool UpdateRecord(Record record)
		{
			try
			{
				dbContext.Records.Update(record);
				dbContext.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public bool DeleteClassbook(DataAccess.EntityModel.Classbook c)
		{
			try
			{
				dbContext.Classbooks.Remove(c);
				dbContext.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public IEnumerable<Record> GetRecordsByDate(int classbookId, DateTime date)
		{
			return dbContext.Records.Include(x => x.Subject).Include(x => x.CreatedBy).Include(x => x.Attendances).ThenInclude(x => x.Student).Include(x => x.Events)
										.Where(x => x.Created.Equals(date)).Where(x => x.Classbook.Id.Equals(classbookId)).ToList();
		}

		public IEnumerable<Subject> GetAllSubjects()
		{
			return dbContext.Subjects.ToList();
		}

		public Teacher GetTeacherByEmail(string email)
		{
			return dbContext.Teachers.Where(x => x.Email.Equals(email)).FirstOrDefault();
		}

		public ClassTeacher GetClassTeacherByEmail(string email)
		{
			return dbContext.ClassTeachers.Include(x => x.Class).Where(x => x.Email.Equals(email)).FirstOrDefault();
		}

		public bool CreateRecord(Record record)
		{
			try
			{
				dbContext.Records.Add(record);
				dbContext.SaveChanges();
				return true;
			}
			catch(Exception ex)
			{
				return false;
			}
		}

		public Record GetRecordById(int recordId)
		{
			return dbContext.Records.Include(x => x.CreatedBy).Include(x => x.Classbook).Include(x => x.Attendances).ThenInclude(x => x.Student).Where(x => x.Id.Equals(recordId)).FirstOrDefault();
		}

		public Student GetStudentById(int id)
		{
			return dbContext.Students.Where(x => x.Id.Equals(id)).FirstOrDefault();
		}

		public Attendance GetAttendance(int recordId, int studentId)
		{
			return dbContext.Attendances.Where(x => x.RecordId.Equals(recordId)).Where(x => x.StudentId.Equals(studentId)).FirstOrDefault();
		}

		public bool UpdateAttendance(Attendance attendance)
		{
			try
			{
				dbContext.Attendances.Update(attendance);
				dbContext.SaveChanges();
				return true;
			}
			catch(Exception ex)
			{
				return false;
			}
		}

		public IEnumerable<Attendance> GetAttendancesByRecordId(int recordId)
		{
			return dbContext.Attendances.Where(x => x.RecordId.Equals(recordId)).ToList();
		}

		public bool CreateInspection(Inspection inspection)
		{
			try
			{
				dbContext.Inspections.Add(inspection);
				dbContext.SaveChanges();
				return true;
			}
			catch(Exception ex)
			{
				return false;
			}
		}

		public bool CreateInstruction(Instruction instruction)
		{
			try
			{
				dbContext.Instructions.Add(instruction);
				dbContext.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public bool CreateHomework(Homework homework)
		{
			try
			{
				dbContext.Homeworks.Add(homework);
				dbContext.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public IEnumerable<Inspection> GetInspectionsByClassbookId(int classbookId)
		{
			return dbContext.Inspections.Include(x => x.Record).ThenInclude(x => x.Classbook).ThenInclude(x => x.Class).Include(x => x.Record).ThenInclude(x => x.Subject).Include(x => x.Record.CreatedBy).Where(x => x.Record.ClassbookId.Equals(classbookId)).ToList();
		}

		public Inspection GetInspectionById(int id)
		{
			return dbContext.Inspections.Include(x => x.Record).ThenInclude(x => x.Subject).Include(x => x.Record.CreatedBy).Where(x => x.Id.Equals(id)).FirstOrDefault();
		}

		public bool DeleteInspection(Inspection inspection)
		{
			try
			{
				dbContext.Inspections.Remove(inspection);
				dbContext.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public IEnumerable<Instruction> GetInstructionsByClassbookId(int classbookId)
		{
			return dbContext.Instructions.Include(x => x.Record).Include(x => x.Record.CreatedBy)
					.Where(x => x.Record.ClassbookId.Equals(classbookId))
					.ToList();
		}

		public Instruction GetInstructionById(int id)
		{
			return dbContext.Instructions.Include(x => x.Record).Include(x => x.Record.CreatedBy)
					.Where(x => x.Id.Equals(id))
					.FirstOrDefault();

		}

		public bool DeleteInstruction(Instruction instruction)
		{
			try
			{
				dbContext.Instructions.Remove(instruction);
				dbContext.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public IEnumerable<Student> GetStudentsByClassId(int id)
		{
			return dbContext.Students.Include(x => x.Class).Include(x => x.ParentStudents).ThenInclude(x => x.Parent)
					.Where(x => x.Class.Id.Equals(id))
					.ToList();
		}

		public bool CreateSchoolHomeNote(SchoolHomeNote note)
		{
			try
			{
				dbContext.SchoolHomeNotes.Add(note);
				dbContext.SaveChanges();
				return true;
			}
			catch(Exception ex)
			{
				return false;
			}
		}

		public IEnumerable<SchoolHomeNote> GetSchoolHomeNotesByStudentId(int studentId)
		{
			return dbContext.SchoolHomeNotes.Include(x => x.Student).Include(x => x.CreatedBy)
					.Where(x => x.StudentId.Equals(studentId))
					.ToList();
		}

		public bool DeleteSchoolHomeNote(SchoolHomeNote note)
		{
			try
			{
				dbContext.SchoolHomeNotes.Remove(note);
				dbContext.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public IEnumerable<Record> GetRecords(int classbookId, DateTime from, DateTime to)
		{
			return dbContext.Records.Include(x => x.Subject).Include(x => x.CreatedBy)
					.Where(x => x.ClassbookId.Equals(classbookId))
					.Where(x => (x.Created >= from && x.Created <= to))
					.OrderBy(x => x.Created);
		}

		public IEnumerable<Attendance> GetAbsencesByDate(int classbookId, DateTime date)
		{
			return dbContext.Attendances.Include(x => x.Student).Include(x => x.Record)
					.Where(x => x.Record.ClassbookId.Equals(classbookId))
					.Where(x => x.Present.Equals(false))
					.Where(x => x.Record.Created.Equals(date))
					.OrderBy(x => x.Record.SerialNumber);
		}

		public IEnumerable<Subject> GetClassSubjects(int classId)
		{
			var cl = dbContext.Classes.Include(x => x.ClassSubjects).ThenInclude(x => x.Subject)
					.Where(x => x.Id.Equals(classId))
					.FirstOrDefault();

			return cl.ClassSubjects.Select(x => x.Subject);
		}

		public IEnumerable<Homework> GetHomeworksByClassbookId(int id)
		{
			return dbContext.Homeworks.Include(x => x.Record).ThenInclude(x => x.Subject)
					.Where(x => x.Record.Classbook.Id.Equals(id))
					.OrderBy(x => x.Deadline);
		}

		public Homework GetHomeworkById(int id)
		{
			return dbContext.Homeworks.Include(x => x.Record).ThenInclude(x => x.Subject)
					.Where(x => x.Id.Equals(id)).FirstOrDefault();
		}

		public string GetClassName(int classbookId)
		{
			return dbContext.Classbooks.Where(x => x.Id.Equals(classbookId)).Select(x => x.Class.Name).FirstOrDefault();
		}
	}
}
