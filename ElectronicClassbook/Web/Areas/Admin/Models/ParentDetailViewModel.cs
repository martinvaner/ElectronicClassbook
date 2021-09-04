using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Admin.Models
{
	public class ParentDetailViewModel
	{
		[Display(Name = "Jméno")]
		public string FirstName { get; set; }
		[Display(Name = "Příjmení")]
		public string LastName { get; set; }
		[Display(Name = "E-mail")]
		public string Email { get; set; }
		[Display(Name = "Adresa")]
		public string Address { get; set; }

		[Display(Name = "Telefonní číslo")]
		public string PhoneNumber { get; set; }
		[Display(Name = "Děti")]
		public List<Student> Childrens { get; set; } = new List<Student>();
	}
}
