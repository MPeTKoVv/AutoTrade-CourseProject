namespace AutoTrade.Services.Data.Interfaces
{
	using Web.ViewModels.CreditCard;

	public interface ICreditCardService
    {
        Task<string> CreateAndReturnIdAsync(CreditCardFormModel formModel, string walletId);

		Task<CreditCardViewModel> GetCreditCardByIdAsync(string creditCardId);

		Task DeleteByIdAsync(string id);
	}
}
