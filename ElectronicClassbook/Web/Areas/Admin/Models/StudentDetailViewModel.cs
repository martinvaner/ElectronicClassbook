using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Admin.Models
{
	public class StudentDetailViewModel
	{

		[Display(Name = "Jméno")]
		public string FirstName { get; set; }
		[Display(Name = "Příjmení")]
		public string LastName { get; set; }
		[Display(Name = "E-mail")]
		public string Email { get; set; }
		[Display(Name = "Adresa")]
		public string Address { get; set; }

		[Display(Name = "Rodné číslo")]
		public string PersonalIdentificationNumber { get; set; }
		[Display(Name ="Pohlaví")]
		public bool? Gender { get; set; }

		[Display(Name = "Datum narození")]
		public DateTime? DateOfBirth { get; set; }
		[Display(Name = "Místo narození")]
		public string BirthLocation { get; set; }
		[Display(Name = "Státní občanství")]
		public string Citizenship { get; set; }

		[Display(Name = "Začátek studia")]
		public DateTime EnrollmentDate { get; set; }
		[Display(Name = "Ukončení studia")]
		public DateTime? CompletionDate { get; set; }
		[Display(Name = "Rodiče")]
		public List<Parent> Parents { get; set; } = new List<Parent>();
	}
}
