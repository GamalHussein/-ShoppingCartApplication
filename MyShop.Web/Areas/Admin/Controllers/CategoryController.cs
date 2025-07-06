using Microsoft.AspNetCore.Mvc;
using MyShop.DataAccess.Data;
using MyShop.Entities.Models;
using MyShop.Entities.Repositores;

namespace MyShop.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CategoryController : Controller
	{
		private readonly IUnitOfWork unitOfWork;

		public CategoryController(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}
		public IActionResult Index()
		{
			var categories = unitOfWork.Category.GetAll()
				.OrderBy(c => c.Name)
				.ToList();
			return View(categories);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Category category)
		{
			if (ModelState.IsValid)
			{
				unitOfWork.Category.Add(category);
				unitOfWork.Save();
				TempData["Create"] = "Category created successfully!";
				return RedirectToAction(nameof(Index));
			}
			return View(category);
		}

		[HttpGet]
		public IActionResult Edit(int id)
		{
			var existingCategory = unitOfWork.Category.GetById(id);
			if (existingCategory == null)
			{
				return NotFound();
			}
			return View(existingCategory);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Category category)
		{
			if (ModelState.IsValid)
			{
				unitOfWork.Category.Update(category);
				unitOfWork.Save();
				TempData["Edit"] = "Category updated successfully!";
				return RedirectToAction(nameof(Index));
			}
			return View(category);
		}

		[HttpGet]
		public IActionResult Delete(int id)
		{
			var existingCategory = unitOfWork.Category.GetById(id);
			if (existingCategory == null)
			{
				return NotFound();
			}
			return View(existingCategory);
		}
		[HttpPost]
		public IActionResult DeleteConfirmed(int id)
		{
			var existingCategory = unitOfWork.Category.GetById(id);

			if (existingCategory != null)
			{
				unitOfWork.Category.Remove(existingCategory);
				unitOfWork.Save();
				TempData["Delete"] = "Category deleted successfully!";
				return RedirectToAction(nameof(Index));
			}
			return NotFound();
		}
	}
}
