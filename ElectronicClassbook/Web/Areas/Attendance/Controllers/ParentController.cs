using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Attendance.Interfaces;
using DataAccess.EntityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Attendance.Models;
using Web.Areas.Attendance.Models.Submodels;

namespace Web.Areas.Attendance.Controllers
{

	[Authorize(Roles = "Rodič")]
	[Area("Attendance")]
	[Route("parent")]
	public class ParentController : Controller
	{
		private readonly IParentManager parentManager;
		public ParentController(IParentManager parentManager)
		{
			this.parentManager = parentManager;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		[Route("SendAbsentNote/{studentId}")]
		public IActionResult SendAbsentNote(int studentId, AbsentNoteViewModel model)
		{
			AbsentNote an = new AbsentNote();
			an.Created = DateTime.Today;
			an.Text = model.Text;
			an.State = parentManager.GetAbsentNoteStateByName("Odeslána");
			an.Parent = parentManager.GetParentByEmail(User.Identity.Name);
			an.Student = parentManager.GetStudentById(studentId);

			List<DataAccess.EntityModel.Attendance> absences = new List<DataAccess.EntityModel.Attendance>();
			foreach (var absence in model.Absences)
			{
				absences.Add(parentManager.GetAttendanceById(absence.Id));
			}
			an.Absences = absences;

			if (!parentManager.CreateAbsentNote(an))
			{
				ModelState.AddModelError("", "Někde se stala chyba. Omluvenka nebyla vytvořena.");
			}

			return RedirectToAction("ShowAbsences", new { id = studentId });
		}

		[HttpPost]
		[Route("CreateAbsentNote/{studentId}")]
		public IActionResult CreateAbsentNote(int studentId, AbsenceListViewModel model)
		{
			AbsentNoteViewModel m = new AbsentNoteViewModel();

			foreach(var absence in model.Absences)
			{
				if(absence.Selected)
				{
					m.Absences.AddRange(absence.Absences);
				}
			}

			if(m.Absences.Count == 0)
			{
				//error
				ModelState.AddModelError("", "Nebyla vybrána žádná absence k omluvení.");
			}

			ViewBag.Childrens = this.GetLayoutData();
			ViewBag.StudentId = studentId;

			return View(m);
		}

		[HttpGet]
		[Route("ShowAbsences")]
		public IActionResult ShowAbsences()
		{
			List<StudentInfoSubModel> kids = this.GetLayoutData();
			var kid = kids.FirstOrDefault();
			if (kid == null)
			{
				//error
			}

			return RedirectToAction("ShowAbsences", new { id = kid.Id });
		}

		[HttpGet]
		[Route("ShowAbsences/{id}")]
		[Route("ShowAbsences/{id}/{all}")]
		public IActionResult ShowAbsences(int id, bool all = false, int pageNumber=1)
		{
			ViewBag.Childrens = this.GetLayoutData();
			ViewBag.StudentId = id;

			AbsenceListViewModel model = new AbsenceListViewModel();

			// get list of absences
			List<DataAccess.EntityModel.Attendance> absences = parentManager.GetAbsenceByStudentId(id).ToList();

			if (!all)
			{
				absences = absences.Where(x => x.IsExcused == false).Where(x => (x.AbsentNoteId == null || x.AbsentNote.State.Name.Equals("Zamítnuta"))).ToList();
			}
			//Groups by created date
			var absencesByDate = absences.GroupBy(x => x.Record.Created);
			foreach (var absence in absencesByDate)
			{
				model.Absences.Add(new AbsenceInfoSubModel()
				{
					Date = absence.Key,
					MissedClasses = absence.Count(),
					Selected = false,
					Absences = absences.Where(x => x.Record.Created.Equals(absence.Key)).ToList()
				});
			}
			ViewBag.NewAbsences = absences.Where(x => (x.AbsentNoteId == null || x.AbsentNote.State.Name.Equals("Zamítnuta"))).Count(x => x.IsExcused == false);
			ViewBag.ShowAll = all;

			return View(model);
		}

		private List<StudentInfoSubModel> GetLayoutData()
		{
			Parent parent = parentManager.GetParentByEmail(User.Identity.Name);
			if(parent == null)
			{
				//error
				return new List<StudentInfoSubModel>();
			}
			List<Student> childrens = parentManager.GetStudentsByParentId(parent.Id).ToList();
			if (childrens == null || childrens.Count == 0)
			{
				//error
				return new List<StudentInfoSubModel>();
			}

			var kids = new List<StudentInfoSubModel>();
			foreach (var kid in childrens)
			{
				kids.Add(new Models.Submodels.StudentInfoSubModel()
				{
					FirstName = kid.FirstName,
					Id = kid.Id
				});
			}

			return kids;
		}
	}
}
