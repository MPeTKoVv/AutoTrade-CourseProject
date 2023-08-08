namespace AutoTrade.Services.Data
{
	using Microsoft.EntityFrameworkCore;

	using Interfaces;
	using Web.Data;
	using Web.ViewModels.Category;

	public class CategoryService : ICategoryService
	{
		private readonly AutoTradeDbContext dbContext;

		public CategoryService(AutoTradeDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<IEnumerable<CarSelectCategoryViewModel>> AllCategoriesAsync()
		{
			IEnumerable<CarSelectCategoryViewModel> allCategories = await this.dbContext
				.Categories
				.Select(c => new CarSelectCategoryViewModel
				{
					Id = c.Id,
					Name = c.Name,
				})
				.ToArrayAsync();

			return allCategories;
		}

		public async Task<IEnumerable<string>> AllCategoryNamesAsync()
		{
			IEnumerable<string> allNames = await dbContext
				.Categories
				.Select(c => c.Name)
				.ToArrayAsync();

			return allNames;
		}

		public async Task<bool> ExistsByIdAsync(int id)
		{
			bool result = await dbContext
				.Categories
				.AnyAsync(c => c.Id == id);

			return result;
		}
	}
}
