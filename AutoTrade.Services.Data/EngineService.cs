namespace AutoTrade.Services.Data
{
	using Microsoft.EntityFrameworkCore;

	using Interfaces;
	using Web.Data;
	using Web.ViewModels.Engine;

	public class EngineService : IEngineService
	{
		private readonly AutoTradeDbContext dbContext;

        public EngineService(AutoTradeDbContext dbContext)
        {
			this.dbContext = dbContext;
        }

		public async Task<IEnumerable<string>> AllEngineTypeNamesAsync()
		{
			IEnumerable<string> allNames = await dbContext
				.EngineTypes
				.Select(et => et.Type)
				.ToArrayAsync();

			return allNames;
		}

		public async Task<IEnumerable<CarSelectEngineTypeViewModel>> AllEngineTypesAsync()
		{
			IEnumerable<CarSelectEngineTypeViewModel> allEngineTypes = await this.dbContext
				.EngineTypes
				.Select(e => new CarSelectEngineTypeViewModel
				{
					Id = e.Id,
					Type = e.Type
				})
				.ToArrayAsync();

			return allEngineTypes;
		}

		public async Task<bool> ExistsByIdAsync(int id)
		{
			bool result = await dbContext
				.EngineTypes
				.AnyAsync(e => e.Id == id);

			return result;
		}
	}
}
