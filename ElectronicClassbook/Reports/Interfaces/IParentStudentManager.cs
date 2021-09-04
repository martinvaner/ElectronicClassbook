using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reports.Interfaces
{
	public interface IParentStudentManager
	{
		Parent GetParentByEmail(string email);

		IEnumerable<Student> GetStudentsByParentId(int id);

		IEnumerable<Attendance> GetAbsencesByStudentId(int id);

		IEnumerable<Homework> GetHomeworksByStudentId(int id);

		IEnumerable<SchoolHomeNote> GetSchoolHomeNotesByStudentId(int id);

		Homework GetHomeworkById(int id);

		bool UpdateHomework(Homework homework);

		SchoolHomeNote GetSchoolHomeNoteById(int id);

		bool UpdateSchoolHomeNote(SchoolHomeNote note);

		IEnumerable<AbsentNote> GetAbsentNotesByStudentId(int studentId);

		Student GetStudentByEmail(string email);
	}
}
