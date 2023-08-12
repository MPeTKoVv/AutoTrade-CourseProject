namespace AutoTrade.Services.Tests
{
	using AutoTrade.Data.Models;
	using AutoTrade.Web.Data;

	public static class DatabaseSeeder
	{
		public static ApplicationUser SellerUser;
		public static ApplicationUser BuyerUser;
		public static Seller Seller;

		public static void SeedDatabase(AutoTradeDbContext dbContext)
		{
			SellerUser = new ApplicationUser()
			{
				UserName = "Pesho",
				NormalizedUserName = "PESHO",
				Email = "pesho@sellers.com",
				NormalizedEmail = "PESHO@SELLERS.COM",
				EmailConfirmed = true,
				PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
				ConcurrencyStamp = "caf271d7-0ba7-4ab1-8d8d-6d0e3711c27d",
				SecurityStamp = "ca32c787-626e-4234-a4e4-8c94d85a2b1c",
				TwoFactorEnabled = false,
				FirstName = "Pesho",
				LastName = "Petrov"
			};
			BuyerUser = new ApplicationUser()
			{
				UserName = "Gosho",
				NormalizedUserName = "GOSHO",
				Email = "gosho@renters.com",
				NormalizedEmail = "GOSHO@RENTERS.COM",
				EmailConfirmed = true,
				PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
				ConcurrencyStamp = "8b51706e-f6e8-4dae-b240-54f856fb3004",
				SecurityStamp = "f6af46f5-74ba-43dc-927b-ad83497d0387",
				TwoFactorEnabled = false,
				FirstName = "Gosho",
				LastName = "Goshov"
			};
			Seller = new Seller()
			{
				PhoneNumber = "+359888888888",
				User = SellerUser
			};

			dbContext.Users.Add(SellerUser);
			dbContext.Users.Add(BuyerUser);
			dbContext.Sellers.Add(Seller);

			dbContext.SaveChanges();
		}
	}
}
