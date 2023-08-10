namespace AutoTrade.Services.Data
{
    using AutoTrade.Web.Data;
    using AutoTrade.Data.Models;
    using Services.Data.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using AutoTrade.Web.ViewModels.Wallet;
    using System.Diagnostics.Metrics;

    public class WalletService : IWalletService
    {
        private readonly AutoTradeDbContext dbContext;

        public WalletService(AutoTradeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> CreateAndReturnIdAsync(string userId)
        {
            Wallet wallet = new Wallet();
            wallet.UserId = Guid.Parse(userId);

            await dbContext.Wallets.AddAsync(wallet);
            await dbContext.SaveChangesAsync();

            string walletId = wallet.Id.ToString();
            return walletId;
        }

        public async Task<string?> GetIdByUserIdAsync(string userId)
        {
            ApplicationUser user = await dbContext
                 .Users
                 .FirstAsync(u => u.Id.ToString() == userId);

            string? walletId = user.WalletId.ToString();

            return walletId;
        }

        public async Task<WalletOverviewViewModel> GetWalletOverviewByUserIdAsync(string userId)
        {
            Wallet wallet = await dbContext
                .Wallets
                .Include(w=>w.User)
                .FirstAsync(w => w.UserId.ToString() == userId);

            WalletOverviewViewModel viewModel = new WalletOverviewViewModel
            {
                WalletId = wallet.Id.ToString(),
                UserId = wallet.UserId.ToString(),
                UserName = $"{wallet.User.FirstName} {wallet.User.LastName}",
                Balance = wallet.Balance,
                CreditCardId = wallet.CreditCardId.ToString(),
            };

            return viewModel;
        }
    }
}
