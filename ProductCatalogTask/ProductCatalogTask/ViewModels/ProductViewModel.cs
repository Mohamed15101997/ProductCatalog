namespace ProductCatalogTask.ViewModels
{
	public class ProductViewModel
	{
		[MaxLength(250, ErrorMessage = "The Max Size of Name is 250 Character"),
			Required(ErrorMessage = "The Name Cannot Empty")]
		public string Name { get; set; } = string.Empty;

		[Required(ErrorMessage = "Start Date Cannot be Empty")]
		[DataType(DataType.Date)]
		public DateTime StartDate { get; set; }

		[Range(1, 7, ErrorMessage = "Duration Must Start from 1 Day To Week")]
		public int Duration { get; set; }

		[Range(1, 10000, ErrorMessage = "Price Must be Between 1 and 10000")]
		public double Price { get; set; }


		[Display(Name = "Category")]
		public int CategoryId { get; set; }
		public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();
	}
}
