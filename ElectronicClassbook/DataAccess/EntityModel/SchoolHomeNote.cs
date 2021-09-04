using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.EntityModel
{
	public class SchoolHomeNote
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public DateTime Created { get; set; }
		[Required]
		public Student Student { get; set; }
		[Required]
		public int StudentId { get; set; }

		[Required]
		public Teacher CreatedBy { get; set; }
		[Required]
		public int TeacherId { get; set; }

		[Required]
		public string Note { get; set; }

		[Required]
		public bool ShowParent { get; set; } = true;
		[Required]
		public bool ShowStudent { get; set; } = true;
	}
}
