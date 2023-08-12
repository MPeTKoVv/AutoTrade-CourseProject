namespace AutoTrade.Services.Tests
{
	using AutoTrade.Data.Models;
	using AutoTrade.Web.Data;

	public static class DatabaseSeeder
	{
		public static ApplicationUser SellerUser;
		public static ApplicationUser CarSellerUser;
		public static ApplicationUser BuyerUser;
		public static Seller Seller;
		public static Seller CarSeller;
		public static Car Car;

		public static void SeedDatabase(AutoTradeDbContext dbContext)
		{
			CarSellerUser = new ApplicationUser()
			{
				Id = Guid.Parse("E44E2759-94B3-4441-B2D4-4D9DD3260CB6"),
				UserName = "Martin",
				NormalizedUserName = "PESHO",
				Email = "martin@sellers.com",
				NormalizedEmail = "MARTIN@SELLERS.COM",
				EmailConfirmed = true,
				PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
				ConcurrencyStamp = "caf271d7-0ba7-4ab1-8d8d-6d0e3711c27d",
				SecurityStamp = "ca32c787-626e-4234-a4e4-8c94d85a2b1c",
				TwoFactorEnabled = false,
				FirstName = "Martin",
				LastName = "Petkov",
			};
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
			CarSeller = new Seller()
			{
				Id = Guid.Parse("CDB33D65-5B4B-4DEC-899B-32E2B843F801"),
				PhoneNumber = "+359888888888",
				User = CarSellerUser
			};
			Car = new Car()
			{
				Id = Guid.NewGuid(),
				Make = "Mercedes",
				Model = "C63 AMG",
				Description = "This is my favorite car and the first in my app :)",
				Horsepower = 500,
				Year = 2023,
				Price = 150000M,
				Mileage = 0,
				ImageUrl = "https://i.kinja-img.com/gawker-media/image/upload/c_fill,f_auto,fl_progressive,g_center,h_675,pg_1,q_80,w_1200/f03a0277f53ad691e6ab18fad632edc6.jpg",
				EngineId = 1,
				CategoryId = 5,
				TransmissionId = 2,
				SellerId = Guid.Parse("CDB33D65-5B4B-4DEC-899B-32E2B843F801"),
				OwnerId = Guid.Parse("E44E2759-94B3-4441-B2D4-4D9DD3260CB6")
			};

			dbContext.Users.Add(SellerUser);
			dbContext.Users.Add(BuyerUser);
			dbContext.Sellers.Add(Seller);
			dbContext.Sellers.Add(CarSeller);
			dbContext.Cars.Add(Car);

			dbContext.SaveChanges();
		}
	}
}
