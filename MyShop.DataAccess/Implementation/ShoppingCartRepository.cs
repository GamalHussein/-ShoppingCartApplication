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
	public class ShoppingCartRepository : BaseRepository<ShopingCart>, IShoppingCartRepository
	{
		private readonly ApplicationDbContext _context;
		public ShoppingCartRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}

		public int DecreaseCount(ShopingCart shopingCart, int count = 1)
		{
			shopingCart.Count -= count;
			if (shopingCart.Count < 1)
			{
				shopingCart.Count = 1; // Ensure count does not go below 1
			}
			return shopingCart.Count;
		}

		public int IncreaseCount(ShopingCart shopingCart, int count = 1)
		{
			shopingCart.Count += count;
			return shopingCart.Count;
		}
	}
	
}
