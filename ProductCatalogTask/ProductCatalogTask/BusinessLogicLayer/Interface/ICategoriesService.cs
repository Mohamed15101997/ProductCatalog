namespace ProductCatalogTask.BusinessLogicLayer.Interface
{
	public interface ICategoriesService
	{
		IEnumerable<SelectListItem> GetSelectList();
	}
}
