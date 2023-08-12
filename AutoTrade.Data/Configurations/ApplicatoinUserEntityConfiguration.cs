namespace AutoTrade.Data.Configurations
{
	using Microsoft.AspNetCore.Identity;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	using Models;
	
	using static Common.GeneralApplicationConstants;

	public class ApplicatoinUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
	{
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			builder
				.Property(u => u.FirstName)
				.HasDefaultValue("Test");

			builder
				.Property(u => u.LastName)
				.HasDefaultValue("Test");

			builder.HasData(SeedUsers());
		}

		public ApplicationUser[] SeedUsers()
		{
			var hasher = new PasswordHasher<ApplicationUser>();

			List<ApplicationUser> users = new List<ApplicationUser>();

			ApplicationUser user;

			user = new ApplicationUser()
			{
				UserName = UserEmail,
				Email = UserEmail,
				NormalizedEmail = UserEmail,
				NormalizedUserName = UserEmail,
				FirstName = "Gosho",
				LastName = "Georgiev"
			};
			user.PasswordHash = hasher.HashPassword(user, "123456");
			
			users.Add(user);

			user = new ApplicationUser()
			{
				Id = Guid.Parse(SellerUserId),
				UserName = SellerEmail,
				Email = SellerEmail,
				NormalizedEmail = SellerEmail,
				NormalizedUserName = SellerEmail,
				FirstName = "Pesho",
				LastName = "Petrov"
			};
			user.PasswordHash = hasher.HashPassword(user, "123456");
			
			users.Add(user);

			user = new ApplicationUser()
			{
				Id = Guid.Parse(AdminUserId),
				UserName = AdminEmail,
				Email = AdminEmail,
				NormalizedEmail = AdminEmail,
				NormalizedUserName = AdminEmail,
				FirstName = "Great",
				LastName = "Admin"
			};
			user.PasswordHash = hasher.HashPassword(user, "123456");

			users.Add(user);

			return users.ToArray();
		}
	}
}
