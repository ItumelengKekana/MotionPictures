using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovingPicturesV2.Models
{
	public class GenreModel
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }

		[DisplayName("Display Order")] //the order in which these will be displayed
		[Range(1, 100, ErrorMessage = "Display Order must be between 1 and 100 only!")]
		public int DisplayOrder { get; set; }
		public DateTime CreatedDatetime { get; set; } = DateTime.Now; //creates a default value
	}
}
