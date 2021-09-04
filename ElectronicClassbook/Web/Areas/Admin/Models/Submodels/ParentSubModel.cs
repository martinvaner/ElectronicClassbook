using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Admin.Models.Submodels
{
	public class ParentSubModel
	{
		public List<SelectListItem> Parents { get; set; } = new List<SelectListItem>();

		[Display(Name = "Rodiče")]
		public int[] ParentsId { get; set; } // index of parent
	}
}
