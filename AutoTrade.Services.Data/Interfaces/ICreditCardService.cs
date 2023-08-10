using AutoTrade.Web.ViewModels.CreditCard;

namespace AutoTrade.Services.Data.Interfaces
{
    public interface ICreditCardService
    {
        Task AddCreditCardByIdAndWalletIdAsync(CreditCardFormModel formModel, string walletId);
    }
}
