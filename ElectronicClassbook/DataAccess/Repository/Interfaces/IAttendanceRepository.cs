using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repository.Interfaces
{
	public interface IAttendanceRepository
	{
		/// <summary>
		/// Get parent by email
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		Parent GetParentByEmail(string email);

		/// <summary>
		/// Get student's absences by student's id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		IEnumerable<Attendance> GetAbsenceByStudentId(int id);

		/// <summary>
		/// Get list of students by their parent's id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		IEnumerable<Student> GetStudentsByParentId(int id);

		/// <summary>
		/// Get absent-note state by name
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		AbsentNoteState GetAbsentNoteStateByName(string name);

		/// <summary>
		/// Get attendance by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Attendance GetAttendanceById(int id);

		/// <summary>
		/// Create absent note
		/// </summary>
		/// <param name="note"></param>
		/// <returns></returns>
		bool CreateAbsentNote(AbsentNote note);

		/// <summary>
		/// Get student by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Student GetStudentById(int id);

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
		/// Get absent note by id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		AbsentNote GetAbsentNoteById(int id);

		/// <summary>
		/// Update absent note data
		/// </summary>
		/// <param name="note"></param>
		/// <returns></returns>
		bool UpdateAbsentNote(AbsentNote note);
	}
}
