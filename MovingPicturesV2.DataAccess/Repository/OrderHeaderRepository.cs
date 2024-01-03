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
	public class OrderHeaderRepository : Repository<OrderHeaderModel>, IOrderHeaderRepository
	{
		private ApplicationDbContext _db;

		public OrderHeaderRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public void Update(OrderHeaderModel obj)
		{
			_db.OrderHeaders.Update(obj);
		}

		public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
		{
			var orderFromDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == id);

			if (orderFromDb != null)
			{
				orderFromDb.OrderStatus = orderStatus;
				if (paymentStatus != null)
				{
					orderFromDb.PaymentStatus = paymentStatus;
				}
			}
		}
		public void UpdateStripePaymentID(int id, string sessionId, string paymentIntentId)
		{
			var orderFromDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == id);

			orderFromDb.SessionId = sessionId;
			orderFromDb.PaymentIntentId = paymentIntentId;
		}
	}
}
