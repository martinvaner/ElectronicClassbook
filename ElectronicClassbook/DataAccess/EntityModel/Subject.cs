using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.EntityModel
{
	public class Subject
	{
		public Subject()
		{
			TeacherSubjects = new HashSet<TeacherSubject>();
			Records = new HashSet<Record>();
			ClassSubjects = new HashSet<ClassSubject>();
		}
		[Key]
		public int Id { get; set; }
		[Required]
		[StringLength(100)]
		public string Code { get; set; }
		[Required]
		[StringLength(200)]
		public string Name { get; set; }
		public ICollection<TeacherSubject> TeacherSubjects { get; set; } 
		public ICollection<Record> Records { get; set; }
		public ICollection<ClassSubject> ClassSubjects { get; set; }
	}
}
