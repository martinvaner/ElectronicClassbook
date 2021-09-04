using Common;
using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Reports.Models
{
	public class SchoolHomeNoteListViewModel
	{
		public PaginatedList<SchoolHomeNote> Notes { get; set; }
	}
}
