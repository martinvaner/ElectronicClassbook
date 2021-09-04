using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Admin.Models.Submodels
{
	public class StudentSubModel
	{
		public List<SelectListItem> Students { get; set; } = new List<SelectListItem>();

		[Display(Name = "Děti")]
		public int[] StudentsId { get; set; } // index of role
	}
}
