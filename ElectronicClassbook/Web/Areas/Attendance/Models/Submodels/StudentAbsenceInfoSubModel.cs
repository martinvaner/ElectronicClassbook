using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Attendance.Models.Submodels
{
	public class StudentAbsenceInfoSubModel
	{
		public Student Student { get; set; }
		
		public int AbsencesCount { get; set; }
	}
}
