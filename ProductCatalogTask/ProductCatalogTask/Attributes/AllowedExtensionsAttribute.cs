namespace ProductCatalogTask.Attributes
{
	public class AllowedExtensionsAttribute : ValidationAttribute
	{
		private readonly string _allowedExtensions;
		public AllowedExtensionsAttribute(string allowedExtensions)
		{
			_allowedExtensions = allowedExtensions;
		}
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			var file = value as IFormFile;
			if (file is not null)
			{
				// I Have Extension get from Form
				var extension = Path.GetExtension(file.FileName);
				// convert Allowed Extension to Array To Search About Extension
				var isAllowed = _allowedExtensions.Split(",").Contains(extension, StringComparer.OrdinalIgnoreCase);

				if (!isAllowed)
				{
					return new ValidationResult($"Only {_allowedExtensions} are allowed");
				}
			}
			return ValidationResult.Success;
		}
	}
}
