using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web.Areas.Admin.Models.Submodels;

namespace Web.Areas.Classbook.Models
{
	public class ClassbookListViewModel
	{
		public int Id { get; set; }
		[Display(Name = "Třída")]
		[Required(ErrorMessage = "Povinná položka")]
		public Class Class { get; set; }
		[Display(Name ="Školní rok")]
		[Required(ErrorMessage = "Povinná položka")]
		public string SchoolYear { get; set; }
		public bool IsActive { get; set; }
	}
}
