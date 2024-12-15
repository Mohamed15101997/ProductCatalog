namespace ProductCatalogTask.ViewModels
{
	public class EditProductViewModel : ProductViewModel
	{
		public int Id { get; set; }
		public string? ImageName { get; set; }

		[AllowedExtensions(FileSettings.AllowedExtensions)]
		[MaxSizeFile(FileSettings.MaxFileSizeInBytes)]
		public IFormFile? Image { get; set; } = default!;
	}
}
