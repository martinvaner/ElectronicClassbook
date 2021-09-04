using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.EntityModel
{
	public class AbsentNote
	{

		public AbsentNote()
		{
			Absences = new HashSet<Attendance>();
		}

		[Key]
		public int Id { get; set; }

		[Required]
		public string Text { get; set; }

		[Required]
		public DateTime Created { get; set; }
		[Required]
		public IEnumerable<Attendance> Absences { get; set; }

		/// <summary>
		/// Absent note can be created by parent and class teacher. Class teacher can get absent note on paper because not every parent has PC at home.
		/// </summary>
		/*public User CreatedBy { get; set; }
		public int CreatedById { get; set; }*/

		public Parent Parent { get; set; }
		public int? ParentId { get; set; }
		public ClassTeacher ClassTeacher { get; set; }
		public int? ClassTeacherId { get; set; }

		[Required]
		public AbsentNoteState State { get; set; }
		[Required]
		public int StateId { get; set; }
		[Required]
		public Student Student { get; set; }
		[Required]
		public int StudentId { get; set; }

	}
}
