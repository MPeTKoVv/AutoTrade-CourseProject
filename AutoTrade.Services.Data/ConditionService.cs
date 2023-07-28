using AutoTrade.Services.Data.Interfaces;
using AutoTrade.Web.Data;
using AutoTrade.Web.ViewModels.Condition;
using Microsoft.EntityFrameworkCore;

namespace AutoTrade.Services.Data
{
	public class ConditionService : IConditionService
	{
		private readonly AutoTradeDbContext dbContext;

		public ConditionService(AutoTradeDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<IEnumerable<CarSelectConditionViewModel>> AllConditoinsAsync()
		{
			IEnumerable<CarSelectConditionViewModel> allConditoins = await this.dbContext
				.Conditions
				.Select(c => new CarSelectConditionViewModel
				{
					Id = c.Id,
					Name = c.Name,
				})
				.ToArrayAsync();

			return allConditoins;
		}
	}
}
