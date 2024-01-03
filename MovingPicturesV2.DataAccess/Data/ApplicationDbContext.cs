using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovingPicturesV2.Models;

namespace MovingPicturesV2.DataAccess.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext()
		{

		}

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer("Server=ITU-PC;Database=MovingPicturesV3;Trusted_Connection=True;TrustServerCertificate=True;");
			}
		}

		public DbSet<GenreModel> Genres { get; set; } //creating the table name (Genres) with columns from the Model

		public DbSet<MediaTypeModel> MediaTypes { get; set; } //creating the table name (MediaTypes) with columns from the Model
		public DbSet<ProductModel> Products { get; set; } //creating the table name (Products) with columns from the Model
		public DbSet<ApplicationUser> ApplicationUsers { get; set; } //creating the users table which extends IdentityUser
		public DbSet<CompanyModel> Companies { get; set; } //creating the companies table
		public DbSet<ShoppingCart> ShoppingCarts { get; set; } //creating the shopping cart table
		public DbSet<OrderHeaderModel> OrderHeaders { get; set; }
		public DbSet<OrderDetailModel> OrderDetails { get; set; }
	}
}
