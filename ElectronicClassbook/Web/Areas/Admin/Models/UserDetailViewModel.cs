using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Admin.Models
{
	public class UserDetailViewModel
	{
		[Display(Name = "Jméno")]
		public string FirstName { get; set; }
		[Display(Name = "Příjmení")]
		public string LastName { get; set; }
		[Display(Name = "E-mail")]
		public string Email { get; set; }
		[Display(Name = "Adresa")]
		public string Address { get; set; }

		[Display(Name ="Role uživatele")]
		public List<Role> Roles { get; set; } = new List<Role>();
		[Display(Name ="Telefonní číslo")]
		public string PhoneNumber { get; set; }

		[Display(Name ="Děti")]
		public List<Student> Students { get; set; } = new List<Student>();

		[Display(Name = "Začátek studia")]
		public DateTime EnrollmentDate { get; set; }
		[Display(Name = "Ukončení studia")]
		public DateTime? CompletionDate { get; set; }
		[Display(Name ="Rodiče")]
		public List<Parent> Parents { get; set; } = new List<Parent>();


		//Teacher
		[Display(Name = "Datum nástupu")]
		public DateTime HireDate { get; set; }
		[Display(Name = "Vyučované předměty")]
		public List<Subject> Subjects { get; set; } = new List<Subject>();

		//Class teacher
		[Display(Name = "Třídní učitel třídy")]
		public Class TeacherClass { get; set; } = new Class();
	}
}
