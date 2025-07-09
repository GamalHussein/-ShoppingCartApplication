using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyShop.Entities.Models;
using MyShop.Entities.Repositores;
using System.Security.Claims;

namespace MyShop.Web.Areas.Customer.Controllers
{
	[Area("Customer")]
	public class HomeController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		public HomeController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public IActionResult Index()
		{
			var products = _unitOfWork.Product.GetAll();
			return View(products);
		}
		public IActionResult Details(int ProductId)
		{
			var product = _unitOfWork.Product.GetFirstOrDefult(
				p => p.Id == ProductId,
				IncludeWord: "Category"
			);

			if (product == null)
			{
				return NotFound(); // أو return View("NotFound");
			}

			ShopingCart Item = new ShopingCart()
			{
				ProductId = ProductId,
				product = product,
				Count = 1
			};

			return View(Item);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public IActionResult Details(ShopingCart shopingCart)
		{
			var user = (ClaimsIdentity)User.Identity;
			var userId = user.FindFirst(ClaimTypes.NameIdentifier).Value;
			shopingCart.ApplicationUserId = userId;

			ShopingCart existingCart = _unitOfWork.ShoppingCart.GetFirstOrDefult(
				s => s.ApplicationUserId == userId && s.ProductId == shopingCart.ProductId
			);
			if (existingCart == null) 
			{ 
					_unitOfWork.ShoppingCart.Add(shopingCart);
			}
			else
			{
				
				existingCart.Count += shopingCart.Count;
				_unitOfWork.ShoppingCart.IncreaseCount(existingCart,shopingCart.Count);
			}
			_unitOfWork.Save();
			return RedirectToAction("Index");
		}

	}
}
