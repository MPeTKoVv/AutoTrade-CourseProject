using AutoTrade.Data.Models;
using AutoTrade.Web.ViewModels.Wallet;

namespace AutoTrade.Services.Data.Interfaces
{
	public interface IWalletService
	{
		Task<string> CreateAndReturnIdAsync(string userId);

		Task<string?> GetIdByUserIdAsync(string userId);

		Task<WalletOverviewViewModel> GetWalletOverviewByUserIdAsync(string userId);

		Task<bool> HasCreditCardByIdAsync(string id);

		Task AddCreditCardByIdAndWalletIdAsync(string walletId, string creditCardId);
	}
}
