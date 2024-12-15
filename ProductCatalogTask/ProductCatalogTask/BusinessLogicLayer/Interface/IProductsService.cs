namespace ProductCatalogTask.BusinessLogicLayer.Interface
{
	public interface IProductsService
	{
		IEnumerable<Product> GetAll();
		IEnumerable<Product> GetCurrent();
		Task<Product?> GetById(int id);
		Task Create(CreateProductViewModel model);
		Task<Product?> Update(EditProductViewModel model);
		Task<bool> Delete(int id);
	}
}
