﻿using MyShop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Entities.ViewModels
{
	public class ShoppingCartVM
	{
		public IEnumerable<ShopingCart> Carts { get; set; }
		public decimal TotalPrice { get; set; }
	}
}
