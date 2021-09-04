using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Areas.Attendance.Models.Submodels;

namespace Web.Areas.Attendance.Models
{
	public class StudentAbsenceListViewModel
	{
		public PaginatedList<StudentAbsenceInfoSubModel> Students { get; set; }
	}
}
