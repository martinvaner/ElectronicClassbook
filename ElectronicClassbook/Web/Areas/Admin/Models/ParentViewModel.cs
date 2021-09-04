using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web.Areas.Admin.Models.Submodels;

namespace Web.Areas.Admin.Models
{
	public class ParentViewModel
	{
		[Display(Name = "Jméno")]
		[Required(ErrorMessage = "Povinná položka")]
		public string FirstName { get; set; }
		[Display(Name = "Příjmení")]
		[Required(ErrorMessage = "Povinná položka")]
		public string LastName { get; set; }
		[Display(Name = "E-mail")]
		[Required(ErrorMessage = "Povinná položka")]
		[EmailAddress]
		public string Email { get; set; }
		[Display(Name = "Adresa")]
		[Required(ErrorMessage = "Povinná položka")]
		public string Address { get; set; }
		[Display(Name = "Heslo")]
		public string Password { get; set; }

		[Display(Name = "Telefonní číslo")]
		[Required(ErrorMessage = "Povinná položka")]
		[RegularExpression(@"^[0-9]+$", ErrorMessage = "Zadejte pouze čísla.")]
		public string PhoneNumber { get; set; }
		public StudentSubModel Childrens { get; set; } = new StudentSubModel();
	}
}
