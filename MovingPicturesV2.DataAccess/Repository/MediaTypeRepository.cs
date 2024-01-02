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
	public class MediaTypeRepository : Repository<MediaTypeModel>, IMediaTypeRepository
	{
		private ApplicationDbContext _db;

		public MediaTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

		public void Update(MediaTypeModel obj)
		{
			_db.MediaTypes.Update(obj);
		}
	}
}
