namespace AutoTrade.Services.Data
{
	using Microsoft.EntityFrameworkCore;

	using AutoTrade.Data.Models;
	using Interfaces;
	using Web.Data;

	public class TransactionService : ITransactionService
	{
		private readonly AutoTradeDbContext dbContext;

		public TransactionService(AutoTradeDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task RecordTransaction(string carId, string buyerId, string sellerId)
		{
			Car car = await dbContext
				.Cars
				.FirstAsync(c => c.Id.ToString() == carId);

			ApplicationUser buyer = await dbContext
				.Users
				.FirstAsync(b => b.Id.ToString() == buyerId);

			Seller seller = await dbContext
				.Sellers
				.FirstAsync(s => s.Id.ToString() == sellerId);

			Transaction transaction = new Transaction
			{
				CarId = car.Id,
				BuyerId =buyer.Id,
				SellerId = seller.Id,
				Amount = car.Price,
				TransactionDate = DateTime.Now
			};

			buyer.BoughtCarsHistory.Add(transaction);
			seller.SoldCarHistory.Add(transaction);

			await dbContext.Transactions.AddAsync(transaction);
			await dbContext.SaveChangesAsync();
		}
	}
}
