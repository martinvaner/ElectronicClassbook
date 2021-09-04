using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Classbook.Models.Submodels
{
	public class SubjectSubModel
	{
		public List<SelectListItem> Subjects { get; set; } = new List<SelectListItem>();

		[Display(Name = "Předmět")]
		[Required(ErrorMessage = "Povinná položka")]
		public int SubjectId { get; set; } = -1;
	}
}
