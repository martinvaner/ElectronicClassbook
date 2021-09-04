using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Classbook.Models
{
	public class StudentListViewModel
	{
		public int Id { get; set; }
		[Display(Name ="Jméno")]
		public string FirstName { get; set; }
		[Display(Name = "Příjmení")]
		public string LastName { get; set; }
		[Display(Name = "E-mail")]
		public string Email { get; set; }
	}
}
