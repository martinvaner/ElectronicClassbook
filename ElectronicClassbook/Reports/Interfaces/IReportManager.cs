using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reports.Interfaces
{
	public interface IReportManager
	{
		IEnumerable<Homework> GetTeachersHomeworksByDeadline(string email, DateTime deadline);

		IEnumerable<AbsentNote> GetAbsentNotesForApproval(string email);

		IEnumerable<Student> GetStudentsByClassId(int classId);

		IEnumerable<Class> GetAllClasses();

		Student GetStudentById(int id);

		IEnumerable<DataAccess.EntityModel.Attendance> GetAbsenceByStudentId(int id);

		IEnumerable<Homework> GetHomeworksByStudentId(int id);

		IEnumerable<SchoolHomeNote> GetSchoolHomeNotesByStudentId(int id);

		Homework GetHomeworkById(int id);
	}
}
