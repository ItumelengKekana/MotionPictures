using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovingPicturesV2.Models.ViewModels
{
	public class OrderVM
	{
		public OrderHeaderModel OrderHeader { get; set; }
		public IEnumerable<OrderDetailModel> OrderDetail { get; set; }
	}
}
