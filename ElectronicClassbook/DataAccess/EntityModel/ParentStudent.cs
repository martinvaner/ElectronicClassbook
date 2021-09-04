using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.EntityModel
{
	public class ParentStudent
	{
		public int ParentId { get; set; }
		public Parent Parent { get; set; }
		public int StudentId {get; set;}
		public Student Student { get; set; }
	}
}
