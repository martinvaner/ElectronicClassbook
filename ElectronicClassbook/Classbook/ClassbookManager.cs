using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Classbook.Interfaces;
using DataAccess.EntityModel;
using DataAccess.Repository.Interfaces;
using jsreport.AspNetCore;
using jsreport.Types;

namespace Classbook
{
	public class ClassbookManager : IClassbookManager
	{
		private readonly IClassbookRepository classbookRepository;

		public ClassbookManager(IClassbookRepository classbookRepository)
		{
			this.classbookRepository = classbookRepository;
		}

		public User GetPrincipal()
		{
			return classbookRepository.GetPrincipal();
		}
		public IEnumerable<DataAccess.EntityModel.Classbook> GetAllClassbooks()
		{
			return classbookRepository.GetAllClassbooks();
		}

		public IEnumerable<Class> GetAllClasses()
		{
			return classbookRepository.GetAllClasses();
		}

		public bool CreateClassbook(DataAccess.EntityModel.Classbook c)
		{
			return classbookRepository.CreateClassbook(c);
		}

		public bool UpdateClassbook(DataAccess.EntityModel.Classbook c)
		{
			return classbookRepository.UpdateClassbook(c);
		}

		public bool DeleteClassbook(DataAccess.EntityModel.Classbook c)
		{
			return classbookRepository.DeleteClassbook(c);
		}

		public IEnumerable<Record> GetRecordsByDate(int classbookId, DateTime date)
		{
			return classbookRepository.GetRecordsByDate(classbookId, date);
		}

		public IEnumerable<Subject> GetAllSubjects()
		{
			return classbookRepository.GetAllSubjects();
		}

		public Teacher GetTeacherByEmail(string email)
		{
			return classbookRepository.GetTeacherByEmail(email);
		}

		public bool CreateRecord(Record record)
		{
			return classbookRepository.CreateRecord(record);
		}

		public Record GetRecordById(int recordId)
		{
			return classbookRepository.GetRecordById(recordId);
		}

		public Student GetStudentById(int id)
		{
			return classbookRepository.GetStudentById(id);
		}

		public ClassTeacher GetClassTeacherByEmail(string email)
		{
			return classbookRepository.GetClassTeacherByEmail(email);
		}
		public Attendance GetAttendance(int recordId, int studentId)
		{
			return classbookRepository.GetAttendance(recordId, studentId);
		}

		public bool UpdateAttendance(Attendance attendance)
		{
			return classbookRepository.UpdateAttendance(attendance);
		}

		public IEnumerable<Attendance> GetAttendancesByRecordId(int recordId)
		{
			return classbookRepository.GetAttendancesByRecordId(recordId);
		}

		public bool CreateInspection(Inspection inspection)
		{
			return classbookRepository.CreateInspection(inspection);
		}

		public bool CreateInstruction(Instruction instruction)
		{
			return classbookRepository.CreateInstruction(instruction);
		}

		public bool CreateHomework(Homework homework)
		{
			return classbookRepository.CreateHomework(homework);
		}

		public bool UpdateRecord(Record record)
		{
			return classbookRepository.UpdateRecord(record);
		}

		public IEnumerable<Inspection> GetInspectionsByClassbookId(int classbookId)
		{
			return classbookRepository.GetInspectionsByClassbookId(classbookId);
		}

		public Inspection GetInspectionById(int id)
		{
			return classbookRepository.GetInspectionById(id);
		}

		public bool DeleteInspection(Inspection inspection)
		{
			return classbookRepository.DeleteInspection(inspection);
		}

		public IEnumerable<Instruction> GetInstructionsByClassbookId(int classbookId)
		{
			return classbookRepository.GetInstructionsByClassbookId(classbookId);
		}

		public Instruction GetInstructionById(int id)
		{
			return classbookRepository.GetInstructionById(id);
		}

		public bool DeleteInstruction(Instruction instruction)
		{
			return classbookRepository.DeleteInstruction(instruction);
		}

		public IEnumerable<Student> GetStudentsByClassId(int id)
		{
			return classbookRepository.GetStudentsByClassId(id);
		}

		public bool CreateSchoolHomeNote(SchoolHomeNote note)
		{
			return classbookRepository.CreateSchoolHomeNote(note);
		}

		public IEnumerable<SchoolHomeNote> GetSchoolHomeNotesByStudentId(int studentId)
		{
			return classbookRepository.GetSchoolHomeNotesByStudentId(studentId);
		}

		public bool DeleteSchoolHomeNote(SchoolHomeNote note)
		{
			return classbookRepository.DeleteSchoolHomeNote(note);
		}

		public School GetSchool()
		{
			return classbookRepository.GetSchool();
		}
		public Class GetClassByClassbookId(int id)
		{
			return classbookRepository.GetAllClassbooks().Where(x => x.Id.Equals(id)).Select(x => x.Class).FirstOrDefault();
		}

		public IEnumerable<Record> GetRecords(int classbookId, DateTime from, DateTime to)
		{
			if (from > to)
				return null;

			return classbookRepository.GetRecords(classbookId, from, to);
		}

		public IEnumerable<Attendance> GetAbsencesByDate(int classbookId, DateTime date)
		{
			return classbookRepository.GetAbsencesByDate(classbookId, date);
		}

		public IEnumerable<Subject> GetClassSubjects(int classId)
		{
			return classbookRepository.GetClassSubjects(classId);
		}

		public IEnumerable<Homework> GetHomeworksByClassbookId(int id)
		{
			return classbookRepository.GetHomeworksByClassbookId(id);
		}

		public Homework GetHomeworkById(int id)
		{
			return classbookRepository.GetHomeworkById(id);
		}

		public string GetClassName(int classbookId)
		{
			return classbookRepository.GetClassName(classbookId);
		}
	}
}
