using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Admin.Models.Submodels
{
	public class ClassSubModel
	{
		public List<SelectListItem> Classes { get; set; } = new List<SelectListItem>();

		[Display(Name = "Třída")]
		public int ClassId { get; set; } = -1;
	}
}
