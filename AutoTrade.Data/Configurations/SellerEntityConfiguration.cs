namespace AutoTrade.Data.Configurations
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	using AutoTrade.Data.Models;

	using static Common.GeneralApplicationConstants;

	public class SellerEntityConfiguration : IEntityTypeConfiguration<Seller>
	{
		public void Configure(EntityTypeBuilder<Seller> builder)
		{
			builder.HasData(SeedSellers());
		}

		private Seller[] SeedSellers()
		{
			List<Seller> sellers = new List<Seller>();

			Seller seller;

			seller = new Seller()
			{
				PhoneNumber = "+359888888888",
				UserId = Guid.Parse(AdminUserId)
			};
			sellers.Add(seller);

			seller = new Seller()
			{
				PhoneNumber = "+359888888888",
				UserId = Guid.Parse(SellerUserId)
			};
			sellers.Add(seller);

			return sellers.ToArray();
		}
	}
}
