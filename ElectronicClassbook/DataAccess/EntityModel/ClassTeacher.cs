using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.EntityModel
{
	public class ClassTeacher : Teacher
	{
		public int? ClassId { get; set; }
		public Class Class { get; set; }
		public ICollection<AbsentNote> AbsentNotes { get; set; }
	}
}
