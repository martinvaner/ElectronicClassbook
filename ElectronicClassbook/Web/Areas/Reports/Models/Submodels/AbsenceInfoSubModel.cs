using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Reports.Models.Submodels
{
	public class AbsenceInfoSubModel
	{
		public DateTime Date { get; set; }
		public int MissedClasses { get; set; }
		public bool IsExcused { get; set; }
		public List<DataAccess.EntityModel.Attendance> Absences { get; set; } = new List<DataAccess.EntityModel.Attendance>();
	}
}
