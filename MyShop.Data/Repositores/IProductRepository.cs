using MyShop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Entities.Repositores
{
	public interface IProductRepository:IBaseRepository<Product>
	{
		void Update(Product product);
	}
}
