using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.Interfaces;
using Common;
using DataAccess.EntityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Web.Areas.Admin.Models;

namespace Web.Areas.Admin.Controllers
{
	[Authorize(Roles = "Admin")]
	[Area("Admin")]
	[Route("subject")]
	public class SubjectController : Controller
	{
		private readonly IAdminManager adminManager;
		public SubjectController(IAdminManager adminManager)
		{
			this.adminManager = adminManager;
		}
		public IActionResult Index(int pageNumber = 1)
		{
			List<SubjectViewModel> model = new List<SubjectViewModel>();

			var subjects = adminManager.GetAllSubjects();
			foreach (var s in subjects)
			{
				model.Add(new SubjectViewModel()
				{
					Id = s.Id,
					Code = s.Code,
					Name = s.Name,

				});
			}

			return View(PaginatedList<SubjectViewModel>.Create(model.AsQueryable(), pageNumber, 5));
		}

		[HttpGet]
		[Route("AddSubject")]
		public IActionResult AddSubject()
		{

			SubjectViewModel model = new SubjectViewModel();

			this.FillTeachers(ref model);

			return View(model);
		}

		[HttpPost]
		[Route("AddSubject")]
		public IActionResult AddSubject(SubjectViewModel model)
		{

			Subject s = new Subject();
			s.Code = model.Code;
			s.Name = model.Name;
			s.TeacherSubjects = new List<TeacherSubject>();

			if (model.Teachers.TeacherIds != null && model.Teachers.TeacherIds.Length > 0)
			{
				List<Teacher> teachers = adminManager.GetTeachersById(model.Teachers.TeacherIds);
				foreach (var t in teachers)
				{
					s.TeacherSubjects.Add(new TeacherSubject()
					{
						Subject = s,
						Teacher = t
					});
				}
			}

			if (!adminManager.CreateSubject(s))
			{
				this.FillTeachers(ref model);
				ModelState.AddModelError("", "Někde se stala chyba. Předmět nebyl vytvořen.");
				return View(model);
			}

			return RedirectToAction("Index");
		}

		[HttpGet]
		[Route("EditSubject")]
		public IActionResult EditSubject(int id)
		{
			Subject s = adminManager.GetSubjectById(id);
			SubjectViewModel model = new SubjectViewModel();
			this.FillTeachers(ref model);

			model.Id = s.Id;
			model.Code = s.Code;
			model.Name = s.Name;

			//get student's parents
			int[] teachersIds = new int[s.TeacherSubjects.Select(t => t.Teacher).Count()];
			for (int i = 0; i < s.TeacherSubjects.Select(t => t.Teacher).Count(); i++)
			{
				teachersIds[i] = s.TeacherSubjects.Select(t => t.Teacher).ElementAt(i).Id;
			}
			model.Teachers.TeacherIds = teachersIds;


			return View(model);
		}

		[HttpPost]
		[Route("EditSubject")]
		public IActionResult EditSubject(SubjectViewModel model)
		{
			Subject s = adminManager.GetSubjectById(model.Id);
			s.Code = model.Code;
			s.Name = model.Name;

			if (model.Teachers.TeacherIds != null && model.Teachers.TeacherIds.Length > 0)
			{
				//handle students
				List<Teacher> teachers = adminManager.GetTeachersById(model.Teachers.TeacherIds);

				//remove
				var teacherSubjects = s.TeacherSubjects.ToList();
				foreach (var ts in teacherSubjects)
				{
					if (teachers == null || !teachers.Contains(ts.Teacher))
					{
						s.TeacherSubjects.Remove(ts);
					}
				}
				if (teachers != null)
				{
					//add
					foreach (var t in teachers)
					{
						if (!s.TeacherSubjects.Select(x => x.Teacher).Contains(t))
						{
							s.TeacherSubjects.Add(new TeacherSubject()
							{
								Teacher = t,
								Subject = s
							});
						}
					}
				}
			}
			else if(s.TeacherSubjects.Count > 0)
			{
				s.TeacherSubjects = new List<TeacherSubject>();
			}

			if (!adminManager.UpdateSubject(s))
			{
				this.FillTeachers(ref model);
				ModelState.AddModelError("", "Někde se stala chyba. Změny nebyly provedeny.");
				return View(model);
			}

			return RedirectToAction("Index");
		}

		[HttpGet]
		[Route("DetailSubject")]
		public IActionResult DetailSubject(int id)
		{
			Subject s = adminManager.GetSubjectById(id);
			SubjectDetailViewModel model = new SubjectDetailViewModel();
			if (s != null)
			{
				model.Code = s.Code;
				model.Name = s.Name;
				model.Teachers = s.TeacherSubjects.Select(x => x.Teacher).ToList();
			}
			return View(model);
		}

		[HttpGet]
		[Route("DeleteSubject")]
		public IActionResult DeleteSubject(int id)
		{
			Subject s = adminManager.GetSubjectById(id);
			if (s != null)
			{
				adminManager.DeleteSubject(s);
			}
			return RedirectToAction("Index");
		}

		private void FillTeachers(ref SubjectViewModel model)
		{
			var teachers = adminManager.GetAllTeachers();

			foreach (var t in teachers)
			{
				model.Teachers.Teachers.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
				{
					Text = t.LastName + " " + t.FirstName + " (" + t.Email + ")",
					Value = t.Id.ToString()
				});
			}
		}
	}
}
