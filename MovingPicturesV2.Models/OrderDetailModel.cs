using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovingPicturesV2.Models
{
	public class OrderDetailModel
	{
		public int Id { get; set; }

		[Required]
		public int OrderId { get; set; }

		[ForeignKey("OrderId")]
		[ValidateNever]
		public OrderHeaderModel OrderHeader { get; set; }

		[Required]
		public int ProductId { get; set; }

		[ForeignKey("ProductId")]
		[ValidateNever]
		public ProductModel Product { get; set; }
		public int Count { get; set; }
		public double Price { get; set; }
	}
}
