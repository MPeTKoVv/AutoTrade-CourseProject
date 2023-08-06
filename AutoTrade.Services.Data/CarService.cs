using AutoTrade.Data.Models;
using AutoTrade.Services.Data.Interfaces;
using AutoTrade.Services.Data.Models.Car;
using AutoTrade.Web.Data;
using AutoTrade.Web.ViewModels.Car;
using AutoTrade.Web.ViewModels.Car.Enums;
using AutoTrade.Web.ViewModels.Home;
using AutoTrade.Web.ViewModels.Seller;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

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

			if (!string.IsNullOrEmpty(queryModel.EngineType))
			{
				carsQuery = carsQuery
					.Where(c => c.EngineType.Type == queryModel.EngineType);
			}

			if (!string.IsNullOrEmpty(queryModel.Transmission))
			{
				carsQuery = carsQuery
					.Where(c => c.Transmission.Name == queryModel.Transmission);
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
					.OrderBy(c => c.Horsepower),
				_ => carsQuery
					.OrderByDescending(c => c.AddedOn)
			};

			IEnumerable<CarAllViewModel> allCars = await carsQuery
				.Where(c => c.IsActive && c.IsForSale)
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
				})
				.ToArrayAsync();
			int totalCars = carsQuery.Count();

			return new AllCarsFilteredAndPagedServiceModel
			{
				TotalCarsCount = totalCars,
				Cars = allCars
			};
		}

		public async Task<IEnumerable<CarAllViewModel>> AllByUserIdAsync(string userId)
		{
			ApplicationUser user = dbContext
				.Users
				.First(u => u.Id.ToString() == userId);

			IEnumerable<CarAllViewModel> usersCars = user
				.OwnedCars
				.Where(c => c.IsActive)
				.Select(c => new CarAllViewModel
				{
					Id = c.Id.ToString(),
					Make = c.Make,
					Model = c.Model,
					Price = c.Price,
					Year = c.Year,
					Horsepower = c.Horsepower,
					ImageUrl = c.ImageUrl
				})
				.ToList();

			return usersCars;
		}

		public async Task<IEnumerable<IndexViewModel>> AllCarsOrderedByAddedOnDescendingAsync()
		{
			IEnumerable<IndexViewModel> orderedCars = await dbContext
				.Cars
				.Where(c => c.IsActive)
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

		public async Task CreateAndReturnIdAsync(CarFormModel carViewModel, string sellerId)
		{
			Car car = new Car
			{
				Make = carViewModel.Make,
				Model = carViewModel.Model,
				Description = carViewModel.Description,
				Horsepower = carViewModel.Horsepower,
				Price = carViewModel.Price,
				Year = carViewModel.Year,
				Mileage = carViewModel.Mileage,
				ImageUrl = carViewModel.ImageUrl,
				AddedOn = DateTime.UtcNow,
				CategoryId = carViewModel.CategoryId,
				EngineId = carViewModel.EngineTypeId,
				SellerId = Guid.Parse(sellerId)
			};

			await dbContext.Cars.AddAsync(car);
			await dbContext.SaveChangesAsync();
		}

		public async Task EditCarByIdAndFormModel(string carId, CarFormModel formModel)
		{
			Car car = await dbContext
				.Cars
				.Where(c => c.IsActive)
				.FirstAsync(c => c.Id.ToString() == carId);

			car.Make = formModel.Make;
			car.Model = formModel.Model;
			car.Price = formModel.Price;
			car.Year = formModel.Year;
			car.Horsepower = formModel.Horsepower;
			car.Mileage = formModel.Mileage;
			car.Description = formModel.Description;
			car.ImageUrl = formModel.ImageUrl;
			car.CategoryId = formModel.CategoryId;
			car.TransmissionId = formModel.TransmissionId;
			car.EngineId = formModel.EngineTypeId;

			await dbContext.SaveChangesAsync();
		}

		public async Task<bool> ExistsByIdAsync(string carId)
		{
			var result = await dbContext
				.Cars
				.AnyAsync(c => c.Id.ToString() == carId);

			return result;
		}

		public async Task<CarFormModel> GetCarForEditByCarIdAsync(string carId)
		{
			Car car = await dbContext
			   .Cars
			   .Include(c => c.Category)
			   .Include(c => c.Transmission)
			   .Include(c => c.EngineType)
			   .Where(c => c.IsActive)
			   .FirstAsync(c => c.Id.ToString() == carId);

			return new CarFormModel
			{
				Make = car.Make,
				Model = car.Model,
				Price = car.Price,
				Year = car.Year,
				Horsepower = car.Horsepower,
				Mileage = car.Mileage,
				Description = car.Description,
				ImageUrl = car.ImageUrl,
				CategoryId = car.CategoryId,
				TransmissionId = car.TransmissionId,
				EngineTypeId = car.EngineId
			};
		}

		public async Task<CarDetailsViewModel> GetDetailsByIdAsync(string carId)
		{
			Car? car = await dbContext
				.Cars
				.Include(c => c.Category)
				.Include(c => c.Transmission)
				.Include(c => c.EngineType)
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
				Transmission = car.Transmission.Name,
				EngineType = car.EngineType.Type,
				Description = car.Description,
				Seller = new SellerInfoOnCarViewModel
				{
					Email = car.Seller.User.Email,
					PhoneNumber = car.Seller.PhoneNumber
				}
			};
		}

		public async Task<CarDeleteDetailsViewModel> GetCarForDeletByIdAsync(string carId)
		{
			Car car = await dbContext
				.Cars
				.Where(c => c.IsActive)
				.FirstAsync(c => c.Id.ToString() == carId);

			return new CarDeleteDetailsViewModel
			{
				Make = car.Make,
				Model = car.Model,
				Price = car.Price,
				ImageUrl = car.ImageUrl
			};
		}

		public async Task<bool> IsSellerWithIdOwnerOfCarWithIdAsync(string carId, string sellerId)
		{
			Car car = await dbContext
				.Cars
				.Where(c => c.IsActive)
				.FirstAsync(c => c.Id.ToString() == carId);

			return car.SellerId.ToString() == sellerId;
		}

		public async Task DeleteCarByIdAsync(string carId)
		{
			Car car = await dbContext
				.Cars
				.Where(c => c.IsActive)
				.FirstAsync(c => c.Id.ToString() == carId);

			car.IsActive = false;

			await this.dbContext.SaveChangesAsync();
		}

		public async Task<bool> IsForSaleByIdAsync(string carId)
		{
			Car car = await dbContext
				.Cars
				.FirstAsync(c => c.Id.ToString() == carId);

			return car.IsForSale;
		}

		public async Task BuyCarAsync(string carId, string userId)
		{
			Car car = await dbContext
				.Cars
				.FirstAsync(c => c.Id.ToString() == carId);

			ApplicationUser user = await dbContext
				.Users
				.FirstAsync(u => u.Id.ToString() == userId);

			car.IsForSale = false;
			user.OwnedCars.Add(car);

			dbContext.SaveChanges();
		}
	}
}
