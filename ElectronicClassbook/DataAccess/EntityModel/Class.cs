using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.EntityModel
{
	public class Class
	{
		public Class()
		{
			Students = new HashSet<Student>();
			ClassSubjects = new HashSet<ClassSubject>();
		}
		[Key]
		public int Id { get; set; }
		[Required]
		[StringLength(200)]
		public string Name { get; set; }
		public ICollection<Student> Students { get; set; }
		public ClassTeacher ClassTeacher { get; set; }
		public ICollection<ClassSubject> ClassSubjects { get; set; }

	}
}
