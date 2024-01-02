using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovingPicturesV2.Models
{
	public class ProductModel
	{
		public int Id { get; set; }

		[Required]
		public string Title { get; set; }

		public string Synopsis { get; set; }

		[Required]
		[DataType(DataType.Date)]
		public DateTime ReleaseDate { get; set; }

		[Required]
		public string Director { get; set; }

		[Required]
		[Range(1, 10000)]
		public double ListPrice { get; set; }

		[Required]
		[Range(1, 10000)]
		public double Price { get; set; }

		[Required]
		[Range(1, 10000)]
		public double Price50 { get; set; } //when buying 50 or more books

		[Required]
		[Range(1, 10000)]
		public double Price100 { get; set; } //when buying 100 or more books

		[ValidateNever]
		public string ImageURL { get; set; }

		[Required]
		public int GenreId { get; set; }  //foreign key

		[ValidateNever]
		public GenreModel Genre { get; set; }  //automatically gets mapped as a foreign key

		[Required]
		public int MediaTypeId { get; set; } //foreign key

		[ValidateNever]
		public MediaTypeModel MediaType { get; set; } //automatically gets mapped as a foreign key

	}
}
