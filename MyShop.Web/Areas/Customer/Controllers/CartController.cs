using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using MyShop.Entities.Repositores;
using MyShop.Entities.ViewModels;
using System.Security.Claims;

namespace MyShop.Web.Areas.Customer.Controllers
{
	[Area("Customer")]
	[Authorize]
	public class CartController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private  ShoppingCartVM shoppingCartVM;
		public CartController(IUnitOfWork unitOfWork)
		{
			this._unitOfWork = unitOfWork;
		}



		public IActionResult Index()
		{
			var user = (ClaimsIdentity)User.Identity;
			var userId = user.FindFirst(ClaimTypes.NameIdentifier).Value;

			shoppingCartVM = new ShoppingCartVM()
			{
				Carts = _unitOfWork.ShoppingCart.GetAll(x => x.ApplicationUserId == userId, IncludeWord: "product"),

				
		    };

			foreach (var item in shoppingCartVM.Carts)
			{

				shoppingCartVM.TotalPrice += (item.Count * item.product.Price);
			}


			return View(shoppingCartVM);
		}

		public IActionResult Blus(int cartId)
		{
			var cart=_unitOfWork.ShoppingCart.GetFirstOrDefult(x => x.Id == cartId);
			_unitOfWork.ShoppingCart.IncreaseCount(cart, 1);
			_unitOfWork.Save();
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Minus(int cartId)
		{
			var cart = _unitOfWork.ShoppingCart.GetFirstOrDefult(x => x.Id == cartId);

			if (cart.Count <= 1)
			{
				_unitOfWork.ShoppingCart.Remove(cart);
				_unitOfWork.Save();
				return RedirectToAction(nameof(Index),"Home");
			}
			else
			{
				_unitOfWork.ShoppingCart.DecreaseCount(cart, 1);
			}	
			_unitOfWork.Save();
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Remove(int cartId)
		{
			var cart = _unitOfWork.ShoppingCart.GetFirstOrDefult(x => x.Id == cartId);
			_unitOfWork.ShoppingCart.Remove(cart);
			_unitOfWork.Save();
			return RedirectToAction(nameof(Index));
		}

	}
}
