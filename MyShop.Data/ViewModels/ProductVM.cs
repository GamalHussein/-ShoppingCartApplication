﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyShop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Entities.ViewModels
{
	public class ProductVM
	{
		public Product product { get; set; }
		[ValidateNever]
		public IEnumerable<SelectListItem> Categories { get; set; }
	}
}
