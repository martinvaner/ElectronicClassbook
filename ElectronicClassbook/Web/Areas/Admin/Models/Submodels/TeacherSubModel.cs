using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Admin.Models.Submodels
{
	public class TeacherSubModel
	{
		public List<SelectListItem> Teachers { get; set; } = new List<SelectListItem>();

		[Display(Name = "Vyučující")]
		public int[] TeacherIds { get; set; }
	}
}
