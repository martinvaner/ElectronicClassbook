using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Classbook.Models
{
	public class HomeworkAddViewModel
	{
		[Display(Name ="Název")]
		[Required(ErrorMessage = "Povinná položka")]
		public string Title { get; set; }
		[Display(Name = "Zadání")]
		[Required(ErrorMessage = "Povinná položka")]
		public string Text { get; set; }
		[Display(Name = "Termín odevzdání")]
		[Required(ErrorMessage = "Povinná položka")]
		public DateTime? Deadline { get; set; }
	}
}
