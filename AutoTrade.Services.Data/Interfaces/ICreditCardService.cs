namespace AutoTrade.Services.Data.Interfaces
{
    public interface ICreditCardService
    {
        Task AddCreditCardByIdAndWalletIdAsync(string id, string walletId);
    }
}
