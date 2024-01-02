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
	public class GenreRepository : Repository<GenreModel>, IGenreRepository
	{
		private ApplicationDbContext _db;

		public GenreRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public void Update(GenreModel obj)
		{
			_db.Genres.Update(obj);
		}
	}
}
