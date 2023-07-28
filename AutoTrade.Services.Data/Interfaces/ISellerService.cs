using AutoTrade.Web.ViewModels.Seller;

namespace AutoTrade.Services.Data.Interfaces
{
    public interface ISellerService
    {
        Task<bool> SellerExistsByUserIdAsync(string userId);

        Task<bool> SellerExistsByPhoneNumberAsync(string userId);

		Task Create(string userId, BecomeSellerViewModel model);
	}
}
