using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Classbook.Models
{
	public class ExportClassbookViewModel
	{
		[Display(Name = "Třída")]
		public string ClassName { get; set; }
		[Display(Name ="Od")]
		[Required(ErrorMessage = "Povinná položka")]
		public DateTime? From { get; set; }
		[Display(Name ="Do")]
		[Required(ErrorMessage = "Povinná položka")]
		public DateTime? To { get; set; }
	}
}
