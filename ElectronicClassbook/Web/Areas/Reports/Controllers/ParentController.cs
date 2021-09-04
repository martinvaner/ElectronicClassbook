using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using DataAccess.EntityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reports.Interfaces;
using Web.Areas.Reports.Models;
using Web.Areas.Reports.Models.Submodels;

namespace Web.Areas.Reports.Controllers
{

	[Authorize(Roles = "Rodič")]
	[Area("Reports")]
	[Route("parent")]
	public class ParentController : Controller
	{
		private readonly IParentStudentManager parentManager;
		public ParentController(IParentStudentManager parentManager)
		{
			this.parentManager = parentManager;
		}

		[Route("Index")]
		public IActionResult Index()
		{
			ParentReportViewModel model = new ParentReportViewModel();
			this.GetLayoutData();

			Parent parent = parentManager.GetParentByEmail(User.Identity.Name);
			if (parent == null)
			{
				//error
			}
			List<Student> childrens = parentManager.GetStudentsByParentId(parent.Id).ToList();
			if (childrens == null || childrens.Count == 0)
			{
				//error
			}

			foreach (var kid in childrens)
			{
				//get absence by student id
				List<DataAccess.EntityModel.Attendance> absence = parentManager.GetAbsencesByStudentId(kid.Id).Where(x => x.IsExcused == false)
																	.Where(x => (x.AbsentNoteId == null || x.AbsentNote.State.Name.Equals("Zamítnuta"))).ToList();

				// get homeworks by student id
				List<Homework> homeworks = parentManager.GetHomeworksByStudentId(kid.Id).Where(x => x.ShowParent == true).Where(x => x.Deadline > DateTime.Today).ToList();

				// get student's school-home notes by student's id
				List<SchoolHomeNote> notes = parentManager.GetSchoolHomeNotesByStudentId(kid.Id).Where(x => x.ShowParent == true).ToList();

				model.Students.Add(new StudentReportViewModel()
				{
					Id = kid.Id,
					FirstName = kid.FirstName,
					LastName = kid.LastName,
					Email = kid.Email,
					Absences = absence,
					Homeworks = homeworks,
					Notes = notes
				});
			}

			return View(model);
		}

		[HttpGet]
		[Route("ShowHomeworks")]
		public IActionResult ShowHomeworks()
		{
			List<StudentInfoSubModel> kids = this.GetLayoutData();
			var kid = kids.FirstOrDefault();
			if(kid == null)
			{
				//error
			}

			return RedirectToAction("ShowHomeworks", new { id = kid.Id });
		}

		[HttpGet]
		[Route("ShowHomeworks/{id}")]
		[Route("ShowHomeworks/{id}/{all}")]
		public IActionResult ShowHomeworks(int id, bool all = false, int pageNumber=1)
		{
			ViewBag.Childrens = this.GetLayoutData();
			ViewBag.StudentId = id;


			//get homeworks for student
			List<Homework> homeworks = parentManager.GetHomeworksByStudentId(id).Where(x => x.Deadline > DateTime.Today).ToList();
			if(homeworks == null)
			{
				//error
			}
			List<Homework> newHomeworks = homeworks.Where(x => x.ShowParent == true).ToList();


			HomeworkListViewModel model = new HomeworkListViewModel();
			if (all)
				model.Homeworks = PaginatedList<Homework>.Create(homeworks.AsQueryable(), pageNumber, 10);
			else
				model.Homeworks = PaginatedList<Homework>.Create(newHomeworks.AsQueryable(), pageNumber, 10);

			ViewBag.NewHomeworksCount = newHomeworks.Count(x => x.ShowParent == true);
			return View(model);
		}

		[HttpGet]
		[Route("DetailHomework/{id}/{homeworkId}")]
		public IActionResult DetailHomework(int id, int homeworkId)
		{
			ViewBag.Childrens = this.GetLayoutData();
			ViewBag.StudentId = id;

			HomeworkDetailViewModel model = new HomeworkDetailViewModel();
			Homework homework = parentManager.GetHomeworkById(homeworkId);
			if(homework == null)
			{
				//error
			}

			model.homework = homework;

			return View(model);
		}

		[HttpGet]
		[Route("HideHomework/{id}/{homeworkId}")]
		public IActionResult HideHomework(int id, int homeworkId)
		{
			//set Show property to false

			Homework homework = parentManager.GetHomeworkById(homeworkId);
			if(homework == null)
			{
				//error
			}

			homework.ShowParent = false;
			parentManager.UpdateHomework(homework);

			return RedirectToAction("ShowHomeworks", new { id });
		}


		[HttpGet]
		[Route("ShowSchoolHomeNotes")]
		public IActionResult ShowSchoolHomeNotes()
		{
			List<StudentInfoSubModel> kids = this.GetLayoutData();
			var kid = kids.FirstOrDefault();
			if (kid == null)
			{
				//error
			}

			return RedirectToAction("ShowSchoolHomeNotes", new { id = kid.Id });
		}

		[HttpGet]
		[Route("ShowSchoolHomeNotes/{id}")]
		[Route("ShowSchoolHomeNotes/{id}/{all}")]
		public IActionResult ShowSchoolHomeNotes(int id, bool all = false, int pageNumber=1)
		{
			ViewBag.Childrens = this.GetLayoutData();
			ViewBag.StudentId = id;

			List<SchoolHomeNote> notes = parentManager.GetSchoolHomeNotesByStudentId(id).ToList();
			if (notes == null)
			{
				//error
			}
			List<SchoolHomeNote> newNotes = notes.Where(x => x.ShowParent == true).ToList();

			SchoolHomeNoteListViewModel model = new SchoolHomeNoteListViewModel();
			if (all)
				model.Notes = PaginatedList<SchoolHomeNote>.Create(notes.AsQueryable(), pageNumber, 10);
			else
				model.Notes = PaginatedList<SchoolHomeNote>.Create(newNotes.AsQueryable(), pageNumber, 10);

			ViewBag.NewNotesCount = newNotes.Count(x => x.ShowParent == true);
			return View(model);
		}

		[HttpGet]
		[Route("HideSchoolHomeNote/{id}/{noteId}")]
		public IActionResult HideSchoolHomeNote(int id, int noteId)
		{
			SchoolHomeNote note = parentManager.GetSchoolHomeNoteById(noteId);
			if (note == null)
			{
				//error
			}

			note.ShowParent = false;
			parentManager.UpdateSchoolHomeNote(note);

			return RedirectToAction("ShowSchoolHomeNotes", new { id });
		}

		[HttpGet]
		[Route("ShowAbsentNotes")]
		public IActionResult ShowAbsentNotes()
		{
			List<StudentInfoSubModel> kids = this.GetLayoutData();
			var kid = kids.FirstOrDefault();
			if (kid == null)
			{
				//error
			}

			return RedirectToAction("ShowAbsentNotes", new { id = kid.Id });
		}

		[HttpGet]
		[Route("ShowAbsentNotes/{id}")]
		public IActionResult ShowAbsentNotes(int id, int pageNumber=1)
		{
			ViewBag.Childrens = this.GetLayoutData();
			ViewBag.StudentId = id;

			AbsentNoteListViewModel model = new AbsentNoteListViewModel();

			var notes = parentManager.GetAbsentNotesByStudentId(id).Where(x => x.Absences.Count() > 0).ToList();
			model.Notes = PaginatedList<AbsentNote>.Create(notes.AsQueryable(), pageNumber, 10);


			return View(model);
		}

		private List<StudentInfoSubModel> GetLayoutData()
		{
			Parent parent = parentManager.GetParentByEmail(User.Identity.Name);
			if (parent == null)
			{
				//error
			}
			List<Student> childrens = parentManager.GetStudentsByParentId(parent.Id).ToList();
			if (childrens == null || childrens.Count == 0)
			{
				//error
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
