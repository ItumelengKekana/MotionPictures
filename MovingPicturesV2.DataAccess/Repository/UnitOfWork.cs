﻿using MovingPicturesV2.DataAccess.Data;
using MovingPicturesV2.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovingPicturesV2.DataAccess.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private ApplicationDbContext _db;

		public UnitOfWork(ApplicationDbContext db)
		{
			_db = db;
			Genre = new GenreRepository(_db);
			MediaType = new MediaTypeRepository(_db);
			Product = new ProductRepository(_db);
			Company = new CompanyRepository(_db);
			ShoppingCart = new ShoppingCartRepository(_db);
			ApplicationUser = new ApplicationUserRepository(_db);
		}
		public IGenreRepository Genre { get; private set; }

		public IMediaTypeRepository MediaType { get; private set; }

		public IProductRepository Product { get; private set; }

		public ICompanyRepository Company { get; private set; }

		public IShoppingCartRepository ShoppingCart { get; private set; }

		public IApplicationUserRepository ApplicationUser { get; private set; }

		public void Save()
		{
			_db.SaveChanges();
		}
	}
}