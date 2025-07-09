using MyShop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Entities.Repositores
{
	public interface IShoppingCartRepository:IBaseRepository<ShopingCart>
	{
		int IncreaseCount(ShopingCart shopingCart, int count = 1);
		int DecreaseCount(ShopingCart shopingCart, int count = 1);
	}
}
