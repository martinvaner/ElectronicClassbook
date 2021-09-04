using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Reports.Models
{
	public class TeacherReportViewModel
	{
		public List<AbsentNote> AbsentNotes { get; set; } = new List<AbsentNote>();

		public List<Homework> Homeworks { get; set; } = new List<Homework>();
	}
}
