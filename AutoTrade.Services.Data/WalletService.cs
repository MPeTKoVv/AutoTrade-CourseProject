namespace AutoTrade.Services.Data
{
	using Microsoft.EntityFrameworkCore;

	using AutoTrade.Data.Models;
	using Interfaces;
	using Web.Data;
	using Web.ViewModels.Wallet;

	public class WalletService : IWalletService
	{
		private readonly AutoTradeDbContext dbContext;

		public WalletService(AutoTradeDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task AddCreditCardByIdAndWalletIdAsync(string walletId, string creditCardId)
		{
			Wallet wallet = await dbContext
				.Wallets
				.FirstAsync(w => w.Id.ToString() == walletId);

			wallet.CreditCardId = Guid.Parse(creditCardId);

			await dbContext.SaveChangesAsync();
		}

		public async Task<string> CreateAndReturnIdAsync(string userId)
		{
			Wallet wallet = new Wallet();
			wallet.UserId = Guid.Parse(userId);

			await dbContext.Wallets.AddAsync(wallet);
			await dbContext.SaveChangesAsync();

			string walletId = wallet.Id.ToString();
			return walletId;
		}

		public async Task DeleteCreditCardByIdAsync(string id)
		{
			Wallet wallet = await dbContext
				.Wallets
				.FirstAsync(w => w.Id.ToString() == id);

			wallet.CreditCardId = null;
			await dbContext.SaveChangesAsync();
		}

		public async Task<decimal> GetBalanceByUserIdAsync(string userId)
		{
			Wallet wallet = await dbContext
				.Wallets
				.FirstAsync(w => w.UserId.ToString() == userId);

			decimal balance = wallet.Balance;

			return balance;
		}

		public async Task<string?> GetIdByUserIdAsync(string userId)
		{
			ApplicationUser user = await dbContext
				 .Users
				 .FirstAsync(u => u.Id.ToString() == userId);

			if (user == null)
			{
				return null;
			}

			string walletId = user.WalletId.ToString()!;

			return walletId;
		}

		public async Task<WalletOverviewViewModel> GetWalletOverviewByUserIdAsync(string userId)
		{
			Wallet wallet = await dbContext
				.Wallets
				.Include(w => w.User)
				.FirstAsync(w => w.UserId.ToString() == userId);

			WalletOverviewViewModel viewModel = new WalletOverviewViewModel
			{
				WalletId = wallet.Id.ToString(),
				UserId = wallet.UserId.ToString(),
				UserName = $"{wallet.User.FirstName} {wallet.User.LastName}",
				Balance = wallet.Balance,
				CreditCardId = wallet.CreditCardId.ToString(),
			};

			return viewModel;
		}

		public async Task<bool> HasCreditCardByIdAsync(string id)
		{
			Wallet wallet = await dbContext
				.Wallets
				.FirstAsync(w => w.Id.ToString() == id);

			bool result = wallet.CreditCardId.HasValue;

			return result;
		}

		public async Task IncreaseBalance(string userId, decimal carPrice)
		{
			ApplicationUser user = await dbContext
				.Users
				.Include(u=>u.Wallet)
				.FirstAsync(u => u.Id.ToString() == userId);

			user.Wallet.Balance += carPrice;
			await dbContext.SaveChangesAsync();
		}

		public async Task ReduceBalance(string userId, decimal carPrice)
		{
			ApplicationUser user = await dbContext
				.Users
				.Include(u => u.Wallet)
				.FirstAsync(u => u.Id.ToString() == userId);

			user.Wallet.Balance -= carPrice;
			await dbContext.SaveChangesAsync();
		}
	}
}
