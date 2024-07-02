namespace AutoTrade.Services.Tests
{
	using Microsoft.EntityFrameworkCore;

	using Data;
	using Data.Interfaces;
	using AutoTrade.Web.Data;

	using static DatabaseSeeder;

	public class CarServiceTests
	{
		private DbContextOptions<AutoTradeDbContext> dbOptions;
		private AutoTradeDbContext dbContext;

		private ICarService carService;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			this.dbOptions = new DbContextOptionsBuilder<AutoTradeDbContext>()
				.UseInMemoryDatabase("AutoTradeInMemory" + Guid.NewGuid().ToString())
				.Options;

			this.dbContext = new AutoTradeDbContext(this.dbOptions);

			dbContext.Database.EnsureCreated();
			SeedDatabase(this.dbContext);

			this.carService = new CarService(this.dbContext);
		}

		//[Test]
		//public async Task ExistsByIdAsyncShouldReturnTrue()
		//{
		//	string existingCarId = Car.Id.ToString();

		//	bool result = await this.carService.ExistsByIdAsync(existingCarId);

		//	Assert.IsTrue(result);
		//}

		[Test]
		public async Task ExistsByIdAsyncShouldReturnFalse()
		{
			string notExistingCarId = string.Format(Car.Id.ToString(), "1");

			bool result = await this.carService.ExistsByIdAsync(notExistingCarId);

			Assert.IsFalse(result);
		}

		//[Test]
		//public async Task IsUserWithIdOwnerOfCarWithIdAsyncShouldReturnTrue()
		//{
		//	string carId = Car.Id.ToString();
		//	string userId = CarSellerUser.Id.ToString();

		//	bool result = await carService.IsUserWithIdOwnerOfCarWithIdAsync(carId, userId);

		//	Assert.IsTrue(result);
		//}

		//[Test]
		//public async Task IsUserWithIdOwnerOfCarWithIdAsyncShouldReturnFalse()
		//{
		//	string carId = Car.Id.ToString();
		//	string userId = BuyerUser.Id.ToString();

		//	bool result = await carService.IsUserWithIdOwnerOfCarWithIdAsync(carId, userId);

		//	Assert.IsFalse(result);
		//}

		[Test]
		public async Task IsForSaleByIdAsyncShouldReturnTrue()
		{
			string carId = Car.Id.ToString();
			Car.IsForSale = true;
			bool result = await carService.IsForSaleByIdAsync(carId);

			Assert.IsTrue(result);
		}

		[Test]
		public async Task IsForSaleByIdAsyncShouldReturnFalse()
		{
			string carId = Car.Id.ToString();
			Car.IsForSale = false;
			bool result = await carService.IsForSaleByIdAsync(carId);

			Assert.IsFalse(result);
		}

		[Test]
		public async Task GetOwnerIdAsyncShouldReturnTrue()
		{
			string expected = Car.OwnerId.ToString();
			string carId = Car.Id.ToString();
			string actual = await carService.GetOwnerIdAsync(carId);

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public async Task GetPriceByIdAsyncShouldReturnTrue()
		{
			decimal expected = Car.Price;
			string carId = Car.Id.ToString();
			decimal actual = await carService.GetPriceByIdAsync(carId);

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public async Task GetSellerIdByCarIdAsyncShouldReturnTrue()
		{
			string expected = Car.SellerId.ToString();
			string carId = Car.Id.ToString();
			string actual = await carService.GetSellerIdByCarIdAsync(carId);

			Assert.AreEqual(expected, actual);
		}
	}
}
