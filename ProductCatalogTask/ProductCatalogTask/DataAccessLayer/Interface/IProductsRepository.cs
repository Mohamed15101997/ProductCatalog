namespace ProductCatalogTask.DataAccessLayer.Interface
{
	public interface IProductsRepository
	{
		IEnumerable<Product> GetAllProduct();
		Task<Product?> GetProductById(int id);
		Task AddProduct(Product product);
		Task<bool> UpdateProduct(Product product);
		Task<bool> DeleteProduct(Product product);

	}
}
