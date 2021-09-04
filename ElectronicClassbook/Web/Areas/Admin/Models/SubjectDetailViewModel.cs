using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Admin.Models
{
	public class SubjectDetailViewModel
	{
		public int Id { get; set; }

		[Display(Name ="Zkratka")]
		public string Code { get; set; }
		[Display(Name = "Název")]
		public string Name { get; set; }
		[Display(Name = "Vyučující")]
		public List<Teacher> Teachers { get; set; } = new List<Teacher>();
	}
}
