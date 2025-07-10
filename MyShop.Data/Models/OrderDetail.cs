using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Entities.Models
{
	public class OrderDetail
	{
		public int Id { get; set; }
		public int Count { get; set; }
		public decimal Price { get; set; }
		public string? ProductId { get; set; }
		public string? OrderHeaderId { get; set; }
		public Product? Product { get; set; }
		public OrderHeader? OrderHeader { get; set; }
	}
}
