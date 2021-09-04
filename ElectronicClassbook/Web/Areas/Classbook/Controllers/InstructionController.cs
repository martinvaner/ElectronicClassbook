using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Classbook.Interfaces;
using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Classbook.Models;

namespace Web.Areas.Classbook.Controllers
{
	[Authorize(Roles = "Admin, Ředitel, Zástupce ředitele, Třídní učitel, Učitel")]
	[Area("Classbook")]
	[Route("instruction")]
	public class InstructionController : Controller
	{

		private readonly IClassbookManager classbookManager;
		public InstructionController(IClassbookManager classbookManager)
		{
			this.classbookManager = classbookManager;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id">classbook id</param>
		/// <param name="date"></param>
		/// <returns></returns>
		public IActionResult Index(int id, DateTime? date, int pageNumber = 1)
		{
			List<InstructionListViewModel> model = new List<InstructionListViewModel>();

			var instructions = classbookManager.GetInstructionsByClassbookId(id);
			if (date != null)
				instructions = instructions.Where(x => x.Record.Created.Equals(date));
			else
				instructions = instructions.OrderByDescending(x => x.Record.Created);

			foreach (var instruction in instructions)
			{
				model.Add(new InstructionListViewModel()
				{
					Id = instruction.Id,
					Date = instruction.Record.Created.Date,
					Author = instruction.Author,
					Title = instruction.Title
				});
			}

			ViewBag.ClassbookId = id;
			ViewBag.ClassName = classbookManager.GetClassName(id);
			ViewBag.Date = date != null ? date.Value.ToString("yyyy-MM-dd") : null;
			return View(PaginatedList<InstructionListViewModel>.Create(model.AsQueryable(), pageNumber, 6));

		}

		[HttpGet]
		[Route("DetailInstruction/{classbookId}/{instructionId}/{date}")]
		[Route("DetailInstruction/{classbookId}/{instructionId}")]
		public IActionResult DetailInstruction(int classbookId, int instructionId, DateTime? date)
		{
			InstructionDetailViewModel model = new InstructionDetailViewModel();

			var instruction = classbookManager.GetInstructionById(instructionId);
			if(instruction != null)
			{
				model.Author = instruction.Author;
				model.Date = instruction.Record.Created;
				model.Text = instruction.Text;
				model.Title = instruction.Title;
			}

			ViewBag.ClassbookId = classbookId;
			ViewBag.ClassName = classbookManager.GetClassName(classbookId);
			ViewBag.Date = date != null ? date.Value.ToString("yyyy-MM-dd") : null;
			return View(model);
		}

		[HttpGet]
		[Route("DeleteInstruction/{classbookId}/{instructionId}/{date}")]
		[Route("DeleteInstruction/{classbookId}/{instructionId}")]
		public IActionResult DeleteInstruction(int classbookId, int instructionId, DateTime? date)
		{
			var instruction = classbookManager.GetInstructionById(instructionId);
			if (instruction != null)
			{
				classbookManager.DeleteInstruction(instruction);
			}

			return RedirectToAction("Index", new { id = classbookId, date = date });
		}
	}
}
