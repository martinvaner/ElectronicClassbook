using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.EntityModel
{
	/// <summary>
	/// Represents bank account
	/// </summary>
	public class Account
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public double Balance { get; set; } = 0;
		public Parent Parent { get; set; }
		public int? ParentId { get; set; }
	}
}
