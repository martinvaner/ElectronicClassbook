using System;
using System.Collections.Generic;
using System.Linq;
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
	[Route("user")]
	public class UserController : Controller
	{
		private readonly IAdminManager adminManager;
		public UserController(IAdminManager adminManager)
		{
			this.adminManager = adminManager;
		}

		[HttpGet]
		[Route("StudentList")]
		public IActionResult StudentList(string searchText, int pageNumber = 1)
		{
			List<UserViewModel> model = new List<UserViewModel>();

			var students = adminManager.GetAllStudents();
			foreach (var student in students)
			{
				model.Add(new UserViewModel()
				{
					Id = student.Id,
					FirstName = student.FirstName,
					LastName = student.LastName,
					Email = student.Email
				});
			}

			//filter
			if (!string.IsNullOrEmpty(searchText))
			{
				model = model.Where(u => u.Email.Contains(searchText)).ToList();
			}
			ViewBag.CurrentFilter = searchText;

			return View(PaginatedList<UserViewModel>.Create(model.AsQueryable(), pageNumber, 5));
		}

		[HttpGet]
		[Route("AddStudent")]
		public IActionResult AddStudent()
		{
			StudentViewModel model = new StudentViewModel();
			model.EnrollmentDate = DateTime.Today;

			this.FillParents(ref model);

			return View(model);
		}

		[HttpPost]
		[Route("AddStudent")]
		public IActionResult AddStudent(StudentViewModel model, bool makeHash = true)
		{
			if (string.IsNullOrEmpty(model.Password))
			{
				this.FillParents(ref model);
				ModelState.AddModelError("", "Heslo musí být vyplněno.");
				return View(model);
			}
			if (model.DateOfBirth > DateTime.Today)
			{
				this.FillParents(ref model);
				ModelState.AddModelError("", "Datum narození musí být v minulosti.");
				return View(model);
			}

			if (model.Gender.GenderId == 0 || model.Gender.GenderId == 1)
			{

				Student student = new Student();
				student.FirstName = model.FirstName;
				student.LastName = model.LastName;
				student.Email = model.Email;
				student.Password = model.Password;
				student.Address = model.Address;
				student.PersonalIdentificationNumber = model.PersonalIdentificationNumber;
				student.Gender = model.Gender.GenderId == 1 ? true : false;
				student.DateOfBirth = model.DateOfBirth;
				student.BirthLocation = model.BirthLocation;
				student.Citizenship = model.Citizenship;
				student.UserRoles = new List<UserRole>()
						{
							new UserRole()
							{
								Role = adminManager.GetAllRoles().Where(x => x.Name.Equals("Žák")).FirstOrDefault(),
								User = student
							}
						};
				student.EnrollmentDate = model.EnrollmentDate;
				if (model.Parents?.ParentsId != null && model.Parents?.ParentsId.Length > 0)
				{
					List<Parent> parents = adminManager.GetParentsById(model.Parents.ParentsId);
					student.ParentStudents = new List<ParentStudent>();
					foreach (var p in parents)
					{
						student.ParentStudents.Add(
							new ParentStudent()
							{
								Parent = p,
								Student = student
							});
					}
				}

				if (!adminManager.CreateStudent(student, makeHash))
				{
					this.FillParents(ref model);
					ModelState.AddModelError("", "Někde se stala chyba. Student nebyl vytvořen.");
					return View(model);
				}
			}
			else
			{
				this.FillParents(ref model);
				return View(model);
			}

			return RedirectToAction("StudentList", "User");
		}

		[HttpGet]
		[Route("DetailStudent/{id}")]
		public IActionResult DetailStudent(int id)
		{
			StudentDetailViewModel model = new StudentDetailViewModel();
			Student student = adminManager.GetStudentById(id);

			if (student != null)
			{
				model.FirstName = student.FirstName;
				model.LastName = student.LastName;
				model.Email = student.Email;
				model.Address = student.Address;
				model.EnrollmentDate = student.EnrollmentDate;
				model.PersonalIdentificationNumber = student.PersonalIdentificationNumber;
				model.Gender = student.Gender;
				model.DateOfBirth = student.DateOfBirth;
				model.BirthLocation = student.BirthLocation;
				model.Citizenship = student.Citizenship;
				model.Parents = student.ParentStudents.Select(x => x.Parent).ToList();
			}

			return View(model);
		}

		[HttpGet]
		[Route("EditStudent/{id}")]
		public IActionResult EditStudent(int id)
		{

			StudentViewModel model = new StudentViewModel();
			this.FillParents(ref model);
			model.EnrollmentDate = DateTime.Today;

			Student student = adminManager.GetStudentById(id);

			if (student != null)
			{
				model.FirstName = student.FirstName;
				model.LastName = student.LastName;
				model.Email = student.Email;
				model.Address = student.Address;
				model.EnrollmentDate = student.EnrollmentDate;
				model.PersonalIdentificationNumber = student.PersonalIdentificationNumber;
				model.Gender.GenderId = student.Gender.Value ? 1 : 0;
				model.DateOfBirth = student.DateOfBirth;
				model.BirthLocation = student.BirthLocation;
				model.Citizenship = student.Citizenship;

				//get student's parents
				int[] parentsIds = new int[student.ParentStudents.Count];
				for (int i = 0; i < student.ParentStudents.Count; i++)
				{
					parentsIds[i] = student.ParentStudents.ElementAt(i).ParentId;
				}
				model.Parents.ParentsId = parentsIds;
			}

			return View(model);
		}

		[HttpPost]
		[Route("EditStudent/{id}")]
		public IActionResult EditStudent(int id, StudentViewModel model)
		{
			if(!ModelState.IsValid)
			{
				this.FillParents(ref model);
				return View(model);
			}
			if (model.DateOfBirth > DateTime.Today)
			{
				this.FillParents(ref model);
				ModelState.AddModelError("", "Datum narození musí být v minulosti.");
				return View(model);
			}

			if (model.Gender.GenderId == 0 || model.Gender.GenderId == 1)
			{ 
				Student student = adminManager.GetStudentById(id);
				student.FirstName = model.FirstName;
				student.LastName = model.LastName;
				student.Email = model.Email;
				student.Address = model.Address;
				student.EnrollmentDate = model.EnrollmentDate;
				student.PersonalIdentificationNumber = model.PersonalIdentificationNumber;
				student.Gender = model.Gender.GenderId == 1 ? true : false;
				student.DateOfBirth = model.DateOfBirth;
				student.BirthLocation = model.BirthLocation;
				student.Citizenship = model.Citizenship;

				if (model.Parents?.ParentsId != null && model.Parents?.ParentsId.Length > 0)
				{
					List<Parent> parents = adminManager.GetParentsById(model.Parents.ParentsId);
					//remove
					var parentStudents = student.ParentStudents.ToList();
					foreach (var s in parentStudents)
					{
						if (!parents.Contains(s.Parent))
						{
							student.ParentStudents.Remove(s);
						}
					}
					//add
					if (parents != null)
					{
						foreach (var p in parents)
						{
							if (!student.ParentStudents.Select(x => x.Parent).Contains(p))
							{
								student.ParentStudents.Add(
								new ParentStudent()
								{
									Parent = p,
									Student = student
								});
							}
						}
					}
				}
				else if (student.ParentStudents.Count > 0)
				{
					student.ParentStudents = new List<ParentStudent>();
				}


				if (!adminManager.UpdateStudent(student))
				{
					this.FillParents(ref model);
					ModelState.AddModelError("", "Někde se stala chyba. Úpravy nebyly provedeny.");
					return View(model);
				}
			}
			else
			{
				this.FillParents(ref model);
				return View(model);
			}

			return RedirectToAction("StudentList", "User");
		}

		[HttpGet]
		[Route("DeleteStudent/{id}")]
		public IActionResult DeleteStudent(int id)
		{
			DeleteUser(id);
			return RedirectToAction("StudentList", "User");
		}

		[HttpGet]
		[Route("ParentList")]
		public IActionResult ParentList(string searchText, int pageNumber = 1)
		{

			List<UserViewModel> model = new List<UserViewModel>();

			var parents = adminManager.GetAllParents();
			foreach (var parent in parents)
			{
				model.Add(new UserViewModel()
				{
					Id = parent.Id,
					FirstName = parent.FirstName,
					LastName = parent.LastName,
					Email = parent.Email
				});
			}

			//filter
			if (!string.IsNullOrEmpty(searchText))
			{
				model = model.Where(u => u.Email.Contains(searchText)).ToList();
			}
			ViewBag.CurrentFilter = searchText;

			return View(PaginatedList<UserViewModel>.Create(model.AsQueryable(), pageNumber, 5));
		}

		[HttpGet]
		[Route("AddParent")]
		public IActionResult AddParent()
		{
			ParentViewModel model = new ParentViewModel();
			this.FillStudents(ref model);

			return View(model);
		}

		[HttpPost]
		[Route("AddParent")]
		public IActionResult AddParent(ParentViewModel model, bool makeHash = true)
		{
			if (!ModelState.IsValid)
			{
				this.FillStudents(ref model);
				return View(model);
			}
			if (string.IsNullOrEmpty(model.Password))
			{
				this.FillStudents(ref model);
				ModelState.AddModelError("", "Heslo musí být vyplněno.");
				return View(model);
			}

			Parent parent = new Parent();
			parent.FirstName = model.FirstName;
			parent.LastName = model.LastName;
			parent.Email = model.Email;
			parent.Password = model.Password;
			parent.Address = model.Address;
			parent.UserRoles = new List<UserRole>()
						{
							new UserRole()
							{
								Role = adminManager.GetAllRoles().Where(x => x.Name.Equals("Rodič")).FirstOrDefault(),
								User = parent
							}
						};
			parent.PhoneNumber = model.PhoneNumber;
			if (model.Childrens?.StudentsId != null && model.Childrens?.StudentsId.Length > 0)
			{
				List<Student> students = adminManager.GetStudentsById(model.Childrens.StudentsId);
				parent.ParentStudents = new List<ParentStudent>();
				foreach (var s in students)
				{
					parent.ParentStudents.Add(
						new ParentStudent()
						{
							Parent = parent,
							Student = s
						});
				}
			}
			if (!adminManager.CreateParent(parent, makeHash))
			{
				this.FillStudents(ref model);
				ModelState.AddModelError("", "Někde se stala chyba. Rodič nebyl vytvořen.");
				return View(model);
			}

			return RedirectToAction("ParentList", "User");
		}

		[HttpGet]
		[Route("DetailParent/{id}")]
		public IActionResult DetailParent(int id)
		{
			ParentDetailViewModel model = new ParentDetailViewModel();
			Parent parent = adminManager.GetParentById(id);
			if (parent != null)
			{
				model.FirstName = parent.FirstName;
				model.LastName = parent.LastName;
				model.Email = parent.Email;
				model.Address = parent.Address;
				model.PhoneNumber = parent.PhoneNumber;
				model.Childrens = parent.ParentStudents.Select(x => x.Student).ToList();
			}

			return View(model);
		}

		[HttpGet]
		[Route("EditParent/{id}")]
		public IActionResult EditParent(int id)
		{
			ParentViewModel model = new ParentViewModel();
			this.FillStudents(ref model);

			Parent parent = adminManager.GetParentById(id);

			model.FirstName = parent.FirstName;
			model.LastName = parent.LastName;
			model.Email = parent.Email;
			model.Address = parent.Address;
			model.PhoneNumber = parent.PhoneNumber;

			//get students
			int[] childrensId = new int[parent.ParentStudents.Count];
			for (int i = 0; i < parent.ParentStudents.Count; i++)
			{
				childrensId[i] = parent.ParentStudents.ElementAt(i).StudentId;
			}
			model.Childrens.StudentsId = childrensId;

			return View(model);
		}

		[HttpPost]
		[Route("EditParent/{id}")]
		public IActionResult EditParent(int id, ParentViewModel model)
		{
			if(!ModelState.IsValid)
			{
				this.FillStudents(ref model);
				return View(model);
			}
			Parent parent = adminManager.GetParentById(id);
			parent.FirstName = model.FirstName;
			parent.LastName = model.LastName;
			parent.Email = model.Email;
			parent.Address = model.Address;
			parent.PhoneNumber = model.PhoneNumber;

			if (model.Childrens?.StudentsId != null && model.Childrens?.StudentsId.Length > 0)
			{
				List<Student> students = adminManager.GetStudentsById(model.Childrens.StudentsId);
				//remove
				var parentStudents = parent.ParentStudents.ToList();
				foreach (var p in parentStudents)
				{
					if (!students.Contains(p.Student))
					{
						parent.ParentStudents.Remove(p);
					}
				}
				//add
				if (students != null)
				{
					foreach (var s in students)
					{
						if (!parent.ParentStudents.Select(x => x.Student).Contains(s))
						{
							parent.ParentStudents.Add(
							new ParentStudent()
							{
								Parent = parent,
								Student = s
							});
						}
					}
				}
			}
			else if (parent.ParentStudents.Count > 0)
			{
				parent.ParentStudents = new List<ParentStudent>();
			}

			if (!adminManager.UpdateParent(parent))
			{
				this.FillStudents(ref model);
				ModelState.AddModelError("", "Někde se stala chyba. Úpravy nebyly provedeny.");
				return View(model);
			}

			return RedirectToAction("ParentList", "User");
		}

		[HttpGet]
		[Route("DeleteParent/{id}")]
		public IActionResult DeleteParent(int id)
		{
			DeleteUser(id);
			return RedirectToAction("ParentList", "User");
		}

		[HttpGet]
		[Route("EmployeeList")]
		public IActionResult EmployeeList(string searchText, int pageNumber = 1)
		{
			List<UserViewModel> model = new List<UserViewModel>();

			var employees = adminManager.GetAllUsers()
								.Where(x => x.UserRoles.Select(y => y.Role.Name).Contains("Učitel")
								|| x.UserRoles.Select(y => y.Role.Name).Contains("Třídní učitel")
								|| x.UserRoles.Select(y => y.Role.Name).Contains("Zástupce ředitele")
								|| x.UserRoles.Select(y => y.Role.Name).Contains("Ředitel")
								|| x.UserRoles.Select(y => y.Role.Name).Contains("Admin"))
								.ToList();

			employees = employees.Distinct().ToList();
			foreach (var employee in employees)
			{
				model.Add(new UserViewModel()
				{
					Id = employee.Id,
					FirstName = employee.FirstName,
					LastName = employee.LastName,
					Email = employee.Email
				});
			}

			//filter
			if (!string.IsNullOrEmpty(searchText))
			{
				model = model.Where(u => u.Email.Contains(searchText)).ToList();
			}
			ViewBag.CurrentFilter = searchText;

			return View(PaginatedList<UserViewModel>.Create(model.AsQueryable(), pageNumber, 5));
		}

		[HttpGet]
		[Route("AddEmployee")]
		public IActionResult AddEmployee()
		{
			EmployeeViewModel model = new EmployeeViewModel();
			model.HireDate = DateTime.Today;

			this.FillRoles(ref model);
			this.FillSubjects(ref model);
			this.FillClasses(ref model);

			return View(model);
		}

		[HttpPost]
		[Route("AddEmployee")]
		public IActionResult AddEmployee(EmployeeViewModel model, bool makeHash = true)
		{
			if (model.UserRoles?.RolesId == null || model.UserRoles?.RolesId.Length == 0)
			{
				this.FillRoles(ref model);
				this.FillSubjects(ref model);
				this.FillClasses(ref model);
				ModelState.AddModelError("", "Vyberte alespoň jednu roli.");
				return View(model);
			}
			if (string.IsNullOrEmpty(model.Password))
			{
				this.FillRoles(ref model);
				this.FillSubjects(ref model);
				this.FillClasses(ref model);
				ModelState.AddModelError("", "Heslo musí být vyplněno.");
				return View(model);
			}
			if (model.DateOfBirth > DateTime.Today)
			{
				this.FillRoles(ref model);
				this.FillSubjects(ref model);
				this.FillClasses(ref model);
				ModelState.AddModelError("", "Datum narození musí být v minulosti.");
				return View(model);
			}

			if (model.Gender.GenderId == 0 || model.Gender.GenderId == 1)
			{

				var roles = new List<Role>();
				foreach (var role in model.UserRoles.RolesId)
				{
					roles.Add(adminManager.GetRoleById(role));
				}

				if (roles.Select(r => r.Name).Contains("Třídní učitel"))
				{
					ClassTeacher classTeacher = new ClassTeacher();
					classTeacher.FirstName = model.FirstName;
					classTeacher.LastName = model.LastName;
					classTeacher.Email = model.Email;
					classTeacher.Password = model.Password;
					classTeacher.Address = model.Address;
					classTeacher.UserRoles = new List<UserRole>();

					classTeacher.TitleBefore = model.TitleBefore;
					classTeacher.TitleAfter = model.TitleAfter;
					classTeacher.PersonalIdentificationNumber = model.PersonalIdentificationNumber;
					classTeacher.Gender = model.Gender.GenderId == 1 ? true : false;
					classTeacher.DateOfBirth = model.DateOfBirth;
					classTeacher.BirthLocation = model.BirthLocation;
					classTeacher.Citizenship = model.Citizenship;


					if (!roles.Select(r => r.Name).Contains("Učitel"))
					{
						roles.Add(adminManager.GetAllRoles().Where(x => x.Name.Equals("Učitel")).FirstOrDefault());
					}
					//add roles
					foreach (var role in roles)
					{
						classTeacher.UserRoles.Add(new UserRole()
						{
							Role = role,
							User = classTeacher
						});
					}

					classTeacher.HireDate = model.HireDate;
					if (model.Subjects?.SubjectsId != null && model.Subjects?.SubjectsId.Length > 0)
					{
						//add subjects
						List<Subject> subjects = adminManager.GetSubjectsById(model.Subjects.SubjectsId);
						classTeacher.TeacherSubjects = new List<TeacherSubject>();
						foreach (var s in subjects)
						{
							classTeacher.TeacherSubjects.Add(new TeacherSubject()
							{
								Subject = s,
								Teacher = classTeacher
							});
						}
					}

					classTeacher.Class = adminManager.GetClassById(model.TeacherClass.ClassId);
					//if(classTeacher.Class == null)
					//{
					//	this.FillRoles(ref model);
					//	this.FillSubjects(ref model);
					//	this.FillClasses(ref model);
					//	ModelState.AddModelError("", "Vyberte třídu.");
					//	return View(model);
					//}
					if(makeHash == false)
					{
						classTeacher.Id = model.Id;
						adminManager.CreateClassTeacherWithId(classTeacher);
						return RedirectToAction("EmployeeList", "User");
					}
					if (!adminManager.CreateClassTeacher(classTeacher, makeHash))
					{
						this.FillRoles(ref model);
						this.FillSubjects(ref model);
						this.FillClasses(ref model);
						ModelState.AddModelError("", "Někde se stala chyba. Zaměstnanec nebyl vytvořen.");
						return View(model);
					}

					return RedirectToAction("EmployeeList", "User");
				}
				if (roles.Select(r => r.Name).Contains("Učitel"))
				{

					Teacher teacher = new Teacher();
					teacher.FirstName = model.FirstName;
					teacher.LastName = model.LastName;
					teacher.Email = model.Email;
					teacher.Password = model.Password;
					teacher.Address = model.Address;
					teacher.UserRoles = new List<UserRole>();

					teacher.TitleBefore = model.TitleBefore;
					teacher.TitleAfter = model.TitleAfter;
					teacher.PersonalIdentificationNumber = model.PersonalIdentificationNumber;
					teacher.Gender = model.Gender.GenderId == 1 ? true : false;
					teacher.DateOfBirth = model.DateOfBirth;
					teacher.BirthLocation = model.BirthLocation;
					teacher.Citizenship = model.Citizenship;

					//add roles
					foreach (var role in roles)
					{
						teacher.UserRoles.Add(new UserRole()
						{
							Role = role,
							User = teacher
						});
					}

					teacher.HireDate = model.HireDate;

					if (model.Subjects.SubjectsId != null && model.Subjects.SubjectsId.Length > 0)
					{
						//add subjects
						List<Subject> subjects = adminManager.GetSubjectsById(model.Subjects.SubjectsId);
						teacher.TeacherSubjects = new List<TeacherSubject>();
						foreach (var s in subjects)
						{
							teacher.TeacherSubjects.Add(new TeacherSubject()
							{
								Subject = s,
								Teacher = teacher
							});
						}
					}
					if (makeHash == false)
					{
						teacher.Id = model.Id;
						adminManager.CreateTeacherWithId(teacher);
						return RedirectToAction("EmployeeList", "User");
					}
					if (!adminManager.CreateTeacher(teacher, makeHash))
					{
						this.FillRoles(ref model);
						this.FillSubjects(ref model);
						this.FillClasses(ref model);
						ModelState.AddModelError("", "Někde se stala chyba. Zaměstnanec nebyl vytvořen.");
						return View(model);
					}

					return RedirectToAction("EmployeeList", "User");
				}
				if (roles.Select(r => r.Name).Contains("Admin") || roles.Select(r => r.Name).Contains("Ředitel")
					|| roles.Select(r => r.Name).Contains("Zástupce ředitele"))
				{
					User user = new User();
					user.FirstName = model.FirstName;
					user.LastName = model.LastName;
					user.Email = model.Email;
					user.Password = model.Password;
					user.Address = model.Address;
					user.UserRoles = new List<UserRole>();

					user.TitleBefore = model.TitleBefore;
					user.TitleAfter = model.TitleAfter;
					user.PersonalIdentificationNumber = model.PersonalIdentificationNumber;
					user.Gender = model.Gender.GenderId == 1 ? true : false;
					user.DateOfBirth = model.DateOfBirth;
					user.BirthLocation = model.BirthLocation;
					user.Citizenship = model.Citizenship;

					//add roles
					foreach (var role in roles)
					{
						user.UserRoles.Add(new UserRole()
						{
							Role = role,
							User = user
						});
					}
					if (makeHash == false)
					{
						user.Id = model.Id;
						adminManager.CreateUserWithId(user);
						return RedirectToAction("EmployeeList", "User");
					}
					if (!adminManager.CreateUser(user, makeHash))
					{
						this.FillRoles(ref model);
						this.FillSubjects(ref model);
						this.FillClasses(ref model);
						ModelState.AddModelError("", "Někde se stala chyba. Zaměstnanec nebyl vytvořen.");
						return View(model);
					}

					return RedirectToAction("EmployeeList", "User");
				}
			}
			else
			{
				this.FillRoles(ref model);
				this.FillSubjects(ref model);
				this.FillClasses(ref model);
				return View(model);
			}

			return RedirectToAction("EmployeeList", "User");
		}

		[HttpGet]
		[Route("DetailEmployee/{id}")]
		public IActionResult DetailEmployee(int id)
		{
			EmployeeDetailViewModel model = new EmployeeDetailViewModel();
			User user = adminManager.GetAllUsers().Where(u => u.Id.Equals(id)).FirstOrDefault();

			List<Role> usersRole = new List<Role>();
			foreach (var userRole in user.UserRoles)
			{
				usersRole.Add(userRole.Role);
			}

			model.FirstName = user.FirstName;
			model.LastName = user.LastName;
			model.Email = user.Email;
			model.Address = user.Address;
			model.Roles = usersRole;

			model.TitleBefore = user.TitleBefore;
			model.TitleAfter = user.TitleAfter;
			model.PersonalIdentificationNumber = user.PersonalIdentificationNumber;
			model.Gender = user.Gender;
			model.DateOfBirth = user.DateOfBirth;
			model.BirthLocation = user.BirthLocation;
			model.Citizenship = user.Citizenship;


			if (usersRole.Select(ur => ur.Name).Contains("Učitel"))
			{
				Teacher teacher = adminManager.GetTeacherById(id);
				model.HireDate = teacher.HireDate;
				model.Subjects = teacher.TeacherSubjects.Select(x => x.Subject).ToList();

				if (usersRole.Select(ur => ur.Name).Contains("Třídní učitel"))
				{
					ClassTeacher classTeacher = adminManager.GetAllClassTeachers().Where(ct => ct.Id.Equals(id)).FirstOrDefault();

					model.TeacherClass = classTeacher.Class;
				}
			}

			return View(model);
		}

		[HttpGet]
		[Route("EditEmployee/{id}")]
		public IActionResult EditEmployee(int id)
		{
			EmployeeViewModel model = new EmployeeViewModel();
			this.FillRoles(ref model);
			this.FillSubjects(ref model);
			this.FillClasses(ref model);

			model.HireDate = DateTime.Today;

			User user = adminManager.GetAllUsers().Where(u => u.Id.Equals(id)).FirstOrDefault();
			if(user == null)
			{
				return RedirectToAction("EmployeeList", "User");
			}

			List<Role> usersRole = new List<Role>();
			foreach (var userRole in user.UserRoles)
			{
				usersRole.Add(userRole.Role);
			}

			model.FirstName = user.FirstName;
			model.LastName = user.LastName;
			model.Email = user.Email;
			model.Address = user.Address;
			model.TitleBefore = user.TitleBefore;
			model.TitleAfter = user.TitleAfter;
			model.PersonalIdentificationNumber = user.PersonalIdentificationNumber;
			model.Gender.GenderId = user.Gender.Value ? 1 : 0;
			model.DateOfBirth = user.DateOfBirth;
			model.BirthLocation = user.BirthLocation;
			model.Citizenship = user.Citizenship;

			int[] userRoles = new int[usersRole.Count];
			for (int i = 0; i < usersRole.Count; i++)
			{
				userRoles[i] = usersRole[i].Id;
			}
			model.UserRoles.RolesId = userRoles;

			if (usersRole.Select(ur => ur.Name).Contains("Učitel"))
			{
				Teacher teacher = adminManager.GetTeacherById(id);
				model.HireDate = teacher.HireDate;

				int[] subjectsIds = new int[teacher.TeacherSubjects.Count];
				for (int i = 0; i < teacher.TeacherSubjects.Count; i++)
				{
					subjectsIds[i] = teacher.TeacherSubjects.ElementAt(i).SubjectId;
				}
				model.Subjects.SubjectsId = subjectsIds;

				if (usersRole.Select(ur => ur.Name).Contains("Třídní učitel"))
				{
					ClassTeacher classTeacher = adminManager.GetAllClassTeachers().Where(ct => ct.Id.Equals(id)).FirstOrDefault();

					model.TeacherClass.ClassId = classTeacher.ClassId != null ? classTeacher.ClassId.Value : 0;
				}
			}

			return View(model);
		}

		[HttpPost]
		[Route("EditEmployee/{id}")]
		public IActionResult EditEmployee(int id, EmployeeViewModel model)
		{
			if(!ModelState.IsValid)
			{
				this.FillRoles(ref model);
				this.FillSubjects(ref model);
				this.FillClasses(ref model);
				return View(model);
			}
			if (model.DateOfBirth > DateTime.Today)
			{
				this.FillRoles(ref model);
				this.FillSubjects(ref model);
				this.FillClasses(ref model);
				ModelState.AddModelError("", "Datum narození musí být v minulosti.");
				return View(model);
			}

			if (model.UserRoles?.RolesId != null && model.UserRoles?.RolesId?.Length > 0)
			{
				if (model.Gender.GenderId == 0 || model.Gender.GenderId == 1)
				{
					User dbUser = adminManager.GetAllUsers().Where(u => u.Id.Equals(id)).FirstOrDefault();
					this.FillRoles(ref model);
					this.FillSubjects(ref model);
					this.FillClasses(ref model);

					var roles = new List<Role>();
					foreach (var role in model.UserRoles.RolesId)
					{
						roles.Add(adminManager.GetRoleById(role));
					}

					if (dbUser.UserRoles.Select(r => r.Role).Select(r => r.Name).Contains("Třídní učitel"))
					{
						if (!roles.Select(r => r.Name).Contains("Třídní učitel"))
						{
							model.Id = id;
							model.Password = dbUser.Password;
							//delete
							adminManager.DeleteClassTeacher(adminManager.GetAllClassTeachers().Where(c => c.Id.Equals(dbUser.Id)).FirstOrDefault());
							//add new user - have to change type
							this.AddEmployee(model, false);

							return RedirectToAction("EmployeeList", "User");
						}

						if (!roles.Select(r => r.Name).Contains("Učitel"))
						{
							ModelState.AddModelError("", "Třídní učitel musí mít zároveň roli učitel.");
							return View(model);
						}

						ClassTeacher classTeacher = adminManager.GetAllClassTeachers().Where(ct => ct.Id.Equals(dbUser.Id)).FirstOrDefault();
						classTeacher.FirstName = model.FirstName;
						classTeacher.LastName = model.LastName;
						classTeacher.Email = model.Email;
						classTeacher.Address = model.Address;
						classTeacher.UserRoles = new List<UserRole>();
						classTeacher.HireDate = model.HireDate;

						classTeacher.TitleBefore = model.TitleBefore;
						classTeacher.TitleAfter = model.TitleAfter;
						classTeacher.PersonalIdentificationNumber = model.PersonalIdentificationNumber;
						classTeacher.Gender = model.Gender.GenderId == 1 ? true : false;
						classTeacher.DateOfBirth = model.DateOfBirth;
						classTeacher.BirthLocation = model.BirthLocation;
						classTeacher.Citizenship = model.Citizenship;

						//remove roles
						var userRoles = classTeacher.UserRoles.ToList();
						foreach (var role in userRoles)
						{
							if (!roles.Contains(role.Role))
							{
								classTeacher.UserRoles.Remove(role);
							}
						}
						//add roles
						if (roles != null)
						{
							foreach (var role in roles)
							{
								if (!classTeacher.UserRoles.Select(x => x.Role).Contains(role))
								{
									classTeacher.UserRoles.Add(new UserRole()
									{
										Role = role,
										User = classTeacher
									});
								}
							}
						}

						if (model.Subjects?.SubjectsId != null && model.Subjects?.SubjectsId.Length > 0)
						{
							List<Subject> subjects = adminManager.GetSubjectsById(model.Subjects.SubjectsId);
							//remove subjects
							var teacherSubjects = classTeacher.TeacherSubjects.ToList();
							foreach (var s in teacherSubjects)
							{
								if (!subjects.Contains(s.Subject))
								{
									classTeacher.TeacherSubjects.Remove(s);
								}
							}
							//add subjects
							if (subjects != null)
							{
								foreach (var s in subjects)
								{
									if (!classTeacher.TeacherSubjects.Select(x => x.Subject).Contains(s))
									{
										classTeacher.TeacherSubjects.Add(new TeacherSubject()
										{
											Subject = s,
											Teacher = classTeacher
										});
									}
								}
							}
						}
						else if (classTeacher.TeacherSubjects.Count > 0)
						{
							classTeacher.TeacherSubjects = new List<TeacherSubject>();
						}

						classTeacher.Class = adminManager.GetClassById(model.TeacherClass.ClassId);

						if (!adminManager.UpdateClassTeacher(classTeacher))
						{
							ModelState.AddModelError("", "Někde se stala chyba. Úpravy nebyly provedeny.");
							return View(model);
						}


						return RedirectToAction("EmployeeList", "User");
					}
					if (dbUser.UserRoles.Select(r => r.Role).Select(r => r.Name).Contains("Učitel"))
					{
						if (!roles.Select(r => r.Name).Contains("Učitel") || roles.Select(r => r.Name).Contains("Třídní učitel"))
						{
							model.Id = id;
							model.Password = dbUser.Password;
							//delete
							adminManager.DeleteTeacher(adminManager.GetTeacherById(dbUser.Id));
							//add new user - have to change type
							this.AddEmployee(model, false);

							return RedirectToAction("EmployeeList", "User");
						}

						Teacher teacher = adminManager.GetAllTeachers().Where(ct => ct.Id.Equals(dbUser.Id)).FirstOrDefault();
						teacher.FirstName = model.FirstName;
						teacher.LastName = model.LastName;
						teacher.Email = model.Email;
						teacher.Address = model.Address;
						teacher.UserRoles = new List<UserRole>();
						teacher.HireDate = model.HireDate;

						teacher.TitleBefore = model.TitleBefore;
						teacher.TitleAfter = model.TitleAfter;
						teacher.PersonalIdentificationNumber = model.PersonalIdentificationNumber;
						teacher.Gender = model.Gender.GenderId == 1 ? true : false;
						teacher.DateOfBirth = model.DateOfBirth;
						teacher.BirthLocation = model.BirthLocation;
						teacher.Citizenship = model.Citizenship;

						//remove roles
						var userRoles = teacher.UserRoles.ToList();
						foreach (var role in userRoles)
						{
							if (!roles.Contains(role.Role))
							{
								teacher.UserRoles.Remove(role);
							}
						}
						//add roles
						if (roles != null)
						{
							foreach (var role in roles)
							{
								if (!teacher.UserRoles.Select(x => x.Role).Contains(role))
								{
									teacher.UserRoles.Add(new UserRole()
									{
										Role = role,
										User = teacher
									});
								}
							}
						}

						if (model.Subjects?.SubjectsId != null && model.Subjects?.SubjectsId.Length > 0)
						{
							List<Subject> subjects = adminManager.GetSubjectsById(model.Subjects.SubjectsId);
							//remove subjects
							var teacherSubjects = teacher.TeacherSubjects.ToList();
							foreach (var s in teacherSubjects)
							{
								if (!subjects.Contains(s.Subject))
								{
									teacher.TeacherSubjects.Remove(s);
								}
							}
							//add subjects
							if (subjects != null)
							{
								foreach (var s in subjects)
								{
									if (!teacher.TeacherSubjects.Select(x => x.Subject).Contains(s))
									{
										teacher.TeacherSubjects.Add(new TeacherSubject()
										{
											Subject = s,
											Teacher = teacher
										});
									}
								}
							}
						}
						else if (teacher.TeacherSubjects.Count > 0)
						{
							teacher.TeacherSubjects = new List<TeacherSubject>();
						}

						if (!adminManager.UpdateTeacher(teacher))
						{
							ModelState.AddModelError("", "Někde se stala chyba. Úpravy nebyly provedeny.");
							return View(model);
						}

						return RedirectToAction("EmployeeList", "User");
					}
					else
					{
						//admin, principal, deputy and combinations

						if (roles.Select(r => r.Name).Contains("Učitel") || roles.Select(r => r.Name).Contains("Třídní učitel"))
						{

							model.Id = id;
							model.Password = dbUser.Password;
							//delete
							adminManager.DeleteUser(dbUser);
							//add new user - have to change type
							this.AddEmployee(model, false);

							return RedirectToAction("EmployeeList", "User");
						}


						//edit data
						dbUser.FirstName = model.FirstName;
						dbUser.LastName = model.LastName;
						dbUser.Email = model.Email;
						dbUser.Address = model.Address;
						dbUser.UserRoles = new List<UserRole>();

						dbUser.TitleBefore = model.TitleBefore;
						dbUser.TitleAfter = model.TitleAfter;
						dbUser.PersonalIdentificationNumber = model.PersonalIdentificationNumber;
						dbUser.Gender = model.Gender.GenderId == 1 ? true : false;
						dbUser.DateOfBirth = model.DateOfBirth;
						dbUser.BirthLocation = model.BirthLocation;
						dbUser.Citizenship = model.Citizenship;

						//remove roles
						var userRoles = dbUser.UserRoles.ToList();
						foreach (var role in userRoles)
						{
							if (!roles.Contains(role.Role))
							{
								dbUser.UserRoles.Remove(role);
							}
						}
						//add roles
						if (roles != null)
						{
							foreach (var role in roles)
							{
								if (!dbUser.UserRoles.Select(x => x.Role).Contains(role))
								{
									dbUser.UserRoles.Add(new UserRole()
									{
										Role = role,
										User = dbUser
									});
								}
							}
						}

						if (!adminManager.UpdateUser(dbUser))
						{
							ModelState.AddModelError("", "Někde se stala chyba. Úpravy nebyly provedeny.");
							return View(model);
						}

						return RedirectToAction("EmployeeList", "User");

					}

				}
				else
				{
					this.FillRoles(ref model);
					this.FillSubjects(ref model);
					this.FillClasses(ref model);
					return View(model);
				}
			}
			else
			{
				this.FillRoles(ref model);
				this.FillSubjects(ref model);
				this.FillClasses(ref model);
				ModelState.AddModelError("", "Není vybrána žádná role.");
				return View(model);
			}

			return RedirectToAction("EmployeeList", "User");
		}

		[HttpGet]
		[Route("DeleteEmployee/{id}")]
		public IActionResult DeleteEmployee(int id)
		{
			DeleteUser(id);
			return RedirectToAction("EmployeeList", "User");
		}

		private bool DeleteUser(int id)
		{
			User user = adminManager.GetAllUsers().Where(x => x.Id.Equals(id)).FirstOrDefault();
			if (user != null)
			{
				if (user is Teacher)
				{
					var t = adminManager.GetTeacherById(id);
					adminManager.DeleteTeacher(t);

					return true;
				}
				if (user is ClassTeacher)
				{
					var t = adminManager.GetAllClassTeachers().Where(x => x.Id.Equals(id)).FirstOrDefault();
					adminManager.DeleteClassTeacher(t);

					return true;
				}
				if (user is Parent)
				{
					var t = adminManager.GetParentById(id);
					adminManager.DeleteParent(t);

					return true;
				}
				if (user is Student)
				{
					var t = adminManager.GetStudentById(id);
					adminManager.DeleteStudent(t);

					return true;
				}

				adminManager.DeleteUser(user);

			}
			return false;
		}

		private void FillRoles(ref EmployeeViewModel model)
		{
			var roles = adminManager.GetAllRoles();

			foreach (var role in roles)
			{
				if (role.Name.Equals("Rodič") || role.Name.Equals("Žák"))
					continue;

				model.UserRoles.UserRoles.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
				{
					Text = role.Name,
					Value = role.Id.ToString()
				});
			}
		}

		private void FillSubjects(ref EmployeeViewModel model)
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

		private void FillParents(ref StudentViewModel model)
		{
			var parents = adminManager.GetAllParents();

			foreach (var parent in parents)
			{
				model.Parents.Parents.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
				{
					Text = parent.LastName + " " + parent.FirstName + " (" + parent.Email + ")",
					Value = parent.Id.ToString()
				});
			}
		}

		private void FillStudents(ref ParentViewModel model)
		{
			var students = adminManager.GetAllStudents();

			foreach (var student in students)
			{
				model.Childrens.Students.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
				{
					Text = student.LastName + " " + student.FirstName + " (" + student.Email + ")",
					Value = student.Id.ToString()
				});
			}
		}

		private void FillClasses(ref EmployeeViewModel model)
		{
			var classes = adminManager.GetAllClasses();

			foreach (var c in classes)
			{
				model.TeacherClass.Classes.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
				{
					Text = c.Name,
					Value = c.Id.ToString()
				});
			}
		}
	}
}
