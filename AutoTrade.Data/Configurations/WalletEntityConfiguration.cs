namespace AutoTrade.Data.Configurations
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	using Models;

	public class WalletEntityConfiguration : IEntityTypeConfiguration<Wallet>
	{
		public void Configure(EntityTypeBuilder<Wallet> builder)
		{
			builder
				.HasOne(w => w.User)
				.WithOne(u => u.Wallet)
				.HasForeignKey<Wallet>(w => w.UserId)
				.OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(w => w.CreditCard)
                .WithOne(c => c.Wallet)
                .HasForeignKey<Wallet>(w => w.CreditCardId)
                .OnDelete(DeleteBehavior.Restrict);
        }
	}
}
