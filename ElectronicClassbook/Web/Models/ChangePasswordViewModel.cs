using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
	public class ChangePasswordViewModel
	{
		[Required(ErrorMessage ="Zadejte současné heslo")]
		[DataType(DataType.Password)]
		[Display(Name = "Současné heslo")]
		public string CurrentPassword { get; set; }
		[Required(ErrorMessage = "Zadejte své nové heslo")]
		[DataType(DataType.Password)]
		[Display(Name = "Nové heslo")]
		public string NewPassword { get; set; }
		[Required(ErrorMessage = "Potvrďte nové heslo")]
		[DataType(DataType.Password)]
		[Display(Name = "Potvrzení nového hesla")]
		[Compare("NewPassword", ErrorMessage = "Hesla se neshodují")]
		public string ConfirmNewPassword { get; set; }
	}
}
