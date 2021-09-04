using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Admin.Models
{
	public class ClassDetailViewModel
	{
		public int Id { get; set; }
		[Display(Name="Název")]
		public string Name { get; set; }
		[Display(Name = "Studenti")]
		public List<Student> Students { get; set; } = new List<Student>();
		[Display(Name = "Třídní učitel")]
		public ClassTeacher ClassTeacher { get; set; } = new ClassTeacher();
		[Display(Name = "Předměty")]
		public List<Subject> Subjects { get; set; } = new List<Subject>();
	}
}
