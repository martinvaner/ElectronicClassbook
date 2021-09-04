using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Classbook.Interfaces;
using Common;
using DataAccess.EntityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Classbook.Models;

namespace Web.Areas.Classbook.Controllers
{
	[Authorize(Roles = "Admin, Ředitel, Zástupce ředitele, Třídní učitel, Učitel")]
	[Area("Classbook")]
	[Route("student")]
	public class StudentController : Controller
	{

		private readonly IClassbookManager classbookManager;
		public StudentController(IClassbookManager classbookManager)
		{
			this.classbookManager = classbookManager;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="id">Classbook id</param>
		/// <returns></returns>
		public IActionResult Index(int id, int pageNumber = 1)
		{
			List<StudentListViewModel> model = new List<StudentListViewModel>();

			var classbook = classbookManager.GetAllClassbooks().Where(x => x.Id.Equals(id)).FirstOrDefault();

			var students = classbookManager.GetStudentsByClassId(classbook.ClassId);

			foreach (var stud in students)
			{
				model.Add(new StudentListViewModel()
				{
					Id = stud.Id,
					Email = stud.Email,
					FirstName = stud.FirstName,
					LastName = stud.LastName
				});
			}

			ViewBag.ClassbookId = id;
			ViewBag.ClassName = classbookManager.GetClassName(id);
			return View(PaginatedList<StudentListViewModel>.Create(model.AsQueryable(), pageNumber, 6));
		}

		[Authorize(Roles = "Učitel")]
		[HttpGet]
		[Route("CreateSchoolHomeNote/{classbookId}/{studentId}")]
		public IActionResult CreateSchoolHomeNote(int classbookId, int studentId)
		{
			SchoolHomeNoteViewModel model = new SchoolHomeNoteViewModel();

			var student = classbookManager.GetStudentById(studentId);
			var teacher = classbookManager.GetTeacherByEmail(User.Identity.Name);
			if (student != null && teacher != null)
			{
				model.Student = student;
				model.CreatedBy = teacher;
				model.Created = DateTime.Today;
			}

			ViewBag.ClassbookId = classbookId;
			ViewBag.ClassName = classbookManager.GetClassName(classbookId);
			return View(model);
		}

		[HttpPost]
		[Route("CreateSchoolHomeNote/{classbookId}/{studentId}")]
		public IActionResult CreateSchoolHomeNote(int classbookId, int studentId, SchoolHomeNoteViewModel model)
		{
			if(model.Created == null)
			{
				return View(model);
			}
			if(model.Created < DateTime.Now.Date.AddDays(-30))
			{
				ModelState.AddModelError("", "Poznámku s tímto datem nelze vytvořit.");
				ViewBag.ClassbookId = classbookId;
				ViewBag.ClassName = classbookManager.GetClassName(classbookId);
				return View(model);
			}

			var student = classbookManager.GetStudentById(model.Student.Id);
			var teacher = classbookManager.GetTeacherByEmail(model.CreatedBy.Email);
			if (student != null && teacher != null)
			{
				SchoolHomeNote note = new SchoolHomeNote();
				note.Created = model.Created;
				note.CreatedBy = teacher;
				note.Note = model.Note;
				note.Student = student;

				if(!classbookManager.CreateSchoolHomeNote(note))
				{
					ModelState.AddModelError("", "Někde se stala chyba. Poznámka nebyla vytvořena.");
					ViewBag.ClassbookId = classbookId;
					ViewBag.ClassName = classbookManager.GetClassName(classbookId);
					return View(model);
				}
			}


			return RedirectToAction("Index", new { id = classbookId });
		}

		[HttpGet]
		[Route("ListSchoolHomeNote/{classbookId}/{studentId}")]
		public IActionResult ListSchoolHomeNote(int classbookId, int studentId, int pageNumber = 1)
		{
			List<SchoolHomeNoteViewModel> model = new List<SchoolHomeNoteViewModel>();

			var notes = classbookManager.GetSchoolHomeNotesByStudentId(studentId).OrderByDescending(x => x.Created);

			if (notes != null && notes.Count() > 0)
			{
				foreach (var note in notes)
				{
					model.Add(new SchoolHomeNoteViewModel()
					{
						Created = note.Created,
						CreatedBy = note.CreatedBy,
						Id = note.Id,
						Note = note.Note,
						Student = note.Student
					});
				}
			}
			ViewBag.ClassbookId = classbookId;
			ViewBag.ClassName = classbookManager.GetClassName(classbookId);
			ViewBag.StudentId = studentId;
			return View(PaginatedList<SchoolHomeNoteViewModel>.Create(model.AsQueryable(), pageNumber, 6));
		}

		[HttpGet]
		[Route("DeleteSchoolHomeNote/{classbookId}/{studentId}/{noteId}")]
		public IActionResult DeleteSchoolHomeNote(int classbookId, int studentId, int noteId)
		{
			var note = classbookManager.GetSchoolHomeNotesByStudentId(studentId).Where(x => x.Id.Equals(noteId)).FirstOrDefault();

			if (note != null)
			{
				classbookManager.DeleteSchoolHomeNote(note);
			}

			return RedirectToAction("ListSchoolHomeNote", new { classbookId = classbookId, studentId = studentId });
		}
	}
}
