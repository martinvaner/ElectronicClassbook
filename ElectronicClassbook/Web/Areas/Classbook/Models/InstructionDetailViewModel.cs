using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Classbook.Models
{
	public class InstructionDetailViewModel
	{
		[Display(Name ="Datum poučení")]
		public DateTime Date { get; set; }
		[Display(Name = "Název")]
		public string Title { get; set; }
		[Display(Name = "Autor poučení")]
		public User Author { get; set; }
		[Display(Name = "Popis")]
		public string Text { get; set; }
	}
}
