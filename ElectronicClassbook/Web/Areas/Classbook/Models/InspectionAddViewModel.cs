using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Classbook.Models
{
	public class InspectionAddViewModel
	{
		[Display(Name ="Zpráva")]
		[Required(ErrorMessage = "Povinná položka")]
		public string Text { get; set; }
		[Display(Name ="Hospitující")]
		[Required(ErrorMessage = "Povinná položka")]
		public string Auditor { get; set; }
	}
}
