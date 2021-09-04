using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Areas.Attendance.Models.Submodels;

namespace Web.Areas.Attendance.Models
{
	public class AbsentNoteListViewModel
	{
		public List<AbsentNote> Notes { get; set; } = new List<AbsentNote>();
	}
}
