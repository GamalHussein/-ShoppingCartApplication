using MyShop.DataAccess.Data;
using MyShop.Entities.Models;
using MyShop.Entities.Repositores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.Implementation
{
	public class CategoryRepository:BaseRepository<Category>, ICategoryRepository
	{
		private readonly ApplicationDbContext _context;
		public CategoryRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}
		public void Update(Category category)
		{
			var objFromDb = _context.Categories.FirstOrDefault(s => s.Id == category.Id);
			if (objFromDb != null)
			{
				objFromDb.Name = category.Name;
				objFromDb.Description = category.Description;
				objFromDb.CreatedDate=DateTime.Now;
				_context.SaveChanges();
			}
		}
	}
	
}
