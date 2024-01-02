using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovingPicturesV2.Models
{
	public class MediaTypeModel
	{
		[Key]
		public int Id { get; set; }

		[DisplayName("Media Type")]
		[MaxLength(50)]
		[Required]
		public string Name { get; set; }
	}
}
