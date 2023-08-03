using AutoTrade.Data.Models;
using AutoTrade.Services.Data.Interfaces;
using AutoTrade.Web.Data;
using AutoTrade.Web.ViewModels.Seller;
using Microsoft.EntityFrameworkCore;

namespace AutoTrade.Services.Data
{
    public class SellerService : ISellerService
    {
        private readonly AutoTradeDbContext dbContext;

        public SellerService(AutoTradeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

		public async Task Create(string userId, BecomeSellerViewModel model)
		{
            Seller seller = new Seller
            {
                UserId = Guid.Parse(userId),
                PhoneNumber = model.PhoneNumber
            };

            await dbContext.Sellers.AddAsync(seller);
            await dbContext.SaveChangesAsync();
		}

		public async Task<string> GetSellerIdByUserIdAsync(string userId)
		{
            Seller? seller = await dbContext
                .Sellers
                .FirstOrDefaultAsync(s => s.UserId.ToString() == userId);

            if (seller == null)
            {
                return null!;
            }

            return seller.Id.ToString();
		}

		public async Task<bool> SellerExistsByPhoneNumberAsync(string phoneNumber)
		{
            bool result = await dbContext
                .Sellers
                .AnyAsync(s => s.PhoneNumber == phoneNumber);

            return result;
		}

		public async Task<bool> SellerExistsByUserIdAsync(string userId)
        {
            bool result = await dbContext
                .Sellers
                .AnyAsync(s => s.UserId.ToString() == userId);

            return result;
        }
    }
}
