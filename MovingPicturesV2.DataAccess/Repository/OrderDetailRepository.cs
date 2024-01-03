using MovingPicturesV2.DataAccess.Data;
using MovingPicturesV2.DataAccess.Repository.IRepository;
using MovingPicturesV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovingPicturesV2.DataAccess.Repository
{
	public class OrderDetailRepository : Repository<OrderDetailModel>, IOrderDetailRepository
	{
		private ApplicationDbContext _db;

		public OrderDetailRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public void Update(OrderDetailModel obj)
		{
			_db.OrderDetails.Update(obj);
		}
	}
}
