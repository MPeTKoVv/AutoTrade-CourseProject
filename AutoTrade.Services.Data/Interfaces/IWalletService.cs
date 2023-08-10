namespace AutoTrade.Services.Data.Interfaces
{
	public interface IWalletService
	{
		Task<string> CreateAndReturnIdAsync(string userId);

		Task<string?> GetIdByUserIdAsync(string userId);
	}
}
