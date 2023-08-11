namespace AutoTrade.Services.Data.Interfaces
{
	using AutoTrade.Web.ViewModels.User;

	public interface IUserService
	{
		Task<string> GetFullNameByEmailAsync(string email);

		Task<bool> HasWalletByIdAsync(string id);

		Task SetWalletIdAsync(string id, string walletId);

		Task<string> GetFullNameByIdAsync(string id);

		Task<IEnumerable<UserViewModel>> AllAsync();

		Task<string> GetBalanceByIdAsync(string id);
	}
}
