using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Classbook.Models.Submodels
{
	public class StudentAttendanceSubModel
	{
		public Student Student { get; set; }
		public AttendanceSubModel Attendace { get; set; }
		[Display(Name ="Počet minut")]
		public int AbsenceInterval { get; set; }
	}
}
