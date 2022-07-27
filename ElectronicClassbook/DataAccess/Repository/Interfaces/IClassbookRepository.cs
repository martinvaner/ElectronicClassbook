using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repository.Interfaces
{
	public interface IClassbookRepository
	{

		/// <summary>
		/// Get school info
		/// </summary>
		/// <returns></returns>
		School GetSchool();

		/// <summary>
		/// Get principal of the school
		/// </summary>
		/// <returns></returns>
		User GetPrincipal();
		/// <summary>
		/// Gets all classbooks
		/// </summary>
		/// <returns></returns>
		IEnumerable<Classbook> GetAllClassbooks();

		/// <summary>
		/// Gets all classes
		/// </summary>
		/// <returns></returns>
		IEnumerable<Class> GetAllClasses();

		/// <summary>
		/// Creates new classbook
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		bool CreateClassbook(DataAccess.EntityModel.Classbook c);

		/// <summary>
		/// Update classbook information
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		bool UpdateClassbook(DataAccess.EntityModel.Classbook c);

		/// <summary>
		/// Delete classbook
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		bool DeleteClassbook(DataAccess.EntityModel.Classbook c);

		/// <summary>
		/// Gets records by classbook Id and date
		/// </summary>
		/// <param name="classbookId">Id of classbook</param>
		/// <param name="date"></param>
		/// <returns></returns>
		IEnumerable<Record> GetRecordsByDate(int classbookId, DateTime date);

		/// <summary>
		/// Gets all subjects
		/// </summary>
		/// <returns></returns>
		IEnumerable<Subject> GetAllSubjects();

		/// <summary>
		/// Gets teacher by its email
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		Teacher GetTeacherByEmail(string email);

		/// <summary>
		/// Gets class teacher by its email
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		ClassTeacher GetClassTeacherByEmail(string email);

		/// <summary>
		/// Creates record in classbook
		/// </summary>
		/// <param name="record"></param>
		/// <returns></returns>
		bool CreateRecord(Record record);

		/// <summary>
		/// Get record by its id
		/// </summary>
		/// <param name="recordId"></param>
		/// <returns></returns>
		Record GetRecordById(int recordId);

		/// <summary>
		/// Get student by its id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Student GetStudentById(int id);

		/// <summary>
		/// Get attendance by record id and student id
		/// </summary>
		/// <param name="recordId"></param>
		/// <param name="studentId"></param>
		/// <returns></returns>
		Attendance GetAttendance(int recordId, int studentId);

		/// <summary>
		/// Get attendances by record id
		/// </summary>
		/// <param name="recordId"></param>
		/// <returns></returns>
		IEnumerable<Attendance> GetAttendancesByRecordId(int recordId);

		/// <summary>
		/// Update attendance
		/// </summary>
		/// <param name="attendance"></param>
		bool UpdateAttendance(Attendance attendance);

		/// <summary>
		/// Create new inspection
		/// </summary>
		/// <param name="inspection"></param>
		/// <returns></returns>
		bool CreateInspection(Inspection inspection);

		/// <summary>
		/// Create new instruction
		/// </summary>
		/// <param name="instruction"></param>
		/// <returns></returns>
		bool CreateInstruction(Instruction instruction);

		/// <summary>
		/// Create new homework
		/// </summary>
		/// <param name="homework"></param>
		/// <returns></returns>
		bool CreateHomework(Homework homework);

		/// <summary>
		/// Update record
		/// </summary>
		/// <param name="record"></param>
		/// <returns></returns>
		bool UpdateRecord(Record record);

		/// <summary>
		/// Gets all inspections in classbook
		/// </summary>
		/// <param name="classbookId"></param>
		/// <returns></returns>
		IEnumerable<Inspection> GetInspectionsByClassbookId(int classbookId);

		/// <summary>
		/// Get inspection by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Inspection GetInspectionById(int id);

		/// <summary>
		/// Delete inspection
		/// </summary>
		/// <param name="inspection"></param>
		/// <returns></returns>
		bool DeleteInspection(Inspection inspection);

		/// <summary>
		/// Get all instructions by classbook id
		/// </summary>
		/// <param name="classbookId"></param>
		/// <returns></returns>
		IEnumerable<Instruction> GetInstructionsByClassbookId(int classbookId);

		/// <summary>
		/// Get instruction by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Instruction GetInstructionById(int id);

		/// <summary>
		/// Delete instruction
		/// </summary>
		/// <param name="instruction"></param>
		/// <returns></returns>
		bool DeleteInstruction(Instruction instruction);

		/// <summary>
		/// Get all students in class
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		IEnumerable<Student> GetStudentsByClassId(int id);

		/// <summary>
		/// Create school-home note
		/// </summary>
		/// <param name="note"></param>
		/// <returns></returns>
		bool CreateSchoolHomeNote(SchoolHomeNote note);

		/// <summary>
		/// Get school-home notes by student's id
		/// </summary>
		/// <param name="studentId"></param>
		/// <returns></returns>
		IEnumerable<SchoolHomeNote> GetSchoolHomeNotesByStudentId(int studentId);

		/// <summary>
		/// Delete school-home note
		/// </summary>
		/// <param name="note"></param>
		/// <returns></returns>
		bool DeleteSchoolHomeNote(SchoolHomeNote note);

		/// <summary>
		/// Get records by classbook id and date interval
		/// </summary>
		/// <param name="classbookId"></param>
		/// <param name="from"></param>
		/// <param name="to"></param>
		/// <returns></returns>
		IEnumerable<Record> GetRecords(int classbookId, DateTime from, DateTime to);

		/// <summary>
		/// Get list of absences in given date
		/// </summary>
		/// <param name="classbookId"></param>
		/// <param name="date"></param>
		/// <returns></returns>
		IEnumerable<Attendance> GetAbsencesByDate(int classbookId, DateTime date);

		/// <summary>
		/// Get class's subjects
		/// </summary>
		/// <param name="classId"></param>
		/// <returns></returns>
		IEnumerable<Subject> GetClassSubjects(int classId);

		/// <summary>
		/// Get list of homeworks by class id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		IEnumerable<Homework> GetHomeworksByClassbookId(int id);

		/// <summary>
		/// Get homework by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Homework GetHomeworkById(int id);

		/// <summary>
		/// Get class name by classbook id
		/// </summary>
		/// <param name="classbookId"></param>
		/// <returns></returns>
		string GetClassName(int classbookId);

	}
}
