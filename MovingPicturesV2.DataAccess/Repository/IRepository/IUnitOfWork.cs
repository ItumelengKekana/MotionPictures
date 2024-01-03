using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovingPicturesV2.DataAccess.Repository.IRepository
{
	public interface IUnitOfWork
	{
		IGenreRepository Genre { get; }

		IMediaTypeRepository MediaType { get; }

		IProductRepository Product { get; }

		ICompanyRepository Company { get; }
		IApplicationUserRepository ApplicationUser { get; }
		IShoppingCartRepository ShoppingCart { get; }
		IOrderDetailRepository OrderDetail { get; }
		IOrderHeaderRepository OrderHeader { get; }

		void Save();
	}
}
