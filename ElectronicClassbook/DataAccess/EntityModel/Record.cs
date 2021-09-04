using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.EntityModel
{
	public class Record
	{
		public Record()
		{
			Attendances = new HashSet<Attendance>();
			Events = new HashSet<Event>();
		}
		[Key]
		public int Id { get; set; }

		/// <summary>
		/// Creation date
		/// </summary>
		[Required]
		public DateTime Created {get; set;}

		/// <summary>
		/// Sequence number of the class in the day
		/// </summary>
		[Required]
		public int SerialNumber { get; set; }

		[Required]
		public Subject Subject { get; set; }
		[Required]
		public int SubjectId { get; set; }


		[Required]
		[StringLength(500)]
		public string Topic { get; set; }

		/// <summary>
		/// In default all students are present
		/// </summary>
		[Required]
		public ICollection<Attendance> Attendances { get; set; }

		public ICollection<Event> Events { get; set; }

		/// <summary>
		/// Who created the record
		/// </summary>
		[Required]
		public Teacher CreatedBy { get; set; }
		[Required]
		public int TeacherId { get; set; }


		[Required]
		public Classbook Classbook { get; set; }
		[Required]
		public int ClassbookId { get; set; }
		[Required]
		public bool IsSubstituted { get; set; } = false;
	}
}
