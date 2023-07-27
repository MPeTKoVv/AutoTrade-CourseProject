using AutoTrade.Services.Data.Interfaces;
using AutoTrade.Web.Data;
using AutoTrade.Web.ViewModels.Home;
using Microsoft.EntityFrameworkCore;

namespace AutoTrade.Services.Data
{
	public class CarService : ICarService
	{
		private readonly AutoTradeDbContext dbContext;

		public CarService(AutoTradeDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<IEnumerable<IndexViewModel>> AllCarsOrderedByAddedOnDescendingAsync()
		{
			IEnumerable<IndexViewModel> orderedCars = await dbContext
				.Cars
				.OrderByDescending(c => c.AddedOn)
				.Select(c => new IndexViewModel()
				{
					Id = c.Id.ToString(),
					Make = c.Make,
					Model = c.Model,
					Price = c.Price,
					ImageUrl = c.Images.First()
                })
				.ToArrayAsync();

			return orderedCars;
		}
	}
}
