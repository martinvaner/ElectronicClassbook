using Common;
using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Areas.Reports.Models.Submodels;

namespace Web.Areas.Reports.Models
{
	public class AbsenceListViewModel
	{
		public PaginatedList<AbsenceInfoSubModel> Absences { get; set; }

		public Student Student { get; set; }
	}
}
