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
	public class ProductRepository : Repository<ProductModel>, IProductRepository
	{
		private ApplicationDbContext _db;

		public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

		public void Update(ProductModel obj)
		{
			var objFromDb = _db.Products.FirstOrDefault(u => u.Id == obj.Id); //retrieve the object and only modify fields that have been changed

			if (objFromDb != null)
			{
				objFromDb.Title = obj.Title;
				objFromDb.ReleaseDate = obj.ReleaseDate;
				objFromDb.Price = obj.Price;
				objFromDb.Price50 = obj.Price50;
				objFromDb.Price100 = obj.Price100;
				objFromDb.ListPrice = obj.ListPrice;
				objFromDb.Synopsis = obj.Synopsis;
				objFromDb.GenreId = obj.GenreId;
				objFromDb.MediaTypeId = obj.MediaTypeId;
				objFromDb.Director = obj.Director;
				if(obj.ImageURL != null)
				{
					obj.ImageURL = obj.ImageURL;  //only changes the url if the user sets it
				}
			}
		}
	}
}
