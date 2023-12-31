namespace AutoTrade.Services.Tests
{
	using Microsoft.EntityFrameworkCore;
	
	using Data;
	using Data.Interfaces;
	using AutoTrade.Web.Data;

	using static DatabaseSeeder;

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

		[Test]
		public async Task SellerExistsByPhoneNumberAsyncShoudReturnTrueWhenExists()
		{
			string phoneNumebr = Seller.PhoneNumber;

			bool result = await this.sellerService.SellerExistsByPhoneNumberAsync(phoneNumebr);

			Assert.IsTrue(result);
		}

		[Test]
		public async Task SellerExistsByPhoneNumberAsyncShoudReturnFalseWhenNotExists()
		{
			string phoneNumebr = "+359999999999";

			bool result = await this.sellerService.SellerExistsByUserIdAsync(phoneNumebr);

			Assert.IsFalse(result);
		}

		[Test]
		public async Task GetSellerIdByUserIdAsyncShoudReturnTrueWhenExists()
		{
			string expected = Seller.Id.ToString();
			
			string sellerUserId = SellerUser.Id.ToString();
			string actual = await this.sellerService.GetSellerIdByUserIdAsync(sellerUserId);

			Assert.AreEqual(expected, actual);
		}
	}
}