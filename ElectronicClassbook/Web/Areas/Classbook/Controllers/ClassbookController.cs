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
using Web.Areas.Classbook.Models.Enums;

namespace Web.Areas.Classbook.Controllers
{
	[Authorize(Roles = "Admin, Ředitel, Zástupce ředitele, Třídní učitel, Učitel")]
	[Area("Classbook")]
	[Route("classbook")]
	public class ClassbookController : Controller
	{

		private readonly IClassbookManager classbookManager;
		public ClassbookController(IClassbookManager classbookManager)
		{
			this.classbookManager = classbookManager;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id">Classbook Id</param>
		/// <param name="date"></param>
		/// <returns></returns>
		public IActionResult Index(int id, DateTime? date)
		{
			List<RecordListViewModel> model = new List<RecordListViewModel>();

			if (date == null)
				date = DateTime.Today;

			//list records by classbook Id and chosen date
			List<Record> records = classbookManager.GetRecordsByDate(id, date.Value).ToList();

			foreach (var record in records)
			{
				model.Add(new RecordListViewModel()
				{
					Id = record.Id,
					Subject = record.Subject,
					Topic = record.Topic,
					CreatedBy = record.CreatedBy,
					Classbook = record.Classbook,
					SerialNumber = record.SerialNumber,
					Created = record.Created,
					IsSubstituted = record.IsSubstituted
				});

			}

			ViewBag.Date = date.Value.ToString("yyyy-MM-dd");
			ViewBag.ClassbookId = id;
			ViewBag.ClassName = classbookManager.GetClassName(id);
			return View(model);
		}

		[Authorize(Roles = "Učitel")]
		[HttpGet]
		[Route("AddRecord/{classbookId}/{date}")]
		public IActionResult AddRecord(int classbookId, DateTime date)
		{
			RecordAddViewModel model = new RecordAddViewModel();
			this.FillSubjects(ref model);

			List<Record> records = classbookManager.GetRecordsByDate(classbookId, date).ToList();
			if (records != null && records.Count > 0)
				model.SerialNumber = records.OrderBy(x => x.SerialNumber).Last().SerialNumber + 1;
			else
				model.SerialNumber = 1;
			model.Created = date;
			model.CreatedBy = classbookManager.GetTeacherByEmail(User.Identity.Name);
			model.Classbook = classbookManager.GetAllClassbooks().Where(x => x.Id.Equals(classbookId)).FirstOrDefault();

			ViewBag.Date = date.ToString("yyyy-MM-dd");
			ViewBag.ClassbookId = classbookId;
			ViewBag.ClassName = classbookManager.GetClassName(classbookId);
			return View(model);
		}

		[HttpPost]
		[Route("AddRecord/{id}/{date}")]
		public IActionResult AddRecord(int id, DateTime date, RecordAddViewModel model)
		{
			if(model.Subject.SubjectId == -1)
			{
				this.FillSubjects(ref model);
				ModelState.AddModelError("", "Vyberte předmět.");
				return View(model);
			}
			Record record = new Record();
			record.Classbook = classbookManager.GetAllClassbooks().Where(x => x.Id.Equals(model.Classbook.Id)).FirstOrDefault();
			record.Created = model.Created;
			record.CreatedBy = classbookManager.GetTeacherByEmail(model.CreatedBy.Email);
			record.SerialNumber = model.SerialNumber;
			record.Subject = classbookManager.GetAllSubjects().Where(x => x.Id.Equals(model.Subject.SubjectId)).FirstOrDefault();
			record.Topic = model.Topic;

			record.Attendances = new List<DataAccess.EntityModel.Attendance>();

			//get students in the class
			var students = record.Classbook.Class.Students;
			//create attendance for every student in clas
			foreach (var student in students)
			{
				record.Attendances.Add(new DataAccess.EntityModel.Attendance()
				{
					Student = student,
					Record = record,
					Present = true
				});
			}

			if (!classbookManager.CreateRecord(record))
			{
				this.FillSubjects(ref model);

				ViewBag.Date = date.ToString("yyyy-MM-dd");
				ViewBag.ClassbookId = id;
				ViewBag.ClassName = classbookManager.GetClassName(id);
				return View(model);
			}

			return RedirectToAction("Index", new { id = record.Classbook.Id, date = record.Created });

			//return RedirectToAction("Index", new { id, date });
		}

		[HttpGet]
		[Route("EditRecord/{classbookId}/{date}/{recordId}")]
		public IActionResult EditRecord(int classbookId, DateTime date, int recordId)
		{
			RecordAddViewModel model = new RecordAddViewModel();
			this.FillSubjects(ref model);
			var record = classbookManager.GetRecordById(recordId);

			if(record != null)
			{
				model.SerialNumber = record.SerialNumber;
				model.Created = record.Created;
				model.CreatedBy = record.CreatedBy;
				model.Classbook = record.Classbook;
				model.Topic = record.Topic;
				model.Subject.SubjectId = record.SubjectId;

				ViewBag.Date = date.ToString("yyyy-MM-dd");
				ViewBag.ClassbookId = classbookId;
				ViewBag.ClassName = classbookManager.GetClassName(classbookId);
				return View(model);
			}

			return RedirectToAction("Index", new { id = classbookId, date = date });
		}
		[HttpPost]
		[Route("EditRecord/{classbookId}/{date}/{recordId}")]
		public IActionResult EditRecord(int classbookId, DateTime date, int recordId, RecordAddViewModel model)
		{
			if (model.Subject.SubjectId == -1)
			{
				this.FillSubjects(ref model);
				ModelState.AddModelError("", "Vyberte předmět.");
				return View(model);
			}
			var record = classbookManager.GetRecordById(recordId);
			if(record == null)
			{
				return RedirectToAction("Index", new { id = classbookId, date = date });
			}

			record.Created = model.Created;
			record.SerialNumber = model.SerialNumber;
			record.Subject = classbookManager.GetAllSubjects().Where(x => x.Id.Equals(model.Subject.SubjectId)).FirstOrDefault();
			record.Topic = model.Topic;


			if (!classbookManager.UpdateRecord(record))
			{
				this.FillSubjects(ref model);

				ViewBag.Date = date.ToString("yyyy-MM-dd");
				ViewBag.ClassbookId = classbookId;
				ViewBag.ClassName = classbookManager.GetClassName(classbookId);
				return View(model);
			}
			return RedirectToAction("Index", new { id = classbookId, date = date });
		}

		[Authorize(Roles = "Učitel")]
		[HttpGet]
		[Route("AddAttendance/{classbookId}/{date}/{recordId}")]
		public IActionResult AddAttendance(int classbookId, DateTime date, int recordId)
		{
			//show all students a dropdowns

			AttendanceViewModel model = new AttendanceViewModel();

			var record = classbookManager.GetRecordById(recordId);

			foreach (var attendance in record.Attendances)
			{
				model.StudentAttendances.Add(new Models.Submodels.StudentAttendanceSubModel()
				{
					Student = attendance.Student,
					Attendace = new Models.Submodels.AttendanceSubModel()
					{
						AttendanceId = attendance.Present ? (int)AttendanceEnum.Present : (attendance.LateArrival ? (int)AttendanceEnum.LateArrival : (int)AttendanceEnum.Absent)
					},

					AbsenceInterval = attendance.AbsenceInterval
				});

			}

			foreach (var a in model.StudentAttendances)
			{
				a.Attendace.Attendances.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
				{
					Text = "Přítomen",
					Value = ((int)AttendanceEnum.Present).ToString()
				});
				a.Attendace.Attendances.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
				{
					Text = "Nepřítomen",
					Value = ((int)AttendanceEnum.Absent).ToString()
				});
				a.Attendace.Attendances.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
				{
					Text = "Pozdní příchod",
					Value = ((int)AttendanceEnum.LateArrival).ToString()
				});
			}


			ViewBag.ClassbookId = classbookId;
			ViewBag.ClassName = classbookManager.GetClassName(classbookId);
			ViewBag.Date = date.ToString("yyyy-MM-dd");
			return View(model);
		}

		[HttpPost]
		[Route("AddAttendance/{classbookId}/{date}/{recordId}")]
		public IActionResult AddAttendance(int classbookId, DateTime date, int recordId, AttendanceViewModel model)
		{
			foreach (var sa in model.StudentAttendances)
			{

				Student student = classbookManager.GetStudentById(sa.Student.Id);

				DataAccess.EntityModel.Attendance studentAttendance = classbookManager.GetAttendance(recordId, student.Id);

				if ((AttendanceEnum)sa.Attendace.AttendanceId == AttendanceEnum.Absent)
				{
					studentAttendance.Present = false;
					studentAttendance.LateArrival = false;
					studentAttendance.AbsenceInterval = 0;
				}
				else if ((AttendanceEnum)sa.Attendace.AttendanceId == AttendanceEnum.LateArrival)
				{
					if(sa.AbsenceInterval >= 60)
					{
						studentAttendance.Present = false;
						studentAttendance.LateArrival = false;
						studentAttendance.AbsenceInterval = 0;
					}
					else
					{
						studentAttendance.Present = false;
						studentAttendance.LateArrival = true;
						studentAttendance.AbsenceInterval = sa.AbsenceInterval;
					}
				}
				else if ((AttendanceEnum)sa.Attendace.AttendanceId == AttendanceEnum.Present)
				{
					studentAttendance.Present = true;
					studentAttendance.LateArrival = false;
					studentAttendance.AbsenceInterval = 0;
				}

				classbookManager.UpdateAttendance(studentAttendance);
			}

			return RedirectToAction("Index", new { id = classbookId, date = date });
		}

		[Authorize(Roles = "Ředitel, Zástupce ředitele")]
		[HttpGet]
		[Route("AddInspection/{classbookId}/{date}/{recordId}")]
		public IActionResult AddInspection(int classbookId, DateTime date, int recordId)
		{
			InspectionAddViewModel model = new InspectionAddViewModel();

			ViewBag.Date = date.ToString("yyyy-MM-dd");
			ViewBag.ClassbookId = classbookId;
			ViewBag.ClassName = classbookManager.GetClassName(classbookId);
			return View(model);
		}

		[Authorize(Roles = "Ředitel, Zástupce ředitele")]
		[HttpPost]
		[Route("AddInspection/{classbookId}/{date}/{recordId}")]
		public IActionResult AddInspection(int classbookId, DateTime date, int recordId, InspectionAddViewModel model)
		{
			Inspection inspection = new Inspection();
			inspection.Auditor = model.Auditor;
			inspection.Text = model.Text;
			inspection.Record = classbookManager.GetRecordById(recordId);

			if (!classbookManager.CreateInspection(inspection))
			{
				ModelState.AddModelError("", "Někde se stala chyba. Záznam o hospitaci nebyl vytvořen.");

				ViewBag.Date = date.ToString("yyyy-MM-dd");
				ViewBag.ClassbookId = classbookId;
				ViewBag.ClassName = classbookManager.GetClassName(classbookId);
				return View(model);
			}

			return RedirectToAction("Index", new { id = classbookId, date = date });
		}

		[Authorize(Roles = "Učitel")]
		[HttpGet]
		[Route("AddInstruction/{classbookId}/{date}/{recordId}")]
		public IActionResult AddInstruction(int classbookId, DateTime date, int recordId)
		{
			InstructionAddViewModel model = new InstructionAddViewModel();
			model.Author = classbookManager.GetTeacherByEmail(User.Identity.Name);

			ViewBag.Date = date.ToString("yyyy-MM-dd");
			ViewBag.ClassbookId = classbookId;
			ViewBag.ClassName = classbookManager.GetClassName(classbookId);
			return View(model);
		}

		[Authorize(Roles = "Učitel")]
		[HttpPost]
		[Route("AddInstruction/{classbookId}/{date}/{recordId}")]
		public IActionResult AddInstruction(int classbookId, DateTime date, int recordId, InstructionAddViewModel model)
		{
			Instruction instruction = new Instruction();
			instruction.Title = model.Title;
			instruction.Text = model.Text;
			instruction.Record = classbookManager.GetRecordById(recordId);
			instruction.Author = classbookManager.GetTeacherByEmail(model.Author.Email);

			if (!classbookManager.CreateInstruction(instruction))
			{
				ModelState.AddModelError("", "Někde se stala chyba. Záznam o poučení nebyl vytvořen.");
				ViewBag.Date = date.ToString("yyyy-MM-dd");
				ViewBag.ClassbookId = classbookId;
				ViewBag.ClassName = classbookManager.GetClassName(classbookId);
				return View(model);
			}

			return RedirectToAction("Index", new { id = classbookId, date = date });
		}

		[Authorize(Roles = "Učitel")]
		[HttpGet]
		[Route("AddHomework/{classbookId}/{date}/{recordId}")]
		public IActionResult AddHomework(int classbookId, DateTime date, int recordId)
		{
			HomeworkAddViewModel model = new HomeworkAddViewModel();
			model.Deadline = DateTime.Today;

			ViewBag.Date = date.ToString("yyyy-MM-dd");
			ViewBag.ClassbookId = classbookId;
			ViewBag.ClassName = classbookManager.GetClassName(classbookId);
			return View(model);
		}

		[Authorize(Roles = "Učitel")]
		[HttpPost]
		[Route("AddHomework/{classbookId}/{date}/{recordId}")]
		public IActionResult AddHomework(int classbookId, DateTime date, int recordId, HomeworkAddViewModel model)
		{
			if (model.Deadline == null)
			{
				ViewBag.Date = date.ToString("yyyy-MM-dd");
				ViewBag.ClassbookId = classbookId;
				ViewBag.ClassName = classbookManager.GetClassName(classbookId);
				return View(model);
			}
			Homework homework = new Homework();
			homework.Title = model.Title;
			homework.Text = model.Text;
			homework.Deadline = model.Deadline.Value;
			homework.Record = classbookManager.GetRecordById(recordId);

			if (!classbookManager.CreateHomework(homework))
			{
				ModelState.AddModelError("", "Někde se stala chyba. Domácí úkol nebyl vytvořen.");
				ViewBag.Date = date.ToString("yyyy-MM-dd");
				ViewBag.ClassbookId = classbookId;
				ViewBag.ClassName = classbookManager.GetClassName(classbookId);
				return View(model);
			}

			return RedirectToAction("Index", new { id = classbookId, date = date });
		}


		[HttpGet]
		[Route("ShowHomeworks/{id}")]
		public IActionResult ShowHomeworks(int id, int pageNumber=1)
		{
			List<Homework> homeworks = classbookManager.GetHomeworksByClassbookId(id).Where(x => x.Deadline > DateTime.Today).ToList();

			HomeworkListViewModel model = new HomeworkListViewModel();
			//model.Homeworks = homeworks;
			model.Homeworks = PaginatedList<Homework>.Create(homeworks.AsQueryable(), pageNumber, 6);

			ViewBag.ClassbookId = id;
			ViewBag.ClassName = classbookManager.GetClassName(id);
			return View(model);
			//return View(PaginatedList<Homework>.Create(model.AsQueryable(), pageNumber, 6));

		}

		[HttpGet]
		[Route("DetailHomework/{classbookId}/{homeworkId}")]
		public IActionResult DetailHomework(int classbookId, int homeworkId)
		{

			HomeworkDetailViewModel model = new HomeworkDetailViewModel();
			Homework homework = classbookManager.GetHomeworkById(homeworkId);

			model.Homework = homework;

			ViewBag.ClassbookId = classbookId;
			ViewBag.ClassName = classbookManager.GetClassName(classbookId);
			return View(model);
		}

		[HttpGet]
		[Route("UpdateSubstitution/{classbookId}/{date}/{recordId}")]
		public IActionResult UpdateSubstitution(int classbookId, DateTime date, int recordId)
		{
			var record = classbookManager.GetRecordById(recordId);
			if (record != null)
			{
				record.IsSubstituted = !record.IsSubstituted;

				classbookManager.UpdateRecord(record);
			}

			return RedirectToAction("Index", new { id = classbookId, date = date });
		}



		private void FillSubjects(ref RecordAddViewModel model)
		{
			var subjects = classbookManager.GetAllSubjects();

			foreach (var s in subjects)
			{
				model.Subject.Subjects.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
				{
					Text = s.Code,
					Value = s.Id.ToString()
				});
			}
		}

	}
}
