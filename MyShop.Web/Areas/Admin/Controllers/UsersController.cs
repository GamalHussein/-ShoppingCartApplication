using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyShop.DataAccess.Data;
using System.Security.Claims;
using Utilties;

namespace MyShop.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles ="Admin")]
	public class UsersController : Controller
	{
		private readonly ApplicationDbContext _context;
		public UsersController(ApplicationDbContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			var userIdValue = userId.Value;
			//var users = _context.ApplicationUsers.Where(u => u.Id != userIdValue).ToList;


			return View(_context.ApplicationUsers.Where(u => u.Id != userIdValue).ToList());
		}

		public IActionResult LockUnLock(string? userId)
		{
			var user = _context.ApplicationUsers.FirstOrDefault(u => u.Id == userId);
			if (user == null)
				return NotFound();

			if(user.LockoutEnd!=null || user.LockoutEnd<DateTime.UtcNow)
				user.LockoutEnd = DateTime.UtcNow.AddYears(1);
			else
				user.LockoutEnd= DateTime.UtcNow;

			return RedirectToAction("Index","Users","Admin");
		}
	}
}
