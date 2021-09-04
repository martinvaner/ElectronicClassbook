using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.EntityModel
{
	public class Attendance
	{

		[Key]
		public int Id { get; set; }

		[Required]
		public int RecordId { get; set; }
		[Required]
		public Record Record { get; set; }

		[Required]
		public int StudentId { get; set; }
		[Required]
		public Student Student { get; set; }

		/// <summary>
		/// If student is present = true/1, if student is absent = false/0
		/// </summary>
		[Required]
		public bool Present { get; set; } = true;

		/// <summary>
		/// If student is excused by parents = true
		/// </summary>
		public bool IsExcused { get; set; } = false;

		/// <summary>
		/// If student absent, but come during class
		/// </summary>
		public bool LateArrival { get; set; } = false;

		/// <summary>
		/// If LateInterval is true, teacher should define AbsenceInterval
		/// </summary>
		public int AbsenceInterval { get; set; } = 0;


		public AbsentNote AbsentNote { get; set; }
		public int? AbsentNoteId { get; set; }
	}
}
