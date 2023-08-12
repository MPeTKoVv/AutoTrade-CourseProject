namespace AutoTrade.Services.Data
{
	using System.Collections.Generic;

	using Microsoft.EntityFrameworkCore;

	using Interfaces;
	using AutoTrade.Data.Models;
	using Web.Data;
	using Web.ViewModels.Transaction;
	using AutoTrade.Services.Mapping;

	public class TransactionService : ITransactionService
	{
		private readonly AutoTradeDbContext dbContext;

		public TransactionService(AutoTradeDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<IEnumerable<TransactionViewModel>> AllAsync()
		{
			IEnumerable<TransactionViewModel> transactions = await dbContext
				.Transactions
				.OrderByDescending(t => t.TransactionDate)
				.Include(t => t.Seller)
				.ThenInclude(s => s.User)
				.Include(t => t.Buyer)
				.To<TransactionViewModel>()
				.ToListAsync();

			return transactions;
		}

		public async Task<IEnumerable<TransactionSoldAndBoughtCarsViewModel>> GetBoughtCarsByUserIdAsync(string userId)
		{
			IQueryable<Transaction> carsQuery = this.dbContext
				.Transactions
				.Where(t => t.BuyerId.ToString() == userId)
				.AsQueryable();

			IEnumerable<TransactionSoldAndBoughtCarsViewModel> boughtCars = await carsQuery
				.OrderByDescending(t => t.TransactionDate)
				.Include(t => t.Car)
				.Select(t => new TransactionSoldAndBoughtCarsViewModel
				{
					CarMake = t.Car.Make,
					CarModel = t.Car.Model,
					BuyerFullName = $"{t.Buyer.FirstName} {t.Buyer.LastName}",
					SellerFullName = $"{t.Seller.User.FirstName} {t.Seller.User.LastName}",
					Amount = t.Amount,
					Date = t.TransactionDate
				})
				.ToListAsync();

			return boughtCars;
		}

		public async Task<IEnumerable<TransactionSoldAndBoughtCarsViewModel>> GetSoldCarsByUserIdAsync(string userId)
		{
			IQueryable<Transaction> carsQuery = this.dbContext
				.Transactions
				.Where(t => t.Seller.UserId.ToString() == userId)
				.AsQueryable();

			IEnumerable<TransactionSoldAndBoughtCarsViewModel> soldCars = await carsQuery
				.OrderByDescending(t => t.TransactionDate)
				.Include(t => t.Car)
				.Select(t => new TransactionSoldAndBoughtCarsViewModel
				{
					CarMake = t.Car.Make,
					CarModel = t.Car.Model,
					BuyerFullName = $"{t.Buyer.FirstName} {t.Buyer.LastName}",
					Amount = t.Amount,
					Date = t.TransactionDate
				})
				.ToListAsync();

			return soldCars;
		}

		public async Task<IEnumerable<TransactionViewModel>?> GetTransactionHistoryByUserIdAsync(string userId)
		{
			IEnumerable<TransactionViewModel> allTransactions = await dbContext
				.Transactions
				.OrderByDescending(t => t.TransactionDate)
				.Include(t => t.Buyer)
				.Include(t => t.Seller)
				.ThenInclude(s => s.User)
				.Where(t => t.BuyerId.ToString() == userId || t.Seller.UserId.ToString() == userId)
				.Select(t => new TransactionViewModel
				{
					Id = t.Id.ToString(),
					Amount = t.Amount,
					Date = t.TransactionDate,
					BuyerId = t.BuyerId.ToString().ToLower(),
					BuyerFullName = $"{t.Buyer.FirstName} {t.Buyer.LastName}",
					SellerId = t.SellerId.ToString().ToLower(),
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
