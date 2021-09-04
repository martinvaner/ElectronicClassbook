using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Reports.Models
{
	public class AbsentNoteDetailViewModel
	{

		public string Text { get; set; }
		public DateTime Created { get; set; }
		public List<DataAccess.EntityModel.Attendance> Absences { get; set; } = new List<DataAccess.EntityModel.Attendance>();
		public Parent Parent { get; set; }
		public AbsentNoteState State { get; set; }


	}
}
