using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repository.Interfaces
{
	public interface IReportsRepository : IDisposable
	{
		/// <summary>
		/// Get parent by email
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		Parent GetParentByEmail(string email);

		/// <summary>
		/// Get list of students by their parent's id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		IEnumerable<Student> GetStudentsByParentId(int id);

		/// <summary>
		/// Get student's absences by student's id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		IEnumerable<Attendance> GetAbsenceByStudentId(int id);

		/// <summary>
		/// Get student's homework by class's id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		IEnumerable<Homework> GetHomeworksByClassId(int id);

		/// <summary>
		/// Get student's school-home notes by student's id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		IEnumerable<SchoolHomeNote> GetSchoolHomeNotesByStudentId(int id);

		/// <summary>
		/// Get class by student's id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Class GetClassByStudentId(int id);

		/// <summary>
		/// Get homework by homework id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Homework GetHomeworkById(int id);

		/// <summary>
		/// Update homework
		/// </summary>
		/// <param name="homework"></param>
		/// <returns></returns>
		bool UpdateHomework(Homework homework);

		/// <summary>
		/// Get school-home note by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		SchoolHomeNote GetSchoolHomeNoteById(int id);

		/// <summary>
		/// Update school-home note
		/// </summary>
		/// <param name="note"></param>
		/// <returns></returns>
		bool UpdateSchoolHomeNote(SchoolHomeNote note);

		/// <summary>
		/// Get parent's absent notes
		/// </summary>
		/// <param name="studentId"></param>
		/// <returns></returns>
		IEnumerable<AbsentNote> GetAbsentNotesByStudentId(int studentId);

		/// <summary>
		/// Get teacher by email
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		Teacher GetTeacherByEmail(string email);

		/// <summary>
		/// Get class teacher by class teacher's email
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		ClassTeacher GetClassTeacherByEmail(string email);

		/// <summary>
		/// Get students in class
		/// </summary>
		/// <param name="classId"></param>
		/// <returns></returns>
		IEnumerable<Student> GetStudentInClass(int classId);

		/// <summary>
		/// Get student's absent notes in state "Odeslána"
		/// </summary>
		/// <param name="studentId"></param>
		/// <returns></returns>
		IEnumerable<AbsentNote> GetNewAbsentNotesByStudentId(int studentId);

		/// <summary>
		/// Get student by email
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		Student GetStudentByEmail(string email);

		/// <summary>
		/// Get list of classes
		/// </summary>
		/// <returns></returns>
		IEnumerable<Class> GetAllClasses();

		/// <summary>
		/// Get student by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Student GetStudentById(int id);
	}
}
