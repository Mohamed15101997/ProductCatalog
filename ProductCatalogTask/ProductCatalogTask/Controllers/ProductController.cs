using Microsoft.AspNetCore.Authorization;

namespace ProductCatalogTask.Controllers
{
	public class ProductController : Controller
	{
		private readonly ICategoriesService _categoriesService;
		private readonly IProductsService _productsService;
		public ProductController(ICategoriesService categoriesService, IProductsService productsService)
		{
			_categoriesService = categoriesService;
			_productsService = productsService;
		}
		[Authorize(Roles = "Admin")]
		public IActionResult Index()
		{
			return View(_productsService.GetAll());
		}
		[Authorize(Roles = "Admin,User")]
		public IActionResult DisplayProducts()
		{
			return View(_productsService.GetCurrent());
		}
		[Authorize(Roles = "Admin,User")]
		public async Task<IActionResult> Details(int id)
		{
			var product = await _productsService.GetById(id);
			if (product == null)
				return NotFound();
			return View("Details", product);
		}
		[Authorize(Roles = "Admin")]
		public IActionResult Create()
		{
			CreateProductViewModel createProductView = new CreateProductViewModel
			{
				Categories = _categoriesService.GetSelectList() ,
				StartDate = DateTime.Today
			};
			return View("Create", createProductView);
		}
		[Authorize(Roles = "Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateProductViewModel model)
		{
			if (!ModelState.IsValid)
			{
				model.Categories = _categoriesService.GetSelectList();
				return View(model);
			}
			await _productsService.Create(model);
			return RedirectToAction(nameof(Index));
		}

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Edit(int id)
		{
			var product =await _productsService.GetById(id);
			if (product == null)
				return NotFound();
			EditProductViewModel model = new EditProductViewModel()
			{
				Id = id,
				Name = product.Name,
				CategoryId = product.CategoryId,
				Categories = _categoriesService.GetSelectList(),
				StartDate = product.StartDate,
				ImageName = product.Image,
				Duration = product.Duration,
				Price = product.Price,
			};
			return View("Edit", model);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(EditProductViewModel model)
		{
			if (!ModelState.IsValid)
			{
				model.Categories = _categoriesService.GetSelectList();
				return View(model);
			}
			var product = await _productsService.Update(model);
			if (product == null)
				return BadRequest();
			return RedirectToAction(nameof(Index));
		}

		[Authorize(Roles = "Admin")]
		[HttpDelete]
		public async Task<IActionResult> Delete(int id)
		{
			var isDeleted = await _productsService.Delete(id);
			if (!isDeleted)
				return BadRequest();
			return Ok();
		}
	}
}
