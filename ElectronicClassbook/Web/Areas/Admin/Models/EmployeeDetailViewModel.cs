using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Admin.Models
{
	public class EmployeeDetailViewModel
	{
		[Display(Name = "Jméno")]
		public string FirstName { get; set; }
		[Display(Name = "Příjmení")]
		public string LastName { get; set; }
		[Display(Name = "E-mail")]
		public string Email { get; set; }
		[Display(Name = "Adresa")]
		public string Address { get; set; }


		[Display(Name = "Titul před jménem")]
		public string TitleBefore { get; set; }
		[Display(Name = "Titul za jménem")]
		public string TitleAfter { get; set; }

		[Display(Name = "Rodné číslo")]
		public string PersonalIdentificationNumber { get; set; }
		[Display(Name = "Pohlaví")]
		public bool? Gender { get; set; } 

		[Display(Name = "Datum narození")]
		public DateTime? DateOfBirth { get; set; }
		[Display(Name = "Místo narození")]
		public string BirthLocation { get; set; }
		[Display(Name = "Státní občanství")]
		public string Citizenship { get; set; }
		[Display(Name = "Role")]
		public List<Role> Roles { get; set; } = new List<Role>();

		[Display(Name = "Datum nástupu")]
		public DateTime HireDate { get; set; }
		[Display(Name = "Vyučované předměty")]
		public List<Subject> Subjects { get; set; } = new List<Subject>();

		//Class teacher
		[Display(Name = "Třídní učitel třídy")]
		public Class TeacherClass { get; set; } = new Class();
	}
}
