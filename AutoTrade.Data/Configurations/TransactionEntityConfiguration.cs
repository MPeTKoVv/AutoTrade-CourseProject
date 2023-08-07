using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AutoTrade.Data.Models;

namespace AutoTrade.Data.Configurations
{
	public class TransactionEntityConfiguration : IEntityTypeConfiguration<Transaction>
	{
		public void Configure(EntityTypeBuilder<Transaction> builder)
		{
			builder
				.HasOne(t => t.Car)
				.WithMany(c => c.Transactions)
				.HasForeignKey(t => t.Id)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.HasOne(t => t.Buyer)
				.WithMany(au => au.BoughtCarsHistory)
				.HasForeignKey(t => t.Id)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.HasOne(t => t.Seller)
				.WithMany(au => au.SoldCarHistory)
				.HasForeignKey(t => t.Id)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
