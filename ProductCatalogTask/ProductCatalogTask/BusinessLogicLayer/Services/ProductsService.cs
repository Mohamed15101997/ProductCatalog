using Microsoft.EntityFrameworkCore;
using ProductCatalogTask.BusinessLogicLayer.Interface;

namespace ProductCatalogTask.BusinessLogicLayer.Services
{
	public class ProductsService : IProductsService
	{
		private readonly IProductsRepository _productsRepository;
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly string _imagesPath;
		public ProductsService(IProductsRepository productsRepository, IWebHostEnvironment webHostEnvironment)
		{
			_productsRepository = productsRepository;

			_webHostEnvironment = webHostEnvironment;
			_imagesPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.imagesPath}";
		}
		public IEnumerable<Product> GetAll()
		{
			return _productsRepository.GetAllProduct(); 
		}
		public IEnumerable<Product> GetCurrent()
		{
			var today = DateTime.Now.Date;

			return _productsRepository.GetAllProduct()
				.Where(p => p.StartDate.Date <= today && today <= p.StartDate.AddDays(p.Duration).Date)
				.ToList();
		}

		public async Task<Product?> GetById(int id)
		{
			return await _productsRepository.GetProductById(id);
		}
		public async Task Create(CreateProductViewModel model)
		{
			var imageName = await SaveCover(model.Image);

			Product product = new()
			{
				Name = model.Name,
				UserId = "jskajskajksj817",
				CategoryId = model.CategoryId,
				Image = imageName , 
				StartDate = model.StartDate,
				Duration = model.Duration,
				CreationDate = DateTime.Now,
				Price = model.Price,
			};
			await _productsRepository.AddProduct(product);
		}

		public async Task<Product?> Update(EditProductViewModel model)
		{
			var product = await _productsRepository.GetProductById(model.Id);
			if (product == null)
				return null;

			product.Name = model.Name;
			product.StartDate = model.StartDate;
			product.CategoryId = model.CategoryId;
			product.Duration = model.Duration;
			product.Price = model.Price;

			var hasNewCover = model.Image is not null;
			var oldCover = product.Image;

			if (hasNewCover)
			{
				product.Image = await SaveCover(model.Image!);
			}
			var isUpdated = await _productsRepository.UpdateProduct(product);

			if (isUpdated)
			{
				if (hasNewCover)
				{
					var coverPath = Path.Combine(_imagesPath, oldCover);
					File.Delete(coverPath);
				}
				return product;
			}
			if (hasNewCover)
			{
				var coverPath = Path.Combine(_imagesPath, product.Image);
				File.Delete(coverPath);
			}
			return null;
		}
		public async Task<bool> Delete(int id)
		{
			var isDeleted = false;
			Product product = await _productsRepository.GetProductById(id);
			if (product == null)
				return isDeleted;
			;
			var isDeletedSuccess = await _productsRepository.DeleteProduct(product);
			if (isDeletedSuccess)
			{
				isDeleted = true;
				var image = Path.Combine(_imagesPath, product.Image);
				File.Delete(image);
			}
			return isDeleted;
		}
		private async Task<string> SaveCover(IFormFile image)
		{
			var imageName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
			var path = Path.Combine(_imagesPath, imageName);
			using var stream = File.Create(path);
			await image.CopyToAsync(stream);
			return imageName;
		}
	}
}
