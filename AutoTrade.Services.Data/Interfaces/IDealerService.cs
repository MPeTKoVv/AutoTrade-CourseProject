using AutoTrade.Web.ViewModels.Dealer;

namespace AutoTrade.Services.Data.Interfaces
{
    public interface IDealerService
    {
        Task<bool> DealerExistsByUserIdAsync(string userId);

        Task<bool> DealerExistsByPhoneNumberAsync(string userId);

		Task Create(string userId, BecomeDealerViewModel model);

        Task<string> GetDealerIdByUserIdAsync(string userId);
	}
}
