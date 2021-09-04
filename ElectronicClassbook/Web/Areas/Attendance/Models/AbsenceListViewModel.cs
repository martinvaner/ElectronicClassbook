using DataAccess.EntityModel;
using System.Collections.Generic;
using Web.Areas.Attendance.Models.Submodels;

namespace Web.Areas.Attendance.Models
{
	public class AbsenceListViewModel
	{
		public List<AbsenceInfoSubModel> Absences { get; set; } = new List<AbsenceInfoSubModel>();

		public Student Student { get; set; }
	}
}
