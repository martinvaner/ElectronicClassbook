using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Areas.Classbook.Models.Submodels;

namespace Web.Areas.Classbook.Models
{
	public class AttendanceViewModel
	{
		public List<StudentAttendanceSubModel> StudentAttendances { get; set; } = new List<StudentAttendanceSubModel>();
	}
}
