using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
	public class ForgotPasswordviewModel
	{
		[Required(ErrorMessage ="Zadejte e-mail")]
		[EmailAddress]
		public string Email { get; set; }
	}
}
