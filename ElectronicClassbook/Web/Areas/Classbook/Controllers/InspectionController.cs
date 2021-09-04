using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Classbook.Interfaces;
using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Classbook.Models;

namespace Web.Areas.Classbook.Controllers
{
	[Authorize(Roles = "Admin, Ředitel, Zástupce ředitele, Třídní učitel, Učitel")]
	[Area("Classbook")]
	[Route("inspection")]
	public class InspectionController : Controller
	{
		private readonly IClassbookManager classbookManager;
		public InspectionController(IClassbookManager classbookManager)
		{
			this.classbookManager = classbookManager;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id">Classbook Id</param>
		/// <param name="date"></param>
		/// <returns></returns>
		public IActionResult Index(int id, DateTime? date, int pageNumber=1)
		{
			List<InspectionListViewModel> model = new List<InspectionListViewModel>();

			var inspections = classbookManager.GetInspectionsByClassbookId(id);
			if (date != null)
				inspections = inspections.Where(x => x.Record.Created.Equals(date));
			else
				inspections = inspections.OrderByDescending(x => x.Record.Created);

			foreach(var inspection in inspections)
			{
				model.Add(new InspectionListViewModel()
				{ 
					Id = inspection.Id,
					Date = inspection.Record.Created.Date,
					Auditor = inspection.Auditor,
					Subject = inspection.Record.Subject
				});
			}

			ViewBag.ClassbookId = id;
			ViewBag.ClassName = classbookManager.GetClassName(id);
			ViewBag.Date = date != null ? date.Value.ToString("yyyy-MM-dd") : null;
			return View(PaginatedList<InspectionListViewModel>.Create(model.AsQueryable(), pageNumber, 6));
		}

		[HttpGet]
		[Route("DetailInspection/{classbookId}/{inspectionId}/{date}")]
		[Route("DetailInspection/{classbookId}/{inspectionId}")]
		public IActionResult DetailInspection(int classbookId, int inspectionId, DateTime? date)
		{
			InspectionDetailViewModel model = new InspectionDetailViewModel();

			var inspection = classbookManager.GetInspectionById(inspectionId);
			if (inspection != null)
			{
				model.Date = inspection.Record.Created;
				model.Auditor = inspection.Auditor;
				model.Text = inspection.Text;
				model.Subject = inspection.Record.Subject;
			}

			ViewBag.ClassbookId = classbookId;
			ViewBag.ClassName = classbookManager.GetClassName(classbookId);
			ViewBag.Date = date != null ? date.Value.ToString("yyyy-MM-dd") : null;
			return View(model);
		}

		[HttpGet]
		[Authorize(Roles = "Ředitel, Zástupce ředitele")]
		[Route("DeleteInspection/{classbookId}/{inspectionId}/{date}")]
		[Route("DeleteInspection/{classbookId}/{inspectionId}")]
		public IActionResult DeleteInspection(int classbookId, int inspectionId, DateTime? date)
		{
			var inspection = classbookManager.GetInspectionById(inspectionId);
			if(inspection != null)
			{
				classbookManager.DeleteInspection(inspection);
			}

			return RedirectToAction("Index", new { id = classbookId, date = date });
		}
	}
}
