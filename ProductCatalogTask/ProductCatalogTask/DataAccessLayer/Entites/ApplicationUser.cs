namespace ProductCatalogTask.DataAccessLayer.Entites
{
	public class ApplicationUser : IdentityUser
	{
		public string Name { get; set; } = string.Empty;
	}
}
