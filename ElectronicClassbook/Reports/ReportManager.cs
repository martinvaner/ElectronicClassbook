using DataAccess.EntityModel;
using DataAccess.Repository.Interfaces;
using Reports.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports
{
	public class ReportManager : IReportManager
	{
		private readonly IReportsRepository reportsRepository;

		public ReportManager(IReportsRepository reportsRepository)
		{
			this.reportsRepository = reportsRepository;
		}

		public IEnumerable<Homework> GetTeachersHomeworksByDeadline(string email, DateTime deadline)
		{
			Teacher teacher = reportsRepository.GetTeacherByEmail(email);
			if (teacher == null) return new List<Homework>();

			//records mají created by -> vezmu všechny eventy, které jsou domácí úkoly a mají deadline = deadline
			var records = teacher.Records.Where(x => x.Events.Count > 0).Select(x => x.Events).ToList();
			List<Homework> homeworks = new List<Homework>();
			foreach (var ev in records)
			{
				foreach (var h in ev)
				{
					if (h is Homework)
					{
						var homework = h as Homework;
						if (homework.Deadline.Equals(deadline))
						{
							homeworks.Add(homework);
						}
					}
				}
			}

			return homeworks;
		}

		public IEnumerable<AbsentNote> GetAbsentNotesForApproval(string email)
		{
			//get class teacher
			//get class
			//iterate students in class
			// get their absentNotes where State = "Odeslána"

			ClassTeacher ct = reportsRepository.GetClassTeacherByEmail(email);

			if (ct != null)
			{
				List<AbsentNote> notes = new List<AbsentNote>();
				if (ct.ClassId == null)
					return new List<AbsentNote>();

				var students = reportsRepository.GetStudentInClass(ct.ClassId.Value).ToList();
				if (students != null && students.Count > 0)
				{
					foreach (var student in students)
					{
						var studentsNotes = reportsRepository.GetAbsentNotesByStudentId(student.Id).OrderBy(x => x.Student.FullName).ToList();
						if (studentsNotes != null && studentsNotes.Count > 0)
						{
							notes.AddRange(studentsNotes);
						}
					}

					return notes;
				}

			}

			return new List<AbsentNote>();
		}

		public IEnumerable<Student> GetStudentsByClassId(int classId)
		{
			return reportsRepository.GetStudentInClass(classId);
		}

		public IEnumerable<Class> GetAllClasses()
		{
			return reportsRepository.GetAllClasses();
		}

		public Student GetStudentById(int id)
		{
			return reportsRepository.GetStudentById(id);
		}

		public IEnumerable<DataAccess.EntityModel.Attendance> GetAbsenceByStudentId(int id)
		{
			return reportsRepository.GetAbsenceByStudentId(id);
		}

		public IEnumerable<Homework> GetHomeworksByStudentId(int id)
		{
			Class c = reportsRepository.GetClassByStudentId(id);

			return reportsRepository.GetHomeworksByClassId(c.Id);
		}

		public IEnumerable<SchoolHomeNote> GetSchoolHomeNotesByStudentId(int id)
		{
			return reportsRepository.GetSchoolHomeNotesByStudentId(id);
		}

		public Homework GetHomeworkById(int id)
		{
			return reportsRepository.GetHomeworkById(id);
		}
	}
}
