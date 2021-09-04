using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Classbook.Models.Submodels
{
	public class DayRecordSubject
	{
		public List<Record> Records { get; set; } = new List<Record>();
		public List<DataAccess.EntityModel.Attendance> Absences { get; set; } = new List<DataAccess.EntityModel.Attendance>();

		public Dictionary<Student, List<DataAccess.EntityModel.Attendance>> StudentAbsence { get; set; } = new Dictionary<Student, List<DataAccess.EntityModel.Attendance>>();

		public DateTime Day { get; set; }
	}
}
