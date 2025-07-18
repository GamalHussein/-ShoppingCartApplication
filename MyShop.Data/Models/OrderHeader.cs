﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Entities.Models
{
	public class OrderHeader
	{
		public int Id { get; set; }
		public DateTime OrderDate { get; set; }
		public DateTime ShipingDate { get; set; }
		public decimal TotalPrice { get; set; }
		public string? OrderStatus { get; set; }
		public string? PaymentStatus { get; set; }
		public string? TrackingNumber { get; set; }
		public string? Carrier { get; set; }
		public DateTime PaymentDate { get; set; }
		//Stripe 
		public string? SessionId { get; set; }
		public string? PaymentIntentId { get; set; }
		[ForeignKey("ApplicationUser")]
		public string ApplicationUserId { get; set; }
		[ValidateNever]
		public ApplicationUser? ApplicationUser { get; set; }

	}
}
