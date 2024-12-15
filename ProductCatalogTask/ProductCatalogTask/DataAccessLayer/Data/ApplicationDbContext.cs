namespace ProductCatalogTask.DataAccessLayer.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}
		public ApplicationDbContext() : base()
		{
		}
		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Category>().HasData(
			new Category { Id = 1, Name = "Electronics" },
			new Category { Id = 2, Name = "Clothing" },
			new Category { Id = 3, Name = "Books" },
			new Category { Id = 4, Name = "Sports" },
			new Category { Id = 5, Name = "Toys" }
		);

			builder.Entity<IdentityRole>().HasData(
				new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
					new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER" }
			);
			base.OnModelCreating(builder);
		}
	}
}
