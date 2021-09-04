using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Attendance.Interfaces;
using Common;
using DataAccess.EntityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Attendance.Models;
using Web.Areas.Attendance.Models.Submodels;

namespace Web.Areas.Attendance.Controllers
{
	[Authorize(Roles = "Třídní učitel")]
	[Area("Attendance")]
	[Route("classTeacher")]
	public class ClassTeacherController : Controller
	{
		private readonly IClassTeacherManager classTeacherManager;

		public ClassTeacherController(IClassTeacherManager classTeacherManager)
		{
			this.classTeacherManager = classTeacherManager;
		}

		[HttpGet]
		[Route("ShowAbsentNotes")]
		public IActionResult ShowAbsentNotes()
		{
			AbsentNoteListViewModel model = new AbsentNoteListViewModel();

			model.Notes = classTeacherManager.GetAbsentNotesByClassTeacherEmail(User.Identity.Name)?.ToList();

			return View(model);
		}
		[HttpGet]
		[Route("ShowStudents")]
		public IActionResult ShowStudents(int pageNumber=1)
		{
			StudentAbsenceListViewModel model = new StudentAbsenceListViewModel();

			var classStudents = classTeacherManager.GetStudentsByClassTeacherEmail(User.Identity.Name);

			List<StudentAbsenceInfoSubModel> students = new List<StudentAbsenceInfoSubModel>(); 
			foreach(var student in classStudents)
			{
				students.Add(new Models.Submodels.StudentAbsenceInfoSubModel()
				{ 
					Student = student,
					AbsencesCount = student.Attendances.Where(x => x.Present == false).Where(x => x.IsExcused == false).Count()
				});
			}
			model.Students = PaginatedList<StudentAbsenceInfoSubModel>.Create(students.AsQueryable(), pageNumber, 10);

			return View(model);
		}

		[HttpGet]
		[Route("ShowStudentAbsences/{studentId}")]
		public IActionResult ShowStudentAbsences(int studentId, int pageNumber=1)
		{
			AbsenceListViewModel model = new AbsenceListViewModel();
			model.Student = classTeacherManager.GetStudentById(studentId);

			// get list of absences
			List<DataAccess.EntityModel.Attendance> absences = classTeacherManager.GetAbsenceByStudentId(studentId).ToList();

			absences = absences.Where(x => x.IsExcused == false).Where(x => (x.AbsentNoteId == null || x.AbsentNote.State.Name.Equals("Zamítnuta"))).ToList();

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
			return View(model);
		}

		[HttpPost]
		[Route("CreateAbsentNote/{studentId}")]
		public IActionResult CreateAbsentNote(int studentId, AbsentNoteViewModel model)
		{
			AbsentNote an = new AbsentNote();
			an.Created = DateTime.Today;
			an.Text = model.Text;
			an.State = classTeacherManager.GetAbsentNoteStateByName("Potvrzena");
			an.ClassTeacher = classTeacherManager.GetClassTeacherByEmail(User.Identity.Name);
			an.Student = classTeacherManager.GetStudentById(studentId);

			List<DataAccess.EntityModel.Attendance> absences = new List<DataAccess.EntityModel.Attendance>();
			foreach (var absence in model.Absences)
			{
				DataAccess.EntityModel.Attendance a = classTeacherManager.GetAttendanceById(absence.Id);
				a.IsExcused = true;
				absences.Add(a);
			}
			an.Absences = absences;

			if (!classTeacherManager.CreateAbsentNote(an))
			{
				ModelState.AddModelError("", "Někde se stala chyba. Omluvenka nebyla vytvořena.");
			}

			return RedirectToAction("ShowStudentAbsences", new { studentId = studentId });

		}

		[HttpPost]
		[Route("ToBeCreatedAbsentNote/{studentId}")]
		public IActionResult ToBeCreatedAbsentNote(int studentId, AbsenceListViewModel model)
		{
			AbsentNoteViewModel m = new AbsentNoteViewModel();

			foreach (var absence in model.Absences)
			{
				if (absence.Selected)
				{
					m.Absences.AddRange(absence.Absences);
				}
			}

			if (m.Absences.Count == 0)
			{
				//error
				ModelState.AddModelError("", "Nebyla vybrána žádná absence k omluvení.");
			}

			ViewBag.StudentId = studentId;
			return View(m);

		}

		[HttpGet]
		[Route("ConfirmAbsentNote/{noteId}")]
		public IActionResult ConfirmAbsentNote(int noteId)
		{
			AbsentNote absentNote = classTeacherManager.GetAbsentNoteById(noteId);
			if (absentNote == null)
			{ 
				//error
			}

			//state to "Potvrzena"
			absentNote.State = classTeacherManager.GetAbsentNoteStateByName("Potvrzena");

			//set absences to IsExcused = true
			foreach(var absence in absentNote.Absences)
			{
				absence.IsExcused = true;
			}

			classTeacherManager.UpdateAbsentNote(absentNote);

			return RedirectToAction("ShowAbsentNotes");
		}

		[HttpGet]
		[Route("DenyAbsentNote/{noteId}")]
		public IActionResult DenyAbsentNote(int noteId)
		{
			AbsentNote absentNote = classTeacherManager.GetAbsentNoteById(noteId);
			if (absentNote == null)
			{
				//error
			}

			//state to "Zamítnuta"
			absentNote.State = classTeacherManager.GetAbsentNoteStateByName("Zamítnuta");

			classTeacherManager.UpdateAbsentNote(absentNote);

			return RedirectToAction("ShowAbsentNotes");
		}
	}
}
