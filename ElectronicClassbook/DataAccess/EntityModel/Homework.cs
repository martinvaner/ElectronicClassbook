using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.EntityModel
{
	public class Homework : Event
	{
		[Required]
		[StringLength(250)]
		public string Title { get; set; }
		[Required]
		public string Text { get; set; }
		[Required]
		public DateTime Deadline { get; set; }
		[Required]
		public bool ShowParent { get; set; } = true;
		[Required]
		public bool ShowStudent { get; set; } = true;
	}
}
