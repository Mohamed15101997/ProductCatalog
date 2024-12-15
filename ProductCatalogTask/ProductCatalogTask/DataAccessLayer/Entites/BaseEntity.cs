namespace ProductCatalogTask.DataAccessLayer.Entites
{
	public class BaseEntity
	{
		public int Id { get; set; }

		[MaxLength(250, ErrorMessage = "The Max Size of Name is 250 Character"),
			Required(ErrorMessage = "The Name Cannot Empty")]
		public string Name { get; set; } = string.Empty;
	}
}
