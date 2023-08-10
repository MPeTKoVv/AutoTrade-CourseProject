using AutoTrade.Web.ViewModels.Wallet;

namespace AutoTrade.Services.Data.Interfaces
{
	public interface IWalletService
	{
		Task<string> CreateAndReturnIdAsync(string userId);

		Task<string?> GetIdByUserIdAsync(string userId);

		Task<WalletOverviewViewModel> GetWalletOverviewByUserIdAsync(string userId);
	}
}
