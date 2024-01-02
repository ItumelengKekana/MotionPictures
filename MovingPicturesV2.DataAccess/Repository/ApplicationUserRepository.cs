﻿using MovingPicturesV2.DataAccess.Data;
using MovingPicturesV2.DataAccess.Repository.IRepository;
using MovingPicturesV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovingPicturesV2.DataAccess.Repository
{
	public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
	{
		private ApplicationDbContext _db;

		public ApplicationUserRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

	}
}