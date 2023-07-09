using AutoTrade.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoTrade.Data.Configurations
{
	public class EngineTypeEntityConfiguration : IEntityTypeConfiguration<EngineType>
	{
		public void Configure(EntityTypeBuilder<EngineType> builder)
		{
			builder.HasData(GenerateEngineTypes());
		}

		private EngineType[] GenerateEngineTypes()
		{
			ICollection<EngineType> engineTypes = new HashSet<EngineType>();

			EngineType engineType;

			engineType = new EngineType
			{
				Id = 1,
				Type = "Petrol"
			};
			engineTypes.Add(engineType);

			engineType = new EngineType
			{
				Id = 2,
				Type = "Diesel"
			};
			engineTypes.Add(engineType);

			engineType = new EngineType
			{
				Id = 3,
				Type = "Electric"
			};
			engineTypes.Add(engineType);

			engineType = new EngineType
			{
				Id = 4,
				Type = "Hybrid"
			};
			engineTypes.Add(engineType);

			engineType = new EngineType
			{
				Id = 5,
				Type = "Hydrogen Fuel Cell"
			};
			engineTypes.Add(engineType);

			return engineTypes.ToArray();
		}
	}
}
