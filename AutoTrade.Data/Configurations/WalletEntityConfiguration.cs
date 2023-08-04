using AutoTrade.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoTrade.Data.Configurations
{
	public class WalletEntityConfiguration : IEntityTypeConfiguration<Wallet>
	{
		public void Configure(EntityTypeBuilder<Wallet> builder)
		{
			builder
				.HasOne(w => w.User)
				.WithOne(u => u.Wallet)
				.HasForeignKey<Wallet>(w => w.Id)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.Property(w => w.Balance)
				.HasDefaultValue(1000000m);
		}
	}
}
