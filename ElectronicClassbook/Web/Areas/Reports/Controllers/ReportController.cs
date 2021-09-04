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
	[Authorize(Roles = "Učitel, Třídní učitel, Ředitel, Zástupce ředitele, Admin")]
	[Area("Reports")]
	[Route("report")]
	public class ReportController : Controller
	{
		private readonly IReportManager reportManager;

		public ReportController(IReportManager reportManager)
		{
			this.reportManager = reportManager;
		}

		[HttpGet]
		[Route("TeacherDashboard")]
		public IActionResult TeacherDashboard()
		{
			TeacherReportViewModel model = new TeacherReportViewModel();

			model.Homeworks = reportManager.GetTeachersHomeworksByDeadline(User.Identity.Name, DateTime.Today).ToList();

			if(User.IsInRole("Třídní učitel"))
			{
				model.AbsentNotes = reportManager.GetAbsentNotesForApproval(User.Identity.Name).ToList();
			}

			return View(model);
		}

		public IActionResult Index(int? classId, string firstName, string lastName, int pageNumber = 1)
		{
			List<Student> students = new List<Student>();
			StudentListViewModel model = new StudentListViewModel();
			this.FillClasses(ref model);
			if (classId != null)
				model.Class.ClassId = classId.Value;

			if(classId != null)
			{
				students = reportManager.GetStudentsByClassId(classId.Value).ToList();
			}

			model.Students = PaginatedList<Student>.Create(students.AsQueryable(), pageNumber, 6);
			return View(model);
		}

		[HttpGet]
		[Route("ShowStudent/{id}")]
		public IActionResult ShowStudent(int id)
		{
			// show info about student - home-school notes, absence, homeworks, - show it based on subjects

			return RedirectToAction("ShowAbsences", new { studentId = id });
		}

		[HttpGet]
		[Route("ShowAbsences/{studentId}")]
		public IActionResult ShowAbsences(int studentId, int pageNumber=1)
		{
			Student student = reportManager.GetStudentById(studentId);


			AbsenceListViewModel model = new AbsenceListViewModel();

			// get list of absences
			List<DataAccess.EntityModel.Attendance> absences = reportManager.GetAbsenceByStudentId(studentId).ToList();
			if (absences == null)
			{
				//error
			}

			//Groups by created date
			var absencesByDate = absences.GroupBy(x => x.Record.Created);
			List<AbsenceInfoSubModel> absenceList = new List<AbsenceInfoSubModel>();
			foreach (var absence in absencesByDate)
			{
				absenceList.Add(new AbsenceInfoSubModel()
				{
					Date = absence.Key,
					MissedClasses = absence.Count(),
					IsExcused = absences.Where(x => x.Record.Created.Equals(absence.Key)).Count(x => x.IsExcused) > 0 ? true : false,
					Absences = absences.Where(x => x.Record.Created.Equals(absence.Key)).ToList()
				});
			}

			model.Absences = PaginatedList<AbsenceInfoSubModel>.Create(absenceList.AsQueryable(), pageNumber, 10);
			ViewBag.StudentId = studentId;
			ViewBag.FullName = student.FullName;
			ViewBag.ClassName = student.Class.Name;
			return View(model);
		}

		[HttpGet]
		[Route("ShowHomeworks/{studentId}")]
		public IActionResult ShowHomeworks(int studentId, int pageNumber=1)
		{
			Student student = reportManager.GetStudentById(studentId);

			List<Homework> homeworks = reportManager.GetHomeworksByStudentId(studentId).Where(x => x.Deadline > DateTime.Today).ToList();
			if (homeworks == null)
			{
				//error
			}

			HomeworkListViewModel model = new HomeworkListViewModel();

			model.Homeworks = PaginatedList<Homework>.Create(homeworks.AsQueryable(), pageNumber, 10);

			ViewBag.StudentId = studentId;
			ViewBag.FullName = student.FullName;
			ViewBag.ClassName = student.Class.Name;
			return View(model);
		}

		[HttpGet]
		[Route("DetailHomework/{studentId}/{homeworkId}")]
		public IActionResult DetailHomework(int studentId, int homeworkId)
		{
			Student student = reportManager.GetStudentById(studentId);

			HomeworkDetailViewModel model = new HomeworkDetailViewModel();
			Homework homework = reportManager.GetHomeworkById(homeworkId);
			if (homework == null)
			{
				//error
			}

			model.homework = homework;

			ViewBag.StudentId = studentId;
			ViewBag.FullName = student.FullName;
			ViewBag.ClassName = student.Class.Name;
			return View(model);
		}

		[HttpGet]
		[Route("ShowHomeSchoolNotes/{studentId}")]
		public IActionResult ShowHomeSchoolNotes(int studentId, int pageNumber=1)
		{
			Student student = reportManager.GetStudentById(studentId);

			List<SchoolHomeNote> notes = reportManager.GetSchoolHomeNotesByStudentId(studentId).ToList();
			if (notes == null)
			{
				//error
			}

			SchoolHomeNoteListViewModel model = new SchoolHomeNoteListViewModel();
			model.Notes = PaginatedList<SchoolHomeNote>.Create(notes.AsQueryable(), pageNumber, 10);

			ViewBag.StudentId = studentId;
			ViewBag.FullName = student.FullName;
			ViewBag.ClassName = student.Class.Name;
			return View(model);
		}

		private void FillClasses(ref StudentListViewModel model)
		{
			var classes = reportManager.GetAllClasses();

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
