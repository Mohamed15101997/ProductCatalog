namespace ProductCatalogTask.ViewModels
{
	public class CreateProductViewModel : ProductViewModel
	{
		[Required(ErrorMessage = "Creation Date Cannot be Empty")]
		public DateTime CreationDate { get; set; }

		[AllowedExtensions(FileSettings.AllowedExtensions)]
		[MaxSizeFile(FileSettings.MaxFileSizeInBytes)]
		public IFormFile Image { get; set; } = default!;
	}
}
