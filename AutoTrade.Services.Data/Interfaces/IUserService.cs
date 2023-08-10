namespace AutoTrade.Services.Data.Interfaces
{
	public interface IUserService
	{
		Task<string> GetFullNameByEmailAsync(string email);

		Task<bool> HasWalletByIdAsync(string id);
	}
}
