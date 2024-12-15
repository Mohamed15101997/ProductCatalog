namespace ProductCatalogTask.DataAccessLayer.Repositories
{
	public class ProductsRepository : IProductsRepository
	{
		private readonly ApplicationDbContext _context;
		public ProductsRepository(ApplicationDbContext context)
		{
			_context = context;
		}
		public IEnumerable<Product> GetAllProduct()
		{
			return _context.Products
				.Include(g => g.Category)
				.AsNoTracking()
				.ToList();
		}
		public async Task<Product?> GetProductById(int id)
		{
			return await  _context.Products
				.Include(g => g.Category)
				.AsNoTracking()
				.FirstOrDefaultAsync(g => g.Id == id);
		}
		public async Task AddProduct(Product product)
		{
			await _context.AddAsync(product);
			await _context.SaveChangesAsync();
		}
		public async Task<bool> UpdateProduct(Product product)
		{
			_context.Products.Update(product);
			var affectedRows = await _context.SaveChangesAsync();
			return affectedRows > 0;
		}
		public async Task<bool> DeleteProduct(Product product)
		{
			_context.Products.Remove(product);
			var affectedRows = await _context.SaveChangesAsync();
			return affectedRows > 0;
		}
	}
}
