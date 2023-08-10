namespace AutoTrade.Data.Configurations
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	using AutoTrade.Data.Models;

	public class CreditCardEntityConfiguration : IEntityTypeConfiguration<CreditCard>
	{
		public void Configure(EntityTypeBuilder<CreditCard> builder)
		{
			builder
				.Property(c => c.IsActive)
				.HasDefaultValue(true);
		}
	}
}
