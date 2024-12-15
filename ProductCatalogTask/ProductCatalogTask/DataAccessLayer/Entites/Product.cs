namespace ProductCatalogTask.DataAccessLayer.Entites
{
	public class Product : BaseEntity
	{
		[Required(ErrorMessage = "Creation Date Cannot be Empty")]
		public DateTime CreationDate { get; set; }

		[Required(ErrorMessage = "User Id Cannot be Empty")]

		[ForeignKey("User")]
		public string UserId { get; set; } = string.Empty;
		public virtual ApplicationUser User { get; set; } = default!;

		[Required(ErrorMessage = "Start Date Cannot be Empty")]
		public DateTime StartDate { get; set; }
		[Range(1, 7, ErrorMessage = "Duration Must Start from 1 Day To Week")]
		public int Duration { get; set; }

		[Range(1, 10000, ErrorMessage = "Price Must be Between 1 and 10000")]
		public double Price { get; set; }

		[MaxLength(200)]
		public string Image { get; set; } = string.Empty;

		[ForeignKey("Category")]
		public int CategoryId { get; set; }
		public Category Category { get; set; } = default!;
	}
}
