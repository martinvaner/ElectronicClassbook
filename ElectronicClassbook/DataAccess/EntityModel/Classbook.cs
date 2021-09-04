using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.EntityModel
{
	public class Classbook
	{
		public Classbook()
		{
			Records = new HashSet<Record>();
		}

		[Key]
		public int Id { get; set; }
		/// <summary>
		/// Class that classbook belongs to.
		/// </summary>
		[Required]
		public Class Class { get; set; }
		[Required]
		public int ClassId { get; set; }


		/// <summary>
		/// School year
		/// </summary>
		[Required]
		[StringLength(200)]
		public string SchoolYear { get; set; }

		/// <summary>
		/// Is classbook active
		/// </summary>
		[Required]
		public bool IsActive { get; set; } = true;

		/// <summary>
		/// Classbook contains records
		/// </summary>
		public ICollection<Record> Records { get; set; }
	}
}
