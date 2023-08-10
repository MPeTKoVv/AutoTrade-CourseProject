namespace AutoTrade.Services.Data.Interfaces
{
	using Web.ViewModels.Wallet;
	public interface IWalletService
	{
		Task<string> CreateAndReturnIdAsync(string userId);

		Task<string?> GetIdByUserIdAsync(string userId);

		Task<WalletOverviewViewModel> GetWalletOverviewByUserIdAsync(string userId);

		Task<bool> HasCreditCardByIdAsync(string id);

		Task AddCreditCardByIdAndWalletIdAsync(string walletId, string creditCardId);

		Task DeleteCreditCardByIdAsync(string creditCardId);
	}
}
