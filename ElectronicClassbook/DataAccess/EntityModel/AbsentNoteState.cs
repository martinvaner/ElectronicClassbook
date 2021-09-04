using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.EntityModel
{
	/// <summary>
	/// Code list for absent-note states
	/// </summary>
	public class AbsentNoteState
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(150)]
		public string Name { get; set; }

		public ICollection<AbsentNote> AbsentNotes { get; set; }
	}
}
