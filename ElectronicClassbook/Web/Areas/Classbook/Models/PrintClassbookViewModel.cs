using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Areas.Classbook.Models.Submodels;

namespace Web.Areas.Classbook.Models
{
	public class PrintClassbookViewModel
	{

		public School School { get; set; }
		public Class Class { get; set; }
		public DataAccess.EntityModel.Classbook Classbook { get; set; }
		public User Principal { get; set; }

		public List<Inspection> Inspections { get; set; } = new List<Inspection>();
		public List<Subject> Subjects { get; set; } = new List<Subject>();
		public List<Instruction> Instructions { get; set; } = new List<Instruction>();
		public List<Student> Students { get; set; } = new List<Student>();
		public List<WeekRecordSubModel> Weeks { get; set; } = new List<WeekRecordSubModel>();

	}
}
