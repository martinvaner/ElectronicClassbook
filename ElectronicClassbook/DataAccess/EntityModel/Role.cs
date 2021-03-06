using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.EntityModel
{
	/// <summary>
	/// Code list for user's roles
	/// </summary>
	public class Role
	{
		public Role()
		{
			UserRoles = new HashSet<UserRole>();
		}

		[Key]
		public int Id { get; set; }
		[Required]
		[StringLength(150)]
		public string Name { get; set; }

		public virtual ICollection<UserRole> UserRoles { get; set; }
	}
}
