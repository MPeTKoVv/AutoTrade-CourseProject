using AutoTrade.Services.Data.Interfaces;
using AutoTrade.Web.Data;
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

        public async Task<bool> SellerExistsByUserIdAsync(string userId)
        {
            bool result = await dbContext.Sellers.AnyAsync(s => s.UserId.ToString() == userId);

            return result;
        }
    }
}
