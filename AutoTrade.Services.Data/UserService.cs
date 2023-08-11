namespace AutoTrade.Services.Data
{
	using System.Collections.Generic;

	using Microsoft.EntityFrameworkCore;

	using AutoTrade.Data.Models;
	using Interfaces;
	using Web.Data;
	using Web.ViewModels.User;
	using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

	public class UserService : IUserService
	{
		private readonly AutoTradeDbContext dbContext;

		public UserService(AutoTradeDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public Task<IEnumerable<UserViewModel>> AllAsync()
		{
			throw new NotImplementedException();
		}

		public async Task<string> GetBalanceByIdAsync(string id)
		{
			ApplicationUser? user = await dbContext
				.Users
				.Include(u => u.Wallet)
				.FirstOrDefaultAsync(u => u.Id.ToString() == id);

			if (user == null)
			{
				return string.Empty;
			}

			string balance = user.Wallet.Balance.ToString("N2");

			return balance;
		}

		public async Task<string> GetFullNameByEmailAsync(string email)
		{
			ApplicationUser? user = await this.dbContext
				.Users
				.FirstOrDefaultAsync(u => u.Email == email);

			if (user == null)
			{
				return string.Empty;
			}

			return $"{user.FirstName} {user.LastName}";
		}

		public async Task<string> GetFullNameByIdAsync(string id)
		{
			ApplicationUser? user = await dbContext
				.Users
				.FirstOrDefaultAsync(u => u.Id.ToString() == id);

			if (user == null)
			{
				return string.Empty;
			}

			string fullName = $"{user.FirstName} {user.LastName}";

			return fullName;
		}

		public async Task<bool> HasWalletByIdAsync(string id)
		{
			ApplicationUser? user = await dbContext
				.Users
				.FirstOrDefaultAsync(u => u.Id.ToString() == id);

			if (user == null)
			{
				return false;
			}

			bool result = user.WalletId.HasValue;

			return result;
		}

		public async Task SetWalletIdAsync(string id, string walletId)
		{
			ApplicationUser user = await dbContext
				.Users
				.FirstAsync(u => u.Id.ToString() == id);

			user.WalletId = Guid.Parse(walletId);
			await dbContext.SaveChangesAsync();
		}
	}
}
