namespace AutoTrade.Services.Data
{
    using AutoTrade.Web.ViewModels.CreditCard;
    using Services.Data.Interfaces;
    using System.Threading.Tasks;

    public class CreditCardService : ICreditCardService
    {
        public Task AddCreditCardByIdAndWalletIdAsync(CreditCardFormModel formModel, string walletId)
        {
            throw new NotImplementedException();
        }
    }
}
