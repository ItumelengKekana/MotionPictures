using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovingPicturesV2.Models
{
	public class ShoppingCart
	{
		public int Id { get; set; } //primary key of the shopping cart table

		public int ProductId { get; set; }

		[ForeignKey("ProductId")]
		[ValidateNever]
		public ProductModel Product { get; set; }

		[Range(1, 1000, ErrorMessage = "Please enter a value between 1 and 1000")]
		public int Count { get; set; }

		public string ApplicationUserId { get; set; } //the user ID

		[ForeignKey("ApplicationUserId")]
		[ValidateNever]
		public ApplicationUser ApplicationUser { get; set; } //foreign key to the user table

		[NotMapped] //This property should not be mapped to the database; it is a placeholder
		public double Price { get; set; }
	}
}
