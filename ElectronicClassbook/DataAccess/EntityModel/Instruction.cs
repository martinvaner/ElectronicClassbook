using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.EntityModel
{
	public class Instruction : Event
	{
		[Required]
		public User Author { get; set; }
		[Required]
		[StringLength(250)]
		public string Title { get; set; }
		[Required]
		public string Text { get; set; }
	}
}
