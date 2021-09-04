using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Classbook.Models
{
	public class SchoolHomeNoteViewModel
	{
		public int Id { get; set; }
		[Display(Name ="Datum")]
		[Required(ErrorMessage = "Povinná položka")]
		public DateTime Created { get; set; }
		[Display(Name = "Žák")]
		public Student Student { get; set; }
		public int StudentId { get; set; }
		[Display(Name = "Vytvořil")]
		public Teacher CreatedBy { get; set; }
		public int TeacherId { get; set; }

		[Display(Name = "Poznámka")]
		[Required(ErrorMessage = "Povinná položka")]
		public string Note { get; set; }
	}
}
