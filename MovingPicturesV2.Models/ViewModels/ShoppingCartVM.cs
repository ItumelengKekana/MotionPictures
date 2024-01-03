using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovingPicturesV2.Models.ViewModels
{
	public class ShoppingCartVM
	{
		public IEnumerable<ShoppingCart> ListCart { get; set; }

		public OrderHeaderModel OrderHeader { get; set; }

	}
}
