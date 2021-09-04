using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Classbook.Models.Submodels
{
	public class AttendanceSubModel
	{
		public List<SelectListItem> Attendances { get; set; } = new List<SelectListItem>();

		public int AttendanceId { get; set; }
	}
}
