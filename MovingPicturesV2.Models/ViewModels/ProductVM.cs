using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MovingPicturesV2.Models.ViewModels
{
	public class ProductVM
	{
		public ProductModel Product { get; set; }

		[ValidateNever]
		public IEnumerable<SelectListItem> GenreList { get; set; }

		[ValidateNever]
		public IEnumerable<SelectListItem> MediaTypeList { get; set; }
	}
}
