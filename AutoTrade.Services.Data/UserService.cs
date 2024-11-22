namespace AutoTrade.Services.Data
{
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    using AutoTrade.Data.Models;
    using Interfaces;
    using Web.Data;
    using Web.ViewModels.User;
    using System.Diagnostics.CodeAnalysis;

    public class UserService : IUserService
    {
        private readonly AutoTradeDbContext dbContext;

        public UserService(AutoTradeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<UserViewModel>> AllAsync()
        {
            List<UserViewModel> allUsers = await dbContext
                .Users
                .Select(u => new UserViewModel
                {
                    Id = u.Id.ToString(),
                    Email = u.Email,
                    FullName = u.FirstName + " " + u.LastName
                })
                .ToListAsync();

            foreach (var user in allUsers)
            {
                Seller? seller = dbContext
                    .Sellers
                    .FirstOrDefault(s => s.UserId.ToString() == user.Id);
                if (seller != null)
                {
                    user.PhoneNumber = seller.PhoneNumber;
                }
                else
                {
                    user.PhoneNumber = string.Empty;
                }
            }

            return allUsers;
        }

        public async Task<string> GetBalanceByIdAsync(string id)
        {
            ApplicationUser? user = await dbContext
                .Users
                .Include(u => u.Wallet)
                .FirstOrDefaultAsync(u => u.Id.ToString() == id);

            if (user == null)
            {
                return string.Empty;
            }

            if (user.Wallet != null)
            {
                string balance = user.Wallet.Balance.ToString("N2");
                return balance;
            }
            else
            {
                return "";
            }

        }

        public async Task<string> GetFullNameByEmailAsync(string email)
        {
            ApplicationUser? user = await this.dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return string.Empty;
            }

            return $"{user.FirstName} {user.LastName}";
        }

        public async Task<string> GetFullNameByIdAsync(string id)
        {
            ApplicationUser? user = await dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Id.ToString() == id);

            if (user == null)
            {
                return string.Empty;
            }

            string fullName = $"{user.FirstName} {user.LastName}";

            return fullName;
        }

        public async Task<bool> HasWalletByIdAsync(string id)
        {
            ApplicationUser? user = await dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Id.ToString() == id);

            if (user == null)
            {
                return false;
            }

            bool result = user.WalletId.HasValue;

            return result;
        }

        public async Task SetWalletIdAsync(string id, string walletId)
        {
            ApplicationUser user = await dbContext
                .Users
                .FirstAsync(u => u.Id.ToString() == id);

            user.WalletId = Guid.Parse(walletId);
            await dbContext.SaveChangesAsync();
        }
    }
}
