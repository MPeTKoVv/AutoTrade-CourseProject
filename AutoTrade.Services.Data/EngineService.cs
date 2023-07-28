using AutoTrade.Services.Data.Interfaces;
using AutoTrade.Web.Data;
using AutoTrade.Web.ViewModels.Engine;
using Microsoft.EntityFrameworkCore;

namespace AutoTrade.Services.Data
{
	public class EngineService : IEngineService
	{
		private readonly AutoTradeDbContext dbContext;

        public EngineService(AutoTradeDbContext dbContext)
        {
			this.dbContext = dbContext;
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
	}
}
