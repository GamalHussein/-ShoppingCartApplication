using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Entities.Repositores
{
	public interface IUnitOfWork : IDisposable
	{
		ICategoryRepository Category { get; }
		IProductRepository Product { get; }
		IShoppingCartRepository ShoppingCart { get; }
		int Save();
	}
}
