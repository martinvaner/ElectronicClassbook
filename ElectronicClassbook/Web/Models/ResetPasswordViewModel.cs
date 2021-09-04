using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
	public class ResetPasswordViewModel
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		[DataType(DataType.Password)]
		[Display(Name ="Nové heslo")]
		public string NewPassword { get; set; }
		[Required]
		[DataType(DataType.Password)]
		[Compare("NewPassword", ErrorMessage ="Hesla se neshodují")]
		[Display(Name = "Potvrďte nové heslo")]
		public string ConfirmPassword { get; set; }
		public string Token { get; set; }
	}
}
