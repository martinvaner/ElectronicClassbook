using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web.Areas.Admin.Models.Submodels;

namespace Web.Areas.Admin.Models
{
	public class UserViewModel
	{
		public int Id { get; set; }
		//Generic user
		[Display(Name="Jméno")]
		[Required(ErrorMessage ="Povinná položka")]
		public string FirstName { get; set; }
		[Display(Name = "Příjmení")]
		[Required(ErrorMessage = "Povinná položka")]
		public string LastName { get; set; }
		[Display(Name = "E-mail")]
		[Required(ErrorMessage = "Povinná položka")]
		public string Email { get; set; }
		[Display(Name ="Adresa")]
		[Required(ErrorMessage = "Povinná položka")]
		public string Address { get; set; }
		[Display(Name ="Heslo")]
		public string Password { get; set; }

		public RoleSubModel UserRoles { get; set; } = new RoleSubModel();

		//Parent

		[Display(Name ="Telefonní číslo")]
		public string PhoneNumber { get; set; }
		public StudentSubModel Childrens { get; set; } = new StudentSubModel();
		//Student

		[Display(Name ="Začátek studia")]
		public DateTime EnrollmentDate { get; set; } 
		[Display(Name ="Ukončení studia")]
		public DateTime? CompletionDate { get; set; }
		public ParentSubModel Parents { get; set; } = new ParentSubModel();


		//Teacher
		[Display(Name ="Datum nástupu")]
		public DateTime HireDate { get; set; }
		public SubjectSubModel Subjects { get; set; } = new SubjectSubModel();

		//Class teacher
		public ClassSubModel TeacherClass { get; set; } = new ClassSubModel();

	}
}
