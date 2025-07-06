using Microsoft.EntityFrameworkCore;
using MyShop.DataAccess.Data;
using MyShop.Entities.Models;
using MyShop.Entities.Repositores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.Implementation
{
	public class ProductRepository : BaseRepository<Product>, IProductRepository
	{
		private readonly ApplicationDbContext _context;
		public ProductRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}
		public void Update(Product product)
		{
			var objFromDb = _context.Products.FirstOrDefault(s => s.Id == product.Id);
			if (objFromDb != null)
			{
				objFromDb.Name = product.Name;
				objFromDb.Description = product.Description;
				objFromDb.Price = product.Price;
				objFromDb.ImageUrl = product.ImageUrl;
				objFromDb.CategoryId = product.CategoryId;
			}
			
		}
	}
}