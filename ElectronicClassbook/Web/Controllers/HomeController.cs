using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return RedirectToHome();
		}

		[Authorize(Roles ="Admin")]
		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		private IActionResult RedirectToHome()
		{
			if (User.IsInRole("Žák"))
			{
				return RedirectToAction("Index", "Student", new { area = "Reports" });
			}
			if (User.IsInRole("Rodič"))
			{
				return RedirectToAction("Index", "Parent", new { area = "Reports" });
			}
			if (User.IsInRole("Třídní učitel") || User.IsInRole("Učitel"))
			{
				return RedirectToAction("TeacherDashboard", "Report", new { area = "Reports" });
			}
			if (User.IsInRole("Admin"))
			{
				return RedirectToAction("Index", "Class", new { area = "Admin" });
			}


			return RedirectToAction("Index", "Home", new { area = "Classbook" });
		}
	}
}
