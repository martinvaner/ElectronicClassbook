using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.EntityModel
{
	public class Parent : User
	{
		public Parent()
		{
			ParentStudents = new HashSet<ParentStudent>();
			AbsentNotes = new HashSet<AbsentNote>();
		}
		[Required]
		[StringLength(100)]
		public string PhoneNumber { get; set; }

		public Account Account { get; set; }

		public ICollection<ParentStudent> ParentStudents { get; set; }

		public ICollection<AbsentNote> AbsentNotes { get; set; }
	}
}
