namespace AutoTrade.Services.Data
{
	using Microsoft.EntityFrameworkCore;

	using AutoTrade.Data.Models;
	using Interfaces;
	using Web.Data;

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
