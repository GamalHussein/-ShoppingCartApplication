using MyShop.DataAccess.Data;
using MyShop.Entities.Repositores;

namespace MyShop.DataAccess.Implementation
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _context;
		public IProductRepository Product { get; private set; }
		public ICategoryRepository Category { get; private set; }
		public UnitOfWork(ApplicationDbContext context)
		{
			_context = context;
			Category = new CategoryRepository(_context);
			Product = new ProductRepository(_context);
		}

		public int Save()
		{
			return _context.SaveChanges();
		}
		public void Dispose()
		{

			_context.Dispose();

		}
	}
	
} 
