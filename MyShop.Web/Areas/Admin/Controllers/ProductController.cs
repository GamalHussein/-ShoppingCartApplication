using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyShop.DataAccess.Data;
using MyShop.Entities.Models;
using MyShop.Entities.Repositores;
using MyShop.Entities.ViewModels;

namespace MyShop.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
		{
			this.unitOfWork = unitOfWork;
			_webHostEnvironment = webHostEnvironment;
		}
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult GetProducts()
		{
			List<Product> products = unitOfWork.Product.GetAll(IncludeWord: "Category").ToList();
			return Json(new { data = products });
		}

		[HttpGet]
		public IActionResult Create()
		{
			ProductVM productVM = new ProductVM()
			{
				product = new Product(),
				Categories = unitOfWork.Category.GetAll().Select(c => new SelectListItem
				{
					Text = c.Name,
					Value = c.Id.ToString()
				})
			};
			return View(productVM);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(ProductVM productvm, IFormFile file)
		{

			if (ModelState.IsValid)
			{
				var RootPath = _webHostEnvironment.WebRootPath;
				if (file != null)
				{
					var fileName = Guid.NewGuid().ToString();
					var uploadPath = Path.Combine(RootPath, @"images\Product");
					var extension = Path.GetExtension(file.FileName);

					using (var fileStream = new FileStream(Path.Combine(uploadPath, fileName + extension), FileMode.Create))
					{
						file.CopyTo(fileStream);
					}
					productvm.product.ImageUrl = @"Images\Product\" + fileName + extension;
				}
				
				unitOfWork.Product.Add(productvm.product);
				unitOfWork.Save();
				TempData["Create"] = "Product created successfully!";
				return RedirectToAction(nameof(Create));
			}
			return View(productvm);
		}

		[HttpGet]
		public IActionResult Edit(int id)
		{
			var existingProducts = unitOfWork.Product.GetById(id);
			if (existingProducts == null)
			{
				return NotFound();
			}
			ProductVM productVM = new ProductVM()
			{
				product = unitOfWork.Product.GetById(id),
				Categories = unitOfWork.Category.GetAll().Select(c => new SelectListItem
				{
					Text = c.Name,
					Value = c.Id.ToString()
				})
			};
			
			return View(productVM);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(ProductVM productvm,IFormFile? file)
		{
			if (ModelState.IsValid)
			{
				var RootPath = _webHostEnvironment.WebRootPath;
				if (file != null)
				{
					var fileName = Guid.NewGuid().ToString();
					var uploadPath = Path.Combine(RootPath, @"images\Product");
					var extension = Path.GetExtension(file.FileName);
					if(productvm.product.ImageUrl != null)
					{
						var oldImagePath = Path.Combine(RootPath, productvm.product.ImageUrl.TrimStart('\\'));
						if (System.IO.File.Exists(oldImagePath))
						{
							System.IO.File.Delete(oldImagePath);
						}
					}

					using (var fileStream = new FileStream(Path.Combine(uploadPath, fileName + extension), FileMode.Create))
					{
						file.CopyTo(fileStream);
					}
					productvm.product.ImageUrl = @"Images\Product" + fileName + extension;
				}
				unitOfWork.Product.Update(productvm.product);
				unitOfWork.Save();
				TempData["Edit"] = "Product updated successfully!";
				return RedirectToAction(nameof(Index));
			}
			return View(productvm.product);
		}

		
		[HttpDelete]
		public IActionResult DeleteProduct(int id)
		{
			try
			{
				Product product = unitOfWork.Product.GetFirstOrDefult(C => C.Id == id);

				unitOfWork.Product.Remove(product);
				unitOfWork.Save();

				var oldImage = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('\\'));
				if (System.IO.File.Exists(oldImage))
				{
					System.IO.File.Delete(oldImage);
				}
				return Json(new { success = true, message = "Product has been deleted" });

			}
			catch
			{
				return Json(new { success = false, message = "Error while Deleteing product" });
			}
		}
	}
}

