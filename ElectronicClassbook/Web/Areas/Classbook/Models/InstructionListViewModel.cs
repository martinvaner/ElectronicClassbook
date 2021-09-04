using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Classbook.Models
{
	public class InstructionListViewModel
	{
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public string Title { get; set; }
		public User Author { get; set; }
	}
}
