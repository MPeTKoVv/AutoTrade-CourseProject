using AutoTrade.Data.Models;
using AutoTrade.Services.Data.Interfaces;
using AutoTrade.Services.Data.Models.Car;
using AutoTrade.Web.Data;
using AutoTrade.Web.ViewModels.Car;
using AutoTrade.Web.ViewModels.Car.Enums;
using AutoTrade.Web.ViewModels.Home;
using AutoTrade.Web.ViewModels.Seller;
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

		public async Task<AllCarsFilteredAndPagedServiceModel> AllAsync(AllCarsQueryModel queryModel)
		{
			IQueryable<Car> carsQuery = this.dbContext
				.Cars
				.Include(c => c.EngineType)
				.AsQueryable();

			if (!string.IsNullOrEmpty(queryModel.Category))
			{
				carsQuery = carsQuery
					.Where(c => c.Category.Name == queryModel.Category);
			}

			if (!string.IsNullOrEmpty(queryModel.Condition))
			{
				carsQuery = carsQuery
					.Where(c => c.Condition.Name == queryModel.Condition);
			}

			if (!string.IsNullOrEmpty(queryModel.EngineType))
			{
				carsQuery = carsQuery
					.Where(c => c.EngineType.Type == queryModel.EngineType);
			}

			if (!string.IsNullOrEmpty(queryModel.SearchString))
			{
				string wildCard = $"%{queryModel.SearchString}%";

				carsQuery = carsQuery
					.Where(c => EF.Functions.Like(c.Make, wildCard) ||
					EF.Functions.Like(c.Model, wildCard));
			}

			carsQuery = queryModel.CarSorting switch
			{
				CarSorting.Newest => carsQuery
					.OrderByDescending(c => c.AddedOn),
				CarSorting.Oldest => carsQuery
					.OrderBy(c => c.AddedOn),
				CarSorting.PriceDescending => carsQuery
					.OrderByDescending(c => c.Price),
				CarSorting.PriceAscending => carsQuery
					.OrderBy(c => c.Price),
				CarSorting.YearDescending => carsQuery
					.OrderByDescending(c => c.Year),
				CarSorting.YearAscending => carsQuery
					.OrderBy(c => c.Year),
				CarSorting.HorsepowerDescending => carsQuery
					.OrderByDescending(c => c.Horsepower),
				CarSorting.HorsepowerAscending => carsQuery
					.OrderByDescending(c => c.Horsepower),
				_ => carsQuery
					.OrderByDescending(c => c.AddedOn)
			};

			IEnumerable<CarAllViewModel> allCars = await carsQuery
				.Where(c => c.IsActive)
				.Skip((queryModel.CurrentPage - 1) * queryModel.CarsPerPage)
				.Take(queryModel.CarsPerPage)
				.Select(c => new CarAllViewModel
				{
					Id = c.Id.ToString(),
					Make = c.Make,
					Model = c.Model,
					Price = c.Price,
					Horsepower = c.Horsepower,
					Year = c.Year,
					ImageUrl = c.ImageUrl,
					IsBought = c.CustomerId.HasValue
				})
				.ToArrayAsync();
			int totalCars = carsQuery.Count();



			return new AllCarsFilteredAndPagedServiceModel
			{
				TotalCarsCount = totalCars,
				Cars = allCars
			};
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
					Year = c.Year,
					Horsepower = c.Horsepower,
					Price = c.Price,
					ImageUrl = c.ImageUrl
				})
				.ToArrayAsync();

			return orderedCars;
		}

		//public async Task<IEnumerable<IndexViewModel>> AllMyCarsForSale(string sellerId)
		//{
		//	IEnumerable<IndexViewModel> allMyCarsForSale = await dbContext
		//		.OrderByDescending(c => c.AddedOn)
		//		.Where(c => c.SellerId.ToString() == sellerId)
		//		.Select(c => new IndexViewModel()
		//		{
		//			Make = c.Make,
		//			Model = c.Model,
		//			Year = c.Year,
		//			Horsepower = c.Horsepower,
		//			Price = c.Price,
		//			ImageUrl = c.ImageUrl
		//              })
		//		.ToArrayAsync();

		//	return allMyCarsForSale;
		//}

		public async Task CreateAndReturnIdAsync(CarFormModel carViewModel, string sellerId)
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
				ImageUrl = carViewModel.ImageUrl,
				AddedOn = DateTime.UtcNow,
				CategoryId = carViewModel.CategoryId,
				ConditionId = carViewModel.ConditionId,
				EngineId = carViewModel.EngineTypeId,
				SellerId = Guid.Parse(sellerId)
			};
			//car.Images.Add(new Image { Url = carViewModel.ImageUrl, CarId = car.Id.ToString() });
			//car.Images.Add(carViewModel.ImageUrl);

			await dbContext.Cars.AddAsync(car);
			await dbContext.SaveChangesAsync();

		}

        public async Task<bool> ExistsByIdAsync(string Id)
        {
			var result = await dbContext
				.Cars
				.AnyAsync(c => c.Id.ToString() == Id);

			return result;
        }

        public async Task<CarDetailsViewModel> GetDetailsByIdAsync(string carId)
		{
			Car? car = await dbContext
				.Cars
				.Include(c=>c.Category)
				.Include(c=>c.Condition)
				.Include(c=>c.EngineType)
				.Include(c => c.Seller)
				.ThenInclude(s => s.User)
				.Where(c => c.IsActive)
				.FirstOrDefaultAsync(c => c.Id.ToString() == carId);

			if (car == null)
			{
				return null;
			}

			return new CarDetailsViewModel
			{
				Id = car.Id.ToString(),
				Make = car.Make,
				Model = car.Model,
				Price = car.Price,
				Year = car.Year,
				Horsepower = car.Horsepower,
				ImageUrl = car.ImageUrl,
				Category = car.Category.Name,
				Condition = car.Condition.Name,
				EngineType = car.EngineType.Type,
				Description = car.Description,
				Seller = new SellerInfoOnCarViewModel
				{
					Email = car.Seller.User.Email,
					PhoneNumber = car.Seller.PhoneNumber
				}
			};
		}
	}
}
