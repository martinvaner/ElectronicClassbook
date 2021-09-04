using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Classbook.Models.Submodels
{
	public class WeekRecordSubModel
	{
		public List<DayRecordSubject> Days { get; set; } = new List<DayRecordSubject>();
	}
}
