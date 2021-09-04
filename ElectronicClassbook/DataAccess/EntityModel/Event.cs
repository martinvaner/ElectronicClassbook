using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.EntityModel
{
	public class Event
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public Record Record { get; set; }
		[Required]
		public int RecordId { get; set; }

	}
}
