namespace ProductCatalogTask.ViewModels
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Name is Required")]
		public string UserName { get; set; }
		[Required(ErrorMessage = "Password is Required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		public bool RememberMe { get; set; }
	}
}
