using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Classbook.Models
{
	public class InstructionAddViewModel
	{
		public Teacher Author { get; set; }
		[Display(Name ="Název")]
		[Required(ErrorMessage = "Povinná položka")]
		public string Title { get; set; }
		[Display(Name ="Popis")]
		[Required(ErrorMessage = "Povinná položka")]
		public string Text { get; set; }
	}
}
