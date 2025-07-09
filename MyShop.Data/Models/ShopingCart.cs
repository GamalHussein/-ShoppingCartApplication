using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Entities.Models
{
	public class ShopingCart
	{
		public int Id { get; set; }
		public int Count { get; set; }
		[ForeignKey("product")]
		public int ProductId { get; set; }
		[ValidateNever]
		public Product product { get; set; }
		[ForeignKey("ApplicationUser")]
		public string ApplicationUserId { get; set; }
		[ValidateNever]
		public ApplicationUser ApplicationUser { get; set; }
	}
}
