namespace AutoTrade.Services.Data
{
	using AutoTrade.Web.Data;
	using AutoTrade.Data.Models;
	using Services.Data.Interfaces;

	public class WalletService : IWalletService
	{
		private readonly AutoTradeDbContext dbContext;

		public WalletService(AutoTradeDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<string> CreateAndReturnIdAsync(string userId)
		{
			Wallet wallet = new Wallet();
			wallet.UserId = Guid.Parse(userId);

			await dbContext.Wallets.AddAsync(wallet);

			string walletId = wallet.Id.ToString();
			return walletId;
		}
	}
}
