using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.EntityModel;

namespace Classbook.Interfaces
{
	public interface IClassbookManager
	{
		IEnumerable<DataAccess.EntityModel.Classbook> GetAllClassbooks();

		IEnumerable<Class> GetAllClasses();

		bool CreateClassbook(DataAccess.EntityModel.Classbook c);

		bool UpdateClassbook(DataAccess.EntityModel.Classbook c);

		bool DeleteClassbook(DataAccess.EntityModel.Classbook c);

		IEnumerable<Record> GetRecordsByDate(int classbookId, DateTime date);

		IEnumerable<Subject> GetAllSubjects();

		Teacher GetTeacherByEmail(string email);

		bool CreateRecord(Record record);

		Record GetRecordById(int recordId);

		Student GetStudentById(int id);

		ClassTeacher GetClassTeacherByEmail(string email);

		Attendance GetAttendance(int recordId, int studentId);

		bool UpdateAttendance(Attendance attendance);

		IEnumerable<Attendance> GetAttendancesByRecordId(int recordId);

		bool CreateInspection(Inspection inspection);

		bool CreateInstruction(Instruction instruction);

		bool CreateHomework(Homework homework);

		bool UpdateRecord(Record record);

		IEnumerable<Inspection> GetInspectionsByClassbookId(int classbookId);

		Inspection GetInspectionById(int id);

		bool DeleteInspection(Inspection inspection);

		IEnumerable<Instruction> GetInstructionsByClassbookId(int classbookId);

		Instruction GetInstructionById(int id);

		bool DeleteInstruction(Instruction instruction);

		IEnumerable<Student> GetStudentsByClassId(int id);

		bool CreateSchoolHomeNote(SchoolHomeNote note);

		IEnumerable<SchoolHomeNote> GetSchoolHomeNotesByStudentId(int studentId);

		bool DeleteSchoolHomeNote(SchoolHomeNote note);

		School GetSchool();
		Class GetClassByClassbookId(int id);

		IEnumerable<Record> GetRecords(int classbookId, DateTime from, DateTime to);

		IEnumerable<Attendance> GetAbsencesByDate(int classbookId, DateTime date);

		IEnumerable<Subject> GetClassSubjects(int classId);

		User GetPrincipal();

		IEnumerable<Homework> GetHomeworksByClassbookId(int id);

		Homework GetHomeworkById(int id);

		string GetClassName(int classbookId);
	}
}
