namespace AutoTrade.Services.Data.Interfaces
{
	using Web.ViewModels.Seller;

	public interface ISellerService
	{
		Task<bool> SellerExistsByUserIdAsync(string userId);

		Task<bool> SellerExistsByPhoneNumberAsync(string userId);

		Task Create(string userId, BecomeSellerViewModel model);

		Task<string> GetSellerIdByUserIdAsync(string userId);
	}
}
