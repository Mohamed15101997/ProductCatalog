namespace ProductCatalogTask.ViewModels
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage ="Name cannot be Empty")
		,MaxLength(20,ErrorMessage = "Name cannot be more than 20 Characters")
		,MinLength(3,ErrorMessage = "Name cannot be less than 3 Characters")]
		public string Name { get; set; } = string.Empty;

		[Required(ErrorMessage = "Name cannot be Empty")
		, MaxLength(20, ErrorMessage = "User Name cannot be more than 20 Characters")
		, MinLength(3, ErrorMessage = "User Name cannot be less than 3 Characters")]
		[Display(Name = "User Name")]
		public string UserName { get; set; } = string.Empty;

		[Required(ErrorMessage = "Password is Required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[DataType(DataType.Password) 
		, Compare("Password")]
		[Display(Name = "Confirm Password")]
		public string ConfirmPassword { get; set; } = string.Empty;
	}
}
