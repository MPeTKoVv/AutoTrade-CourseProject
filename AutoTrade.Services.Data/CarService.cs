namespace AutoTrade.Services.Data
{
	using Microsoft.EntityFrameworkCore;

	using Mapping;
	using AutoTrade.Data.Models;
	using Interfaces;
	using Models.Car;
	using Models.Statistics;
	using Web.Data;
	using Web.ViewModels.Car;
	using Web.ViewModels.Car.Enums;
	using Web.ViewModels.Home;
	using Web.ViewModels.Seller;

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
				.Where(c => c.IsActive && c.IsForSale)
				.OrderByDescending(c => c.AddedOnForSale)
				.Take(5)
				.To<IndexViewModel>()
				.ToArrayAsync();

			return orderedCars;
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
					.OrderByDescending(c => c.AddedOnForSale),
				CarSorting.Oldest => carsQuery
					.OrderBy(c => c.AddedOnForSale),
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
				.To<CarAllViewModel>()
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
			IEnumerable<CarAllViewModel> usersCars = await dbContext
				.Cars
				.Where(c => c.IsActive && !c.IsForSale && c.OwnerId.ToString() == userId)
				.OrderBy(c => c.Make)
				.ThenBy(c => c.Model)
				.To<CarAllViewModel>()
				.ToListAsync();

			return usersCars;
		}

		public async Task<IEnumerable<CarAllViewModel>> AllCarsForSaleBySellerIdAsync(string sellerId)
		{
			IEnumerable<CarAllViewModel> carsForSale = await dbContext
				.Cars
				.Where(c => c.IsActive && c.IsForSale && c.SellerId.ToString() == sellerId)
				.OrderByDescending(c => c.AddedOnForSale)
				.To<CarAllViewModel>()
				.ToListAsync();

			return carsForSale;
		}

		public async Task CreateAndReturnIdAsync(CarFormModel formModel, string sellerId)
		{
			Car newCar = AutoMapperConfig.MapperInstance.Map<Car>(formModel);
			newCar.AddedOn = DateTime.UtcNow;
			newCar.SellerId = Guid.Parse(sellerId);

			await dbContext.Cars.AddAsync(newCar);
			await dbContext.SaveChangesAsync();
		}

		public async Task EditCarByIdAndFormModel(string carId, CarFormModel formModel)
		{
			Car car = await dbContext
				.Cars
				.Where(c => c.IsActive)
				.FirstAsync(c => c.Id.ToString() == carId);

			car = AutoMapperConfig.MapperInstance.Map<Car>(formModel);

			await dbContext.SaveChangesAsync();
		}

		public async Task<bool> ExistsByIdAsync(string carId)
		{
			var result = await dbContext
				.Cars
				.Where(c => c.IsActive)
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

			CarFormModel formModel = AutoMapperConfig.MapperInstance.Map<CarFormModel>(car);

			return formModel;
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
				.Include(c => c.Owner)
				.Where(c => c.IsActive)
				.FirstOrDefaultAsync(c => c.Id.ToString() == carId);

			if (car == null)
			{
				return null;
			}

			CarDetailsViewModel viewModel = new CarDetailsViewModel
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
				IsForSale = car.IsForSale,
				Seller = new SellerInfoOnCarViewModel
				{
					FullName = $"{car.Owner.FirstName} {car.Owner.LastName}",
					Email = car.Owner.Email,
					PhoneNumber = car.Seller?.PhoneNumber
				}
			};

			return viewModel;
		}

		public async Task<CarDeleteDetailsViewModel> GetCarForDeletByIdAsync(string carId)
		{
			Car car = await dbContext
				.Cars
				.Where(c => c.IsActive)
				.FirstAsync(c => c.Id.ToString() == carId);

			CarDeleteDetailsViewModel viewModel = AutoMapperConfig.MapperInstance.Map<CarDeleteDetailsViewModel>(car);

			return viewModel;
		}

		public async Task<bool> IsUserWithIdOwnerOfCarWithIdAsync(string carId, string ownerId)
		{
			Car car = await dbContext
				.Cars
				.Where(c => c.IsActive)
				.FirstAsync(c => c.Id.ToString() == carId);

			return car.OwnerId.ToString() == ownerId;
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
			car.SellerId = null;
			car.OwnerId = Guid.Parse(userId);
			user.OwnedCars.Add(car);

			await dbContext.SaveChangesAsync();
		}

		public async Task CarForSaleAsync(string carId, string sellerId)
		{
			Car car = await dbContext
				.Cars
				.FirstAsync(c => c.Id.ToString() == carId);

			car.IsForSale = true;
			car.SellerId = Guid.Parse(sellerId);
			car.AddedOnForSale = DateTime.UtcNow;

			dbContext.SaveChanges();
		}

		public async Task<StatisticsServiceModel> GetStatisticsAsync()
		{
			return new StatisticsServiceModel
			{
				TotalCars = await dbContext.Cars.CountAsync(),
				TotalSales = await dbContext.Transactions.CountAsync(),
				TotalUsers = await dbContext.Users.CountAsync(),
				TotalSellers = await dbContext.Sellers.CountAsync()
			};
		}

		public async Task ReturnCarToGarageAsync(string id)
		{
			Car car = await dbContext
				.Cars
				.FirstAsync(c => c.Id.ToString() == id);

			car.IsForSale = false;
			car.SellerId = null;

			dbContext.SaveChanges();
		}

		public async Task<string> GetOwnerIdAsync(string carId)
		{
			Car car = await dbContext
				.Cars
				.FirstAsync(c => c.Id.ToString() == carId);

			string ownerId = car.OwnerId.ToString()!;

			return ownerId;
		}

		public async Task<decimal> GetPriceByIdAsync(string id)
		{
			Car car = await dbContext
				.Cars
				.FirstAsync(c => c.Id.ToString() == id);

			decimal price = car.Price;

			return price;
		}

		public async Task<string> GetSellerIdByCarIdAsync(string id)
		{
			Car car = await dbContext
				.Cars
				.FirstAsync(c => c.Id.ToString() == id);

			string sellerId = car.SellerId.ToString()!;

			return sellerId;
		}
	}
}
