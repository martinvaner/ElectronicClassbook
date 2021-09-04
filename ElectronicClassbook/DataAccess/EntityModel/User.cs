using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.EntityModel
{
	public class User
	{

		public User()
		{
			UserRoles = new HashSet<UserRole>();
		}

		[NotMapped]
		public string FullName {
			get
			{
				return LastName + " " + FirstName + " (" + Email + ")";
			}
			set { } }

		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string FirstName { get; set; }

		[Required]
		[StringLength(100)]
		public string LastName { get; set; }

		[Required]
		[StringLength(200)]
		public string Email { get; set; }

		[Required]
		[StringLength(250)]
		public string Address { get; set; }

		public string TitleBefore { get; set; }
		public string TitleAfter { get; set; }

		public string PersonalIdentificationNumber { get; set; }

		/// <summary>
		/// True / 1 - Male
		/// False / 0 - Female
		/// </summary>
		public bool? Gender { get; set; }

		public DateTime? DateOfBirth { get; set; }
		public string BirthLocation { get; set; }
		public string Citizenship { get; set; }

		[Required]
		[StringLength(250)]
		public string Password { get; set; }

		[StringLength(250)]
		public string ResetToken { get; set; }
		
		public DateTime? ResetTokenTimestamp { get; set; }

		public virtual ICollection<UserRole> UserRoles { get; set; }
	}
}
