using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.Interfaces;
using Common;
using DataAccess.EntityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Models;

namespace Web.Areas.Admin.Controllers
{
	[Authorize(Roles = "Admin")]
	[Area("Admin")]
	[Route("class")]
	public class ClassController : Controller
	{
		private readonly IAdminManager adminManager;
		public ClassController(IAdminManager adminManager)
		{
			this.adminManager = adminManager;
		}
		public IActionResult Index(int pageNumber = 1)
		{
			List<ClassViewModel> model = new List<ClassViewModel>();

			//get all users to UserViewModel
			var classes = adminManager.GetAllClasses().OrderBy(x => x.Name);

			foreach (var c in classes)
			{
				model.Add(new ClassViewModel()
				{
					Id = c.Id,
					Name = c.Name
				});
			}

			return View(PaginatedList<ClassViewModel>.Create(model.AsQueryable(), pageNumber, 5));
		}

		[HttpGet]
		[Route("AddClass")]
		public IActionResult AddClass()
		{
			ClassViewModel model = new ClassViewModel();
			this.FillStudents(ref model);
			this.FillClassTeachers(ref model);
			this.FillSubjects(ref model);

			return View(model);
		}

		[HttpPost]
		[Route("AddClass")]
		public IActionResult AddClass(ClassViewModel model)
		{
			Class c = new Class();
			c.Name = model.Name;
			c.ClassTeacher = adminManager.GetAllClassTeachers().Where(x => x.Id.Equals(model.ClassTeacher.ClassTeacherId)).FirstOrDefault();
			c.Students = adminManager.GetStudentsById(model.Students.StudentsId);

			if (model.Subjects.SubjectsId != null && model.Subjects.SubjectsId.Length > 0)
			{
				//add subjects
				List<Subject> subjects = adminManager.GetSubjectsById(model.Subjects.SubjectsId);
				c.ClassSubjects = new List<ClassSubject>();
				foreach (var s in subjects)
				{
					c.ClassSubjects.Add(new ClassSubject()
					{
						Subject = s,
						Class = c
					});
				}
			}

			if (!adminManager.CreateClass(c))
			{
				this.FillStudents(ref model);
				this.FillClassTeachers(ref model);
				this.FillSubjects(ref model);
				ModelState.AddModelError("", "Někde se stala chyba. Třída nebyla vytvořena.");
				return View(model);
			}


			return RedirectToAction("Index");
		}

		[HttpGet]
		[Route("EditClass")]
		public IActionResult EditClass(int id)
		{
			Class c = adminManager.GetClassById(id);
			ClassViewModel model = new ClassViewModel();
			this.FillClassTeachers(ref model);
			this.FillStudents(ref model);
			this.FillSubjects(ref model);

			model.Id = c.Id;
			model.Name = c.Name;
			if (c.ClassTeacher != null)
				model.ClassTeacher.ClassTeacherId = c.ClassTeacher.Id;

			//get student's parents
			if (c.Students != null)
			{
				int[] studentsIds = new int[c.Students.Count];
				for (int i = 0; i < c.Students.Count; i++)
				{
					studentsIds[i] = c.Students.ElementAt(i).Id;
				}
				model.Students.StudentsId = studentsIds;
			}

			if (c.ClassSubjects != null)
			{
				int[] subjectsIds = new int[c.ClassSubjects.Count];
				for (int i = 0; i < c.ClassSubjects.Count; i++)
				{
					subjectsIds[i] = c.ClassSubjects.ElementAt(i).SubjectId;
				}
				model.Subjects.SubjectsId = subjectsIds;
			}


			return View(model);
		}

		[HttpPost]
		[Route("EditClass")]
		public IActionResult EditClass(ClassViewModel model)
		{

			Class c = adminManager.GetClassById(model.Id);
			c.Name = model.Name;
			c.ClassTeacher = adminManager.GetAllClassTeachers().Where(x => x.Id.Equals(model.ClassTeacher.ClassTeacherId)).FirstOrDefault();

			if (model.Students.StudentsId != null && model.Students.StudentsId.Length > 0)
			{
				//handle students
				List<Student> students = adminManager.GetStudentsById(model.Students.StudentsId);
				if (c.Students != null)
				{
					//remove
					var classStudents = c.Students.ToList();
					foreach (var s in classStudents)
					{
						if (students == null || !students.Contains(s))
						{
							c.Students.Remove(s);
						}
					}
				}
				if (students != null)
				{
					//add
					foreach (var s in students)
					{
						if (!c.Students.Contains(s))
						{
							c.Students.Add(s);
						}
					}
				}
			}
			else if(c.Students.Count > 0)
			{
				c.Students = new List<Student>();
			}

			if (model.Subjects.SubjectsId != null && model.Subjects?.SubjectsId?.Length > 0)
			{
				//handle subjects
				List<Subject> subjects = adminManager.GetSubjectsById(model.Subjects.SubjectsId);
				//remove
				if (c.ClassSubjects != null)
				{
					var classSubjects = c.ClassSubjects.ToList();
					foreach (var s in classSubjects)
					{
						if (!subjects.Contains(s.Subject))
						{
							c.ClassSubjects.Remove(s);
						}
					}
				}
				//add
				if (subjects != null)
				{
					foreach (var s in subjects)
					{
						if (!c.ClassSubjects.Select(x => x.Subject).Contains(s))
						{
							c.ClassSubjects.Add(new ClassSubject()
							{
								Subject = s,
								Class = c
							});
						}
					}
				}
			}
			else if (c.ClassSubjects.Count > 0)
			{
				c.ClassSubjects = new List<ClassSubject>();
			}

			if (!adminManager.UpdateClass(c))
			{
				this.FillClassTeachers(ref model);
				this.FillStudents(ref model);
				this.FillSubjects(ref model);
				ModelState.AddModelError("", "Někde se stala chyba. Úpravy nebyly provedeny.");
				return View(model);
			}

			return RedirectToAction("Index");
		}

		[HttpGet]
		[Route("DetailClass")]
		public IActionResult DetailClass(int id)
		{
			Class c = adminManager.GetClassById(id);
			ClassDetailViewModel model = new ClassDetailViewModel();
			if (c != null)
			{
				model.Name = c.Name;
				model.ClassTeacher = c.ClassTeacher;
				model.Students = c.Students.ToList();
				model.Subjects = c.ClassSubjects.Select(x => x.Subject).ToList();

			}
			return View(model);
		}

		[HttpGet]
		[Route("DeleteClass")]
		public IActionResult DeleteClass(int id)
		{
			Class c = adminManager.GetClassById(id);
			if (c != null)
			{
				adminManager.DeleteClass(c);
			}
			return RedirectToAction("Index");
		}

		private void FillStudents(ref ClassViewModel model)
		{
			var students = adminManager.GetAllStudents();

			foreach (var student in students)
			{
				model.Students.Students.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
				{
					Text = student.LastName + " " + student.FirstName + " (" + student.Email + ")",
					Value = student.Id.ToString()
				});
			}
		}

		private void FillClassTeachers(ref ClassViewModel model)
		{
			var teachers = adminManager.GetAllClassTeachers();

			foreach (var t in teachers)
			{
				model.ClassTeacher.ClassTeachers.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
				{
					Text = t.LastName + " " + t.FirstName + " (" + t.Email + ")",
					Value = t.Id.ToString()
				});
			}
		}

		private void FillSubjects(ref ClassViewModel model)
		{
			var subjects = adminManager.GetAllSubjects();

			foreach (var subject in subjects)
			{
				model.Subjects.Subjects.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
				{
					Text = subject.Name,
					Value = subject.Id.ToString()
				});
			}
		}
	}
}
