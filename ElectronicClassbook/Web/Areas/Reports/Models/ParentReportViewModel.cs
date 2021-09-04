using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Areas.Reports.Models.Submodels;

namespace Web.Areas.Reports.Models
{
	public class ParentReportViewModel : ParentBaseViewModel
	{
		public List<StudentReportViewModel> Students { get; set; } = new List<StudentReportViewModel>();
	}
}
