using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Admin.Models.Submodels
{
	public class SubjectSubModel
	{
		public List<SelectListItem> Subjects { get; set; } = new List<SelectListItem>();

		[Display(Name = "Předměty")]
		public int[] SubjectsId { get; set; } // index of subject
	}
}
