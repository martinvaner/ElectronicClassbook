using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Classbook.Interfaces;
using Common;
using DataAccess.EntityModel;
using jsreport.AspNetCore;
using jsreport.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Classbook.Models;

namespace Web.Areas.Classbook.Controllers
{
	[Authorize(Roles = "Admin, Ředitel, Zástupce ředitele, Třídní učitel, Učitel")]
	[Area("Classbook")]
	[Route("home")]
	public class HomeController : Controller
	{
		private readonly IClassbookManager classbookManager;
		private readonly IJsReportMVCService jsReportMVCService;

		public HomeController(IClassbookManager classbookManager, IJsReportMVCService jsReportMVCService)
		{
			this.classbookManager = classbookManager;
			this.jsReportMVCService = jsReportMVCService;
		}
		public IActionResult Index(int pageNumber = 1)
		{
			List<ClassbookListViewModel> model = new List<ClassbookListViewModel>();

			//get all classbooks, order them by active, school year and class name
			var classbooks = classbookManager.GetAllClassbooks().OrderByDescending(x => x.IsActive).ThenByDescending(x => x.SchoolYear).ThenBy(x => x.Class.Name);

			foreach (var c in classbooks)
			{
				model.Add(new ClassbookListViewModel()
				{
					Id = c.Id,
					Class = c.Class,
					IsActive = c.IsActive,
					SchoolYear = c.SchoolYear
				});
			}

			return View(PaginatedList<ClassbookListViewModel>.Create(model.AsQueryable(), pageNumber, 6));
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		[Route("AddClassbook")]
		public IActionResult AddClassbook()
		{
			ClassbookAddViewModel model = new ClassbookAddViewModel();
			this.FillClasses(ref model);

			return View(model);
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		[Route("AddClassbook")]
		public IActionResult AddClassbook(ClassbookAddViewModel model)
		{
			DataAccess.EntityModel.Classbook c = new DataAccess.EntityModel.Classbook();

			c.IsActive = model.IsActive;
			c.SchoolYear = model.SchoolYear;
			c.Records = new List<Record>();
			c.Class = classbookManager.GetAllClasses().Where(x => x.Id.Equals(model.Class.ClassId)).FirstOrDefault();

			if (!classbookManager.CreateClassbook(c))
			{
				ModelState.AddModelError("", "Někde se stala chyba. Třídní kniha nebyla vytvořena.");
				this.FillClasses(ref model);
				return View(model);
			}

			return RedirectToAction("Index");
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		[Route("EditClassbook")]
		public IActionResult EditClassbook(ClassbookAddViewModel model)
		{
			DataAccess.EntityModel.Classbook c = classbookManager.GetAllClassbooks().Where(x => x.Id.Equals(model.Id)).FirstOrDefault();
			c.IsActive = model.IsActive;
			c.SchoolYear = model.SchoolYear;
			c.Class = classbookManager.GetAllClasses().Where(x => x.Id.Equals(model.Class.ClassId)).FirstOrDefault();

			if(!classbookManager.UpdateClassbook(c))
			{
				ModelState.AddModelError("", "Někde se stala chyba. Změny nebyly provedeny.");
				this.FillClasses(ref model);
				return View(model);
			}


			return RedirectToAction("Index");
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		[Route("EditClassbook")]
		public IActionResult EditClassbook(int id)
		{
			DataAccess.EntityModel.Classbook c = classbookManager.GetAllClassbooks().Where(x => x.Id.Equals(id)).FirstOrDefault();
			ClassbookAddViewModel model = new ClassbookAddViewModel();
			this.FillClasses(ref model);

			model.Id = c.Id;
			model.IsActive = c.IsActive;
			model.SchoolYear = c.SchoolYear;
			model.Class.ClassId = c.Class.Id;

			return View(model);
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		[Route("DeleteClassbook")]
		public IActionResult DeleteClassbook(int id)
		{
			DataAccess.EntityModel.Classbook c = classbookManager.GetAllClassbooks().Where(x => x.Id.Equals(id)).FirstOrDefault();

			if (c != null)
			{
				classbookManager.DeleteClassbook(c);
			}
			return RedirectToAction("Index");
		}

		[HttpGet]
		[Authorize(Roles = "Admin, Ředitel, Zástupce ředitele, Třídní učitel")]
		[Route("ExportClassbook/{id}")]
		public IActionResult ExportClassbook(int id)
		{
			ExportClassbookViewModel model = new ExportClassbookViewModel();

			var classbook = classbookManager.GetAllClassbooks().Where(x => x.Id.Equals(id)).FirstOrDefault();
			model.ClassName = classbook.Class.Name;
			model.From = DateTime.Today;
			model.To = DateTime.Today;

			if (User.IsInRole("Třídní učitel"))
			{
				var classTeacher = classbookManager.GetClassTeacherByEmail(User.Identity.Name);
				if(classTeacher.Class == null || classbook.Class.Id != classTeacher.Class.Id)
				{
					ModelState.AddModelError("", "Pro tisk této třídní knihy nemáte oprávnění.");
					ViewBag.ShowPrintBtn = false;
					return View(model);
				}
			}

			ViewBag.ShowPrintBtn = true;
			return View(model);
		}

		[HttpPost]
		[Authorize(Roles = "Admin, Ředitel, Zástupce ředitele, Třídní učitel")]
		[Route("ExportClassbook/{id}")]
		[MiddlewareFilter(typeof(JsReportPipeline))]
		public IActionResult ExportClassbook(int id, ExportClassbookViewModel m)
		{
			//if(m.To == null || m.From == null || m.From < new DateTime(2010,1,1) || m.To < new DateTime(2010, 1, 1) || m.From > m.To)
			//{
			//	ModelState.AddModelError("", "Zadejte validní datum.");
			//	ViewBag.ShowPrintBtn = true;
			//	return View(m);
			//}
			if(m.To == null || m.To < new DateTime(2010, 1, 1))
			{
				m.To = DateTime.Today;
			}
			if(m.From == null || m.From < new DateTime(2010, 1, 1))
			{
				m.From = DateTime.Today;
			}
			if(m.From > m.To)
			{
				m.From = m.To;
			}

			PrintClassbookViewModel model = new PrintClassbookViewModel();
			model.School = classbookManager.GetSchool();
			model.Class = classbookManager.GetClassByClassbookId(id);
			model.Classbook = classbookManager.GetAllClassbooks().Where(x => x.Id == id).FirstOrDefault();
			model.Inspections = classbookManager.GetInspectionsByClassbookId(id).ToList();
			model.Instructions = classbookManager.GetInstructionsByClassbookId(id).ToList();
			model.Students = classbookManager.GetStudentsByClassId(model.Class.Id).ToList();
			model.Subjects = classbookManager.GetClassSubjects(model.Class.Id).ToList();
			model.Principal = classbookManager.GetPrincipal();

			//Get records by days and weeks
			int weekListIndex = -1;
			for (var day = m.From.Value.Date; day <= m.To.Value.Date; day = day.AddDays(1))
			{
				if (day.DayOfWeek == DayOfWeek.Saturday || day.DayOfWeek == DayOfWeek.Sunday)
				{
					continue;
				}
				if (day.DayOfWeek == DayOfWeek.Monday || model.Weeks.Count == 0)
				{
					model.Weeks.Add(new Models.Submodels.WeekRecordSubModel());
					weekListIndex++;
				}

				var absences = classbookManager.GetAbsencesByDate(id, day).ToList();

				model.Weeks[weekListIndex].Days.Add(new Models.Submodels.DayRecordSubject()
				{
					Day = day,
					Records = classbookManager.GetRecordsByDate(id, day).ToList(),
					Absences = classbookManager.GetAbsencesByDate(id, day).ToList(),
					StudentAbsence = absences.GroupBy(x => x.Student).ToDictionary(x => x.Key, x => x.ToList())
				});
			}

			HttpContext.JsReportFeature().Recipe(Recipe.ChromePdf);

			return View("~/Areas/Classbook/Views/PrintTemplates/Classbook.cshtml", model);
		}

		private void FillClasses(ref ClassbookAddViewModel model)
		{
			var classes = classbookManager.GetAllClasses();

			foreach (var c in classes)
			{
				model.Class.Classes.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
				{
					Text = c.Name,
					Value = c.Id.ToString()
				});
			}
		}
	}
}
