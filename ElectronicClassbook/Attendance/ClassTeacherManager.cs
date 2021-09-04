using Attendance.Interfaces;
using DataAccess.EntityModel;
using DataAccess.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Attendance
{
	public class ClassTeacherManager : IClassTeacherManager
	{
		private readonly IAttendanceRepository attendanceRepository;
		public ClassTeacherManager(IAttendanceRepository attendanceRepository)
		{
			this.attendanceRepository = attendanceRepository;
		}

		public IEnumerable<AbsentNote> GetAbsentNotesByClassTeacherEmail(string email)
		{
			//get class teacher
			//get class
			//iterate students in class
			// get their absentNotes where State = "Odeslána"

			ClassTeacher ct = attendanceRepository.GetClassTeacherByEmail(email);
			
			if(ct != null)
			{
				List<AbsentNote> notes = new List<AbsentNote>();
				if (ct.ClassId == null)
					return new List<AbsentNote>();

				var students = attendanceRepository.GetStudentInClass(ct.ClassId.Value).ToList();
				if(students != null && students.Count > 0)
				{
					foreach(var student in students)
					{
						var studentsNotes = attendanceRepository.GetNewAbsentNotesByStudentId(student.Id).OrderBy(x => x.Student.FullName).ToList();
						if(studentsNotes != null && studentsNotes.Count > 0)
						{
							notes.AddRange(studentsNotes);
						}
					}

					return notes;
				}

			}

			return new List<AbsentNote>();

		}

		public IEnumerable<Student> GetStudentsByClassTeacherEmail(string email)
		{

			ClassTeacher ct = attendanceRepository.GetClassTeacherByEmail(email);

			if(ct != null && ct.ClassId != null)
			{
				return attendanceRepository.GetStudentInClass(ct.ClassId.Value).ToList();
			}

			return new List<Student>();
		}

		public AbsentNote GetAbsentNoteById(int id)
		{
			return attendanceRepository.GetAbsentNoteById(id);
		}

		public AbsentNoteState GetAbsentNoteStateByName(string name)
		{
			return attendanceRepository.GetAbsentNoteStateByName(name);
		}

		public bool UpdateAbsentNote(AbsentNote note)
		{
			return attendanceRepository.UpdateAbsentNote(note);
		}

		public Student GetStudentById(int id)
		{
			return attendanceRepository.GetStudentById(id);
		}

		public IEnumerable<DataAccess.EntityModel.Attendance> GetAbsenceByStudentId(int id)
		{
			return attendanceRepository.GetAbsenceByStudentId(id);
		}

		public ClassTeacher GetClassTeacherByEmail(string email)
		{
			return attendanceRepository.GetClassTeacherByEmail(email);
		}

		public DataAccess.EntityModel.Attendance GetAttendanceById(int id)
		{
			return attendanceRepository.GetAttendanceById(id);
		}

		public bool CreateAbsentNote(AbsentNote note)
		{
			return attendanceRepository.CreateAbsentNote(note);
		}

	}
}
