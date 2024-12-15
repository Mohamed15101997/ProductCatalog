namespace ProductCatalogTask.DataAccessLayer.Entites
{
	public class Category : BaseEntity
	{
		public ICollection<Product> Products { get; set; } = new List<Product>();
	}
}
