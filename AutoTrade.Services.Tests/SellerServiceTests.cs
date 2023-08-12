namespace AutoTrade.Services.Tests
{
	using Microsoft.EntityFrameworkCore;
	
	using AutoTrade.Web.Data;

	using static DatabaseSeeder;
	using AutoTrade.Services.Data.Interfaces;
	using NUnit.Framework.Constraints;
	using AutoTrade.Services.Data;

	public class SellerServiceTests
	{
		private DbContextOptions<AutoTradeDbContext> dbOptions;
		private AutoTradeDbContext dbContext;

		private ISellerService sellerService;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			this.dbOptions = new DbContextOptionsBuilder<AutoTradeDbContext>()
				.UseInMemoryDatabase("AutoTradeInMemory" + Guid.NewGuid().ToString())
				.Options;

			this.dbContext = new AutoTradeDbContext(this.dbOptions);

			dbContext.Database.EnsureCreated();
			SeedDatabase(this.dbContext);

			this.sellerService = new SellerService(this.dbContext);
		}

		[Test]
		public async Task SellerExistsByUserIdAsyncShoudReturnTrueWhenExists()
		{
			string existingSellerUserId = SellerUser.Id.ToString();

			bool result = await this.sellerService.SellerExistsByUserIdAsync(existingSellerUserId);

			Assert.IsTrue(result);
		}

		[Test]
		public async Task SellerExistsByUserIdAsyncShoudReturnFalseWhenNotExists()
		{
			string existingSellerUserId = BuyerUser.Id.ToString();

			bool result = await this.sellerService.SellerExistsByUserIdAsync(existingSellerUserId);

			Assert.IsFalse(result);
		}

	}
}