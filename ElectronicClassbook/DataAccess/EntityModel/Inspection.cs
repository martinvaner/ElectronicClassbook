using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.EntityModel
{
	public class Inspection : Event
	{
		[Required]
		public string Text { get; set; }

		[Required]
		[StringLength(250)]
		public string Auditor { get; set; }

	}
}
