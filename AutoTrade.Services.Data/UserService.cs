using AutoTrade.Data.Models;
using AutoTrade.Services.Data.Interfaces;
using AutoTrade.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace AutoTrade.Services.Data
{
	public class UserService : IUserService
	{
		private readonly AutoTradeDbContext dbContext;

		public UserService(AutoTradeDbContext dbContext)
		{
			this.dbContext = dbContext;
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

	}
}
