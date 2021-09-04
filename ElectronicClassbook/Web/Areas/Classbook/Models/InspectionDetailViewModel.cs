using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Classbook.Models
{
	public class InspectionDetailViewModel
	{
		[Display(Name ="Datum hospitace")]
		public DateTime Date { get; set; }
		[Display(Name = "Hospitující")]
		public string Auditor { get; set; }
		[Display(Name = "Proběhla u předmětu")]
		public Subject Subject { get; set; }
		[Display(Name = "Zápis")]
		public string Text { get; set; }
	}
}
