using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web.Areas.Classbook.Models.Submodels;

namespace Web.Areas.Classbook.Models
{
	public class RecordAddViewModel
	{
		public int Id { get; set; }
		[Display(Name = "Vytvořeno")]
		public DateTime Created { get; set; }

		[Display(Name = "Hodina")]
		[Required(ErrorMessage = "Povinná položka")]
		public int SerialNumber { get; set; }
		[Display(Name = "Předmět")]
		[Required(ErrorMessage = "Povinná položka")]
		public SubjectSubModel Subject { get; set; } = new SubjectSubModel();
		[Display(Name = "Téma")]
		[Required(ErrorMessage = "Povinná položka")]
		public string Topic { get; set; }
		[Display(Name = "Zápis vytvořil")]
		public Teacher CreatedBy { get; set; }

		public DataAccess.EntityModel.Classbook Classbook { get; set; }
	}
}
