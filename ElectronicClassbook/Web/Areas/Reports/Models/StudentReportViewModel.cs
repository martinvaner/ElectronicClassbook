using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Reports.Models
{
	public class StudentReportViewModel
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }

		public List<DataAccess.EntityModel.Attendance> Absences { get; set; } = new List<DataAccess.EntityModel.Attendance>();

		public List<Homework> Homeworks { get; set; } = new List<Homework>();

		public List<SchoolHomeNote> Notes { get; set; } = new List<SchoolHomeNote>();
	}
}
