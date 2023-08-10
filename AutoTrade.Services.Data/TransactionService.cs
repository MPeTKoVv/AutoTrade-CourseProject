namespace AutoTrade.Services.Data
{
	using Microsoft.EntityFrameworkCore;

	using AutoTrade.Data.Models;
	using Interfaces;
	using Web.Data;
	using System.Collections.Generic;
	using AutoTrade.Web.ViewModels.Transaction;

	public class TransactionService : ITransactionService
	{
		private readonly AutoTradeDbContext dbContext;

		public TransactionService(AutoTradeDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<IEnumerable<TransactionAllViewModel>?> GetTransactionHistoryByUserIdAsync(string userId)
		{
			IEnumerable<TransactionAllViewModel> allTransactions = await dbContext
				.Transactions
				.OrderByDescending(t => t.TransactionDate)
				.Include(t => t.Buyer)
				.Include(t => t.Seller)
				.ThenInclude(s => s.User)
				.Where(t => t.BuyerId.ToString() == userId || t.Seller.UserId.ToString() == userId)
				.Select(t => new TransactionAllViewModel
				{
					Id = t.Id.ToString(),
					Amount = t.Amount,
					Date = t.TransactionDate,
					BuyerId = t.BuyerId.ToString(),
					BuyerFullName = $"{t.Buyer.FirstName} {t.Buyer.LastName}",
					SellerId = t.SellerId.ToString(),
					SellerFullName = $"{t.Seller.User.FirstName} {t.Seller.User.LastName}"
				})
				.ToListAsync();

			return allTransactions;
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
				BuyerId = buyer.Id,
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
