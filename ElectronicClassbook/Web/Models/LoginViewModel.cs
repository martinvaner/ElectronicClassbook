using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
	public class LoginViewModel
	{
		[Required(ErrorMessage ="Zadejte e-mail")]
		[Display(Name = "E-mail")]
		[EmailAddress]
		public string Email { get; set; }
		[Required(ErrorMessage ="Zadejte heslo")]
		[DataType(DataType.Password)]
		[Display(Name = "Heslo")]
		public string Password { get; set; }
	}
}
