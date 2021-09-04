using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Classbook.Models
{
	public class RecordListViewModel
	{
		public int Id { get; set; }

		public DateTime Created { get; set; }

		[Display(Name ="Hodina")]
		public int SerialNumber { get; set; }
		[Display(Name = "Předmět")]
		public Subject Subject { get; set; }
		[Display(Name = "Téma")]
		public string Topic { get; set; }
		[Display(Name = "Učitel")]
		public Teacher CreatedBy { get; set; }

		public bool IsSubstituted { get; set; }

		public DataAccess.EntityModel.Classbook Classbook { get; set; }
	}
}
