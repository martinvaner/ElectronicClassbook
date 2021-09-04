using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Admin.Models.Submodels
{
	public class GenderSubModel
	{
		public List<SelectListItem> Genders { get; set; } = new List<SelectListItem>() {
			new SelectListItem(){ Text = "Žena", Value = "0"},
			new SelectListItem(){ Text = "Muž", Value = "1"}
		};

		[Display(Name = "Pohlaví")]
		[Required(ErrorMessage = "Povinná položka")]
		public int? GenderId { get; set; }
	}
}
