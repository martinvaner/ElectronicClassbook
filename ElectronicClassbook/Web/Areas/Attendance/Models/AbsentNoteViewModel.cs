using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web.Areas.Attendance.Models.Submodels;

namespace Web.Areas.Attendance.Models
{
	public class AbsentNoteViewModel
	{
		public DateTime Created { get; set; }
		public Parent Parent { get; set; }

		[Required(ErrorMessage = "Povinná položka")]
		public string Text { get; set; }

		public List<DataAccess.EntityModel.Attendance> Absences { get; set; } = new List<DataAccess.EntityModel.Attendance>();
	}
}
