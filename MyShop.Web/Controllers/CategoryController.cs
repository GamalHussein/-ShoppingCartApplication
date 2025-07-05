using Microsoft.AspNetCore.Mvc;
using MyShop.DataAccess.Data;
using MyShop.Entities.Models;

namespace MyShop.Web.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ApplicationDbContext _context;

		public CategoryController(ApplicationDbContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			var categories = _context.Categories.ToList();
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
				_context.Categories.Add(category);
				_context.SaveChanges();
				return RedirectToAction(nameof(Index));
			}
			return View(category);
		}

		[HttpGet]
		public IActionResult Edit(int id)
		{
			var existingCategory = _context.Categories.Find(id);
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
				_context.Categories.Update(category);
				_context.SaveChanges();
				return RedirectToAction(nameof(Index));
			}
			return View(category);
		}

		[HttpGet]
		public IActionResult Delete(int id)
		{
			var existingCategory = _context.Categories.Find(id);
			if (existingCategory == null)
			{
				return NotFound();
			}
			return View(existingCategory);
		}
		[HttpPost]
		public IActionResult DeleteConfirmed(int id)
		{
			var existingCategory = _context.Categories.Find(id);
			if (existingCategory != null)
			{
				_context.Categories.Remove(existingCategory);
				_context.SaveChanges();
				return RedirectToAction(nameof(Index));
			}
			return NotFound();
		}
	}
}
