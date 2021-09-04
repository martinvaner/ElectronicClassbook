using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Interfaces
{
	public interface IClassTeacherManager
	{
		IEnumerable<AbsentNote> GetAbsentNotesByClassTeacherEmail(string email);

		AbsentNote GetAbsentNoteById(int id);
		AbsentNoteState GetAbsentNoteStateByName(string name);

		bool UpdateAbsentNote(AbsentNote note);

		IEnumerable<Student> GetStudentsByClassTeacherEmail(string email);

		Student GetStudentById(int id);

		IEnumerable<DataAccess.EntityModel.Attendance> GetAbsenceByStudentId(int id);

		ClassTeacher GetClassTeacherByEmail(string email);

		DataAccess.EntityModel.Attendance GetAttendanceById(int id);

		bool CreateAbsentNote(AbsentNote note);
	}
}
