using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web.Areas.Admin.Models.Submodels;

namespace Web.Areas.Admin.Models
{
	public class StudentViewModel
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

		[Display(Name = "Rodné číslo")]
		[Required(ErrorMessage = "Povinná položka")]
		[RegularExpression(@"^[0-9]+$", ErrorMessage = "Zadejte pouze čísla.")]
		public string PersonalIdentificationNumber { get; set; }

		[Required(ErrorMessage = "Povinná položka")]
		public GenderSubModel Gender { get; set; } = new GenderSubModel();

		[Display(Name = "Datum narození")]
		[Required(ErrorMessage = "Povinná položka")]
		public DateTime? DateOfBirth { get; set; }
		[Display(Name = "Místo narození")]
		[Required(ErrorMessage = "Povinná položka")]
		public string BirthLocation { get; set; }
		[Display(Name = "Státní občanství")]
		[Required(ErrorMessage = "Povinná položka")]
		public string Citizenship { get; set; }

		[Display(Name = "Začátek studia")]
		[Required(ErrorMessage = "Povinná položka")]
		public DateTime EnrollmentDate { get; set; }
		[Display(Name = "Ukončení studia")]
		public DateTime? CompletionDate { get; set; }
		public ParentSubModel Parents { get; set; } = new ParentSubModel();
	}
}
