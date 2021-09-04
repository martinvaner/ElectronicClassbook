using DataAccess.EntityModel;
using DataAccess.Repository.Interfaces;
using Reports.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reports
{
	public class ParentStudentManager : IParentStudentManager
	{
		private readonly IReportsRepository reportsRepository;

		public ParentStudentManager(IReportsRepository reportsRepository)
		{
			this.reportsRepository = reportsRepository;
		}

		public IEnumerable<Attendance> GetAbsencesByStudentId(int id)
		{
			return reportsRepository.GetAbsenceByStudentId(id);
		}

		public IEnumerable<Homework> GetHomeworksByStudentId(int id)
		{
			//get class id
			Class c = reportsRepository.GetClassByStudentId(id);
			if(c != null)
				return reportsRepository.GetHomeworksByClassId(c.Id);

			return new List<Homework>();
		}

		public Parent GetParentByEmail(string email)
		{
			return reportsRepository.GetParentByEmail(email);
		}

		public IEnumerable<SchoolHomeNote> GetSchoolHomeNotesByStudentId(int id)
		{
			return reportsRepository.GetSchoolHomeNotesByStudentId(id);
		}

		public IEnumerable<Student> GetStudentsByParentId(int id)
		{
			return reportsRepository.GetStudentsByParentId(id);
		}

		public Homework GetHomeworkById(int id)
		{
			return reportsRepository.GetHomeworkById(id);
		}

		public bool UpdateHomework(Homework homework)
		{
			return reportsRepository.UpdateHomework(homework);
		}

		public SchoolHomeNote GetSchoolHomeNoteById(int id)
		{
			return reportsRepository.GetSchoolHomeNoteById(id);
		}

		public bool UpdateSchoolHomeNote(SchoolHomeNote note)
		{
			return reportsRepository.UpdateSchoolHomeNote(note);
		}

		public IEnumerable<AbsentNote> GetAbsentNotesByStudentId(int studentId)
		{
			return reportsRepository.GetAbsentNotesByStudentId(studentId);
		}

		public Student GetStudentByEmail(string email)
		{
			return reportsRepository.GetStudentByEmail(email);
		}
	}
}
