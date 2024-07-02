namespace AutoTrade.Services.Data.Interfaces
{
	using Web.ViewModels.Category;

	public interface ICategoryService
	{
		Task<IEnumerable<CarSelectCategoryViewModel>> AllCategoriesAsync();

		Task<bool> ExistsByIdAsync(int id);

		Task<IEnumerable<string>> AllCategoryNamesAsync();
	}
}
