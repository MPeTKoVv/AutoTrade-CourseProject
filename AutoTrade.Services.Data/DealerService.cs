using AutoTrade.Data.Models;
using AutoTrade.Services.Data.Interfaces;
using AutoTrade.Web.Data;
using AutoTrade.Web.ViewModels.Dealer;
using Microsoft.EntityFrameworkCore;

namespace AutoTrade.Services.Data
{
    public class DealerService : IDealerService
    {
        private readonly AutoTradeDbContext dbContext;

        public DealerService(AutoTradeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

		public async Task Create(string userId, BecomeDealerViewModel model)
		{
            Dealer seller = new Dealer
            {
                UserId = Guid.Parse(userId),
                PhoneNumber = model.PhoneNumber
            };

            await dbContext.Sellers.AddAsync(seller);
            await dbContext.SaveChangesAsync();
		}

		public async Task<string> GetDealerIdByUserIdAsync(string userId)
		{
            Dealer? seller = await dbContext
                .Sellers
                .FirstOrDefaultAsync(s => s.UserId.ToString() == userId);

            if (seller == null)
            {
                return null!;
            }

            return seller.Id.ToString();
		}

		public async Task<bool> DealerExistsByPhoneNumberAsync(string phoneNumber)
		{
            bool result = await dbContext
                .Sellers
                .AnyAsync(s => s.PhoneNumber == phoneNumber);

            return result;
		}

		public async Task<bool> DealerExistsByUserIdAsync(string userId)
        {
            bool result = await dbContext
                .Sellers
                .AnyAsync(s => s.UserId.ToString() == userId);

            return result;
        }
    }
}
