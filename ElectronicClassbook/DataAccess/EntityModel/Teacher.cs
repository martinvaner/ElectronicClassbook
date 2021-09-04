using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.EntityModel
{
	public class Teacher : User
	{
		public Teacher()
		{
			TeacherSubjects = new HashSet<TeacherSubject>();
			SchoolHomeNotes = new HashSet<SchoolHomeNote>();
			Records = new HashSet<Record>();
		}
		[Required]
		public DateTime HireDate { get; set; }
		public ICollection<TeacherSubject> TeacherSubjects { get; set; }
		public ICollection<Record> Records { get; set; }

		public ICollection<SchoolHomeNote> SchoolHomeNotes { get; set; }
	}
}
