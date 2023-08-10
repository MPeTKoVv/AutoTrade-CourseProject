namespace AutoTrade.Services.Data.Interfaces
{
	using Web.ViewModels.CreditCard;

	public interface ICreditCardService
    {
        Task<string> CreateAndReturnIdAsync(CreditCardFormModel formModel, string walletId);

		Task<CreditCardViewModel> GetCreditCardByIdAsync(string creditCardId);

		Task DeleteByIdAsync(string id);

		Task<bool> CardBelongsToUserByIdAsync(string id, string userId);

		Task<decimal> WithdrawByIdAsync(WithdrawMoneyFormModel formModel, string id);
    }
}
