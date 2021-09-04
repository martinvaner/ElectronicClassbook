﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Admin.Models.Submodels
{
	public class RoleSubModel
	{
		public List<SelectListItem> UserRoles { get; set; } = new List<SelectListItem>();

		[Display(Name = "Role")]
		public int[] RolesId { get; set; } // index of role
	}
}
