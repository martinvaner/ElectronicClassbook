using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web.Areas.Admin.Models.Submodels;

namespace Web.Areas.Admin.Models
{
	public class SubjectViewModel
	{
		public int Id { get; set; }
		[Display(Name="Zkratka")]
		[Required(ErrorMessage = "Povinná položka")]
		public string Code { get; set; }
		[Display(Name = "Název")]
		[Required(ErrorMessage = "Povinná položka")]
		public string Name { get; set; }
		public TeacherSubModel Teachers { get; set; } = new TeacherSubModel();
	}
}
