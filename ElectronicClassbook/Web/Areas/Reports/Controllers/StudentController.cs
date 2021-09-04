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
	[Authorize(Roles = "Žák")]
	[Area("Reports")]
	[Route("student")]
	public class StudentController : Controller
	{

		private readonly IParentStudentManager studentManager;
		public StudentController(IParentStudentManager studentManager)
		{
			this.studentManager = studentManager;
		}

		[Route("Index")]
		public IActionResult Index()
		{
			//absence, homeworks, notes7
			StudentReportViewModel model = new StudentReportViewModel();

			Student student = studentManager.GetStudentByEmail(User.Identity.Name);

			List<DataAccess.EntityModel.Attendance> absence = studentManager.GetAbsencesByStudentId(student.Id).Where(x => x.IsExcused == false)
																	.Where(x => (x.AbsentNoteId == null || x.AbsentNote.State.Name.Equals("Zamítnuta"))).ToList();
			List<Homework> homeworks = studentManager.GetHomeworksByStudentId(student.Id).Where(x => x.ShowStudent == true).Where(x => x.Deadline > DateTime.Today).ToList();
			List<SchoolHomeNote> notes = studentManager.GetSchoolHomeNotesByStudentId(student.Id).Where(x => x.ShowStudent == true).ToList();

			model.Id = student.Id;
			model.FirstName = student.FirstName;
			model.LastName = student.LastName;
			model.Email = student.Email;
			model.Absences = absence;
			model.Homeworks = homeworks;
			model.Notes = notes;

			return View(model);
		}

		[HttpGet]
		[Route("ShowHomeworks")]
		public IActionResult ShowHomeworks()
		{
			Student student = studentManager.GetStudentByEmail(User.Identity.Name);

			return RedirectToAction("ShowHomeworks", new { id = student.Id });
		}

		[HttpGet]
		[Route("ShowHomeworks/{id}")]
		[Route("ShowHomeworks/{id}/{all}")]
		public IActionResult ShowHomeworks(int id, bool all = false, int pageNumber=1)
		{
			ViewBag.StudentId = id;

			//get homeworks for student
			List<Homework> homeworks = studentManager.GetHomeworksByStudentId(id).Where(x => x.Deadline > DateTime.Today).ToList();
			if (homeworks == null)
			{
				//error
			}
			List<Homework> newHomeworks = homeworks.Where(x => x.ShowStudent == true).ToList();


			HomeworkListViewModel model = new HomeworkListViewModel();
			if (all)
				model.Homeworks = PaginatedList<Homework>.Create(homeworks.AsQueryable(), pageNumber, 10);
			else
				model.Homeworks = PaginatedList<Homework>.Create(newHomeworks.AsQueryable(), pageNumber, 10);

			ViewBag.NewHomeworksCount = newHomeworks.Count(x => x.ShowStudent == true);
			return View(model);
		}


		[HttpGet]
		[Route("DetailHomework/{id}/{homeworkId}")]
		public IActionResult DetailHomework(int id, int homeworkId)
		{
			ViewBag.StudentId = id;

			HomeworkDetailViewModel model = new HomeworkDetailViewModel();
			Homework homework = studentManager.GetHomeworkById(homeworkId);
			if (homework == null)
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

			Homework homework = studentManager.GetHomeworkById(homeworkId);
			if (homework == null)
			{
				//error
			}

			homework.ShowStudent = false;
			studentManager.UpdateHomework(homework);

			return RedirectToAction("ShowHomeworks", new { id });
		}



		[HttpGet]
		[Route("ShowSchoolHomeNotes")]
		public IActionResult ShowSchoolHomeNotes()
		{
			Student student = studentManager.GetStudentByEmail(User.Identity.Name);

			return RedirectToAction("ShowSchoolHomeNotes", new { id = student.Id });
		}

		[HttpGet]
		[Route("ShowSchoolHomeNotes/{id}")]
		[Route("ShowSchoolHomeNotes/{id}/{all}")]
		public IActionResult ShowSchoolHomeNotes(int id, bool all = false, int pageNumber=1)
		{
			ViewBag.StudentId = id;

			List<SchoolHomeNote> notes = studentManager.GetSchoolHomeNotesByStudentId(id).ToList();
			if (notes == null)
			{
				//error
			}
			List<SchoolHomeNote> newNotes = notes.Where(x => x.ShowStudent == true).ToList();

			SchoolHomeNoteListViewModel model = new SchoolHomeNoteListViewModel();
			if (all)
				model.Notes = PaginatedList<SchoolHomeNote>.Create(notes.AsQueryable(), pageNumber, 10);
			else
				model.Notes = PaginatedList<SchoolHomeNote>.Create(newNotes.AsQueryable(), pageNumber, 10);

			ViewBag.NewNotesCount = newNotes.Count(x => x.ShowStudent == true);
			return View(model);
		}


		[HttpGet]
		[Route("HideSchoolHomeNote/{id}/{noteId}")]
		public IActionResult HideSchoolHomeNote(int id, int noteId)
		{
			SchoolHomeNote note = studentManager.GetSchoolHomeNoteById(noteId);
			if (note == null)
			{
				//error
			}

			note.ShowStudent = false;
			studentManager.UpdateSchoolHomeNote(note);

			return RedirectToAction("ShowSchoolHomeNotes", new { id });
		}


		[HttpGet]
		[Route("ShowAbsentNotes")]
		public IActionResult ShowAbsentNotes()
		{
			Student student = studentManager.GetStudentByEmail(User.Identity.Name);

			return RedirectToAction("ShowAbsentNotes", new { id = student.Id });
		}

		[HttpGet]
		[Route("ShowAbsentNotes/{id}")]
		public IActionResult ShowAbsentNotes(int id, int pageNumber=1)
		{
			ViewBag.StudentId = id;

			AbsentNoteListViewModel model = new AbsentNoteListViewModel();

			var notes = studentManager.GetAbsentNotesByStudentId(id).Where(x => x.Absences.Count() > 0).ToList();
			model.Notes = PaginatedList<AbsentNote>.Create(notes.AsQueryable(), pageNumber, 10);


			return View(model);
		}

		[HttpGet]
		[Route("ShowAbsences")]
		public IActionResult ShowAbsences()
		{
			Student student = studentManager.GetStudentByEmail(User.Identity.Name);

			return RedirectToAction("ShowAbsences", new { id = student.Id });
		}

		[HttpGet]
		[Route("ShowAbsences/{id}")]
		[Route("ShowAbsences/{id}/{all}")]
		public IActionResult ShowAbsences(int id, bool all = false, int pageNumber=1)
		{
			ViewBag.StudentId = id;

			AbsenceListViewModel model = new AbsenceListViewModel();

			// get list of absences
			List<DataAccess.EntityModel.Attendance> absences = studentManager.GetAbsencesByStudentId(id).ToList();
			if (absences == null)
			{
				//error
			}
			if (!all)
				absences = absences.Where(x => x.IsExcused == false).Where(x => (x.AbsentNoteId == null || x.AbsentNote.State.Name.Equals("Zamítnuta"))).ToList();

			//Groups by created date
			var absencesByDate = absences.GroupBy(x => x.Record.Created);
			List<AbsenceInfoSubModel> absenceList = new List<AbsenceInfoSubModel>();
			foreach (var absence in absencesByDate)
			{
				absenceList.Add(new AbsenceInfoSubModel()
				{
					Date = absence.Key,
					MissedClasses = absence.Count(),
					Absences = absences.Where(x => x.Record.Created.Equals(absence.Key)).ToList()
				});
			}
			model.Absences = PaginatedList<AbsenceInfoSubModel>.Create(absenceList.AsQueryable(), pageNumber, 10);

			ViewBag.NewAbsences = absences.Where(x => (x.AbsentNoteId == null || x.AbsentNote.State.Name.Equals("Zamítnuta"))).Count(x => x.IsExcused == false);
			ViewBag.ShowAll = all;

			return View(model);
		}
	}
}
