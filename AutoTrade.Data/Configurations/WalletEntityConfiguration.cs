namespace AutoTrade.Data.Configurations
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	using Models;
    using System;

    using static Common.GeneralApplicationConstants;

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

            builder.HasData(GenerateWallets());
        }

        private Wallet[] GenerateWallets()
        {
            ICollection<Wallet> wallets = new HashSet<Wallet>();

            Wallet wallet;

            wallet = new Wallet
            {
                Id = Guid.Parse(UserWalletId),
                UserId = Guid.Parse(UserId),
                Balance = 150000m
            };
            wallets.Add(wallet);

            wallet = new Wallet
            {
                Id = Guid.Parse(SellerWalletId),
                UserId = Guid.Parse(SellerUserId),
                Balance = 500000m
            };
            wallets.Add(wallet);

            wallet = new Wallet
            {
                Id = Guid.Parse(AdminWalletId),
                UserId = Guid.Parse(AdminUserId),
                Balance = 100000000m
            };
            wallets.Add(wallet);

            return wallets.ToArray();
        }
    }
}
