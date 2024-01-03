using MovingPicturesV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovingPicturesV2.DataAccess.Repository.IRepository
{
	public interface IOrderDetailRepository : IRepository<OrderDetailModel>
	{
		void Update(OrderDetailModel obj);

	}
}
