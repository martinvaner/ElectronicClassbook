using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Admin.Models.Submodels
{
	public class ClassTeacherSubModel
	{
		public List<SelectListItem> ClassTeachers { get; set; } = new List<SelectListItem>();

		[Display(Name = "Třídní učitel")]
		public int ClassTeacherId { get; set; }
	}
}
