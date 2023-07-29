using AutoTrade.Data.Models;
using AutoTrade.Services.Data.Interfaces;
using AutoTrade.Web.Data;
using AutoTrade.Web.ViewModels.Car;
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

		public async Task<IEnumerable<IndexViewModel>> AllCarsAsync()
		{
			var orderedCars = await dbContext
				.Cars
				.OrderByDescending(c => c.AddedOn)
				.Take(1)
				.Select(c => new IndexViewModel()
				{
					Id = c.Id.ToString(),
					Make = c.Make,
					Model = c.Model,
					Year = c.Year,
					Horsepower = c.Horsepower,
					Price = c.Price,
					ImageUrl = c.Images.FirstOrDefault()!.Url
				})
				.ToArrayAsync();

			return orderedCars;
		}

		public async Task<IEnumerable<IndexViewModel>> AllCarsOrderedByAddedOnDescendingAsync()
		{
			IEnumerable<IndexViewModel> orderedCars = await dbContext
				.Cars
				.Include(c => c.Images)
				.OrderByDescending(c => c.AddedOn)
				.Select(c => new IndexViewModel()
				{
					Id = c.Id.ToString(),
					Make = c.Make,
					Model = c.Model,
					Year = c.Year,
					Horsepower = c.Horsepower,
					Price = c.Price,
					ImageUrl = c.Images.FirstOrDefault()!.Url
				})
				.ToArrayAsync();

			return orderedCars;
		}

		public async Task<IEnumerable<IndexViewModel>> AllMyCarsForSale(string sellerId)
		{
			IEnumerable<IndexViewModel> allMyCarsForSale = await dbContext
				.Cars.Include(c => c.Images)
				.OrderByDescending(c => c.AddedOn)
				.Where(c => c.SellerId.ToString() == sellerId)
				.Select(c => new IndexViewModel()
				{
					Make = c.Make,
					Model = c.Model,
					Year = c.Year,
					Horsepower = c.Horsepower,
					Price = c.Price,
					ImageUrl = c.Images.FirstOrDefault()!.Url
				})
				.ToArrayAsync();

			return allMyCarsForSale;
		}

		public async Task CreateAndReturnIdAsync(CarViewModel carViewModel, string sellerId)
		{
			Car car = new Car
			{
				Make = carViewModel.Make,
				Model = carViewModel.Model,
				Country = carViewModel.Country,
				Description = carViewModel.Description,
				Horsepower = carViewModel.Horsepower,
				Price = carViewModel.Price,
				Year = carViewModel.Year,
				Mileage = carViewModel.Mileage,
				AddedOn = DateTime.UtcNow,
				CategoryId = carViewModel.CategoryId,
				ConditionId = carViewModel.ConditionId,
				EngineId = carViewModel.EngineTypeId,
				SellerId = Guid.Parse(sellerId)
			};
			 car.Images.Add(new Image { Url = carViewModel.ImageUrl});

			await dbContext.Cars.AddAsync(car);
			await dbContext.SaveChangesAsync();

		}
	}
}
