using Common;
using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Classbook.Models
{
	public class HomeworkListViewModel
	{
		public PaginatedList<Homework> Homeworks { get; set; }
	}
}
