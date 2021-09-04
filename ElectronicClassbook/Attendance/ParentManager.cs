using Attendance.Interfaces;
using DataAccess.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.EntityModel;

namespace Attendance
{
	public class ParentManager : IParentManager
	{
		private readonly IAttendanceRepository attendanceRepository;

		public ParentManager(IAttendanceRepository attendanceRepository)
		{
			this.attendanceRepository = attendanceRepository;
		}

		public IEnumerable<DataAccess.EntityModel.Attendance> GetAbsenceByStudentId(int id)
		{
			return attendanceRepository.GetAbsenceByStudentId(id);
		}

		public Parent GetParentByEmail(string email)
		{
			return attendanceRepository.GetParentByEmail(email);
		}

		public IEnumerable<Student> GetStudentsByParentId(int id)
		{
			return attendanceRepository.GetStudentsByParentId(id);
		}

		public AbsentNoteState GetAbsentNoteStateByName(string name)
		{
			return attendanceRepository.GetAbsentNoteStateByName(name);
		}

		public DataAccess.EntityModel.Attendance GetAttendanceById(int id)
		{
			return attendanceRepository.GetAttendanceById(id);
		}

		public bool CreateAbsentNote(AbsentNote note)
		{
			return attendanceRepository.CreateAbsentNote(note);
		}

		public Student GetStudentById(int id)
		{
			return attendanceRepository.GetStudentById(id);
		}
	}
}
