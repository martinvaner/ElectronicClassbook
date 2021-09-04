using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.EntityModel
{
	public class Student : User
	{
		public Student()
		{
			ParentStudents = new HashSet<ParentStudent>();
			Attendances = new HashSet<Attendance>();
			SchoolHomeNotes = new HashSet<SchoolHomeNote>();
		}
		[Required]
		public DateTime EnrollmentDate { get; set; }

		public DateTime? CompletionDate { get; set; }

		public Class Class { get; set; }

		public ICollection<ParentStudent> ParentStudents { get; set; }

		public ICollection<Attendance> Attendances { get; set; }

		public ICollection<SchoolHomeNote> SchoolHomeNotes { get; set; }
		public ICollection<AbsentNote> AbsentNotes { get; set; }

	}
}
