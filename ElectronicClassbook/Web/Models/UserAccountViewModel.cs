using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
	public class UserAccountViewModel
	{
		[Display(Name = "Jméno")]
		public string FirstName { get; set; }
		[Display(Name = "Příjmení")]
		public string LastName { get; set; }
		[Display(Name = "E-mail")]
		public string Email { get; set; }
		[Display(Name = "Přiřazené role")]
		public List<Role> Roles { get; set; }
		[Display(Name = "Zůstatek")]
		public double Balance { get; set; }
	}
}
