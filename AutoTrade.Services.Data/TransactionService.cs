using AutoTrade.Data.Models;
using AutoTrade.Services.Data.Interfaces;
using AutoTrade.Web.Data;
using AutoTrade.Web.ViewModels.Transaction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrade.Services.Data
{
	public class TransactionService : ITransactionService
	{
		private readonly AutoTradeDbContext dbContext;

		public TransactionService(AutoTradeDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public Task RecordTransaction(string carId, string buyerId, string sellerId, decimal amout)
		{
			//Transaction transaction = new Transaction
			//{
			//	CarId = Guid.Parse(carId),
			//	BuyerId = Guid.Parse(buyerId),

			//}

			return Task.CompletedTask;
		}
	}
}
