using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.EntityModel
{
	public class TeacherSubject
	{
		public int TeacherId { get; set; }
		public Teacher Teacher { get; set; }
		public int SubjectId {get; set;}
		public Subject Subject { get; set; }
	}
}
