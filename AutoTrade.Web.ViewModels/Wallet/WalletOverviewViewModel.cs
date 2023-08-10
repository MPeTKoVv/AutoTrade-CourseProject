namespace AutoTrade.Web.ViewModels.Wallet
{
    using Transaction;

    public class WalletOverviewViewModel
    {
        public string WalletId { get; set; } = null!;

        public string UserId { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public decimal Balance { get; set; }

        public string? CreditCardId { get; set; }
        public IEnumerable<TransactionAllViewModel> TransactionHistory { get; set; } = null!;
    }
}
