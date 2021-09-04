using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Interfaces
{
	public interface IParentManager
	{
		Parent GetParentByEmail(string email);

		IEnumerable<DataAccess.EntityModel.Attendance> GetAbsenceByStudentId(int id);

		IEnumerable<Student> GetStudentsByParentId(int id);

		AbsentNoteState GetAbsentNoteStateByName(string name);

		DataAccess.EntityModel.Attendance GetAttendanceById(int id);
		bool CreateAbsentNote(AbsentNote note);

		Student GetStudentById(int id);
	}
}
