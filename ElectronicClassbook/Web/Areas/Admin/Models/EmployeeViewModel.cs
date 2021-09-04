using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web.Areas.Admin.Models.Submodels;

namespace Web.Areas.Admin.Models
{
	public class EmployeeViewModel
	{
		public int Id { get; set; }

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

		[Display(Name = "Titul před jménem")]
		public string TitleBefore { get; set; }
		[Display(Name = "Titul za jménem")]
		public string TitleAfter { get; set; }

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

		public RoleSubModel UserRoles { get; set; } = new RoleSubModel();

		[Display(Name = "Datum nástupu")]
		[Required(ErrorMessage = "Povinná položka")]
		public DateTime HireDate { get; set; }
		public SubjectSubModel Subjects { get; set; } = new SubjectSubModel();

		//Class teacher
		public ClassSubModel TeacherClass { get; set; } = new ClassSubModel();
	}
}
