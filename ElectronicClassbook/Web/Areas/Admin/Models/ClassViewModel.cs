using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web.Areas.Admin.Models.Submodels;

namespace Web.Areas.Admin.Models
{
	public class ClassViewModel
	{
		public int Id { get; set; }
		[Display(Name = "Název")]
		[Required(ErrorMessage = "Povinná položka")]
		public string Name { get; set; }
		public StudentSubModel Students { get; set; } = new StudentSubModel();
		public ClassTeacherSubModel ClassTeacher { get; set; } = new ClassTeacherSubModel();
		public SubjectSubModel Subjects { get; set; } = new SubjectSubModel();
	}
}
