using AutoTrade.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoTrade.Data.Configurations
{
	public class CarEntityConfiguration : IEntityTypeConfiguration<Car>
	{
		public void Configure(EntityTypeBuilder<Car> builder)
		{
			builder
				.HasOne(car => car.Category)
				.WithMany(c => c.Cars)
				.HasForeignKey(car => car.CategoryId)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.HasOne(c => c.EngineType)
				.WithMany(et => et.Cars)
				.HasForeignKey(car => car.EngineId)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.HasOne(c => c.Transmission)
				.WithMany(t => t.Cars)
				.HasForeignKey(c => c.TransmissionId)
				.OnDelete(DeleteBehavior.Restrict);

			//builder
			//	.HasOne(c => c.Owner)
			//	.WithMany(o => o.OwnedCars)
			//	.HasForeignKey(c => c.OwnerId)
			//	.OnDelete(DeleteBehavior.Restrict);

			builder
				.Property(c => c.AddedOn)
				.HasDefaultValueSql("GETDATE()");

			builder
				.Property(c => c.IsActive)
				.HasDefaultValue(true);

			builder
				.Property(c => c.IsForSale)
				.HasDefaultValue(true);

			//builder
			//   .HasOne(c => c.Customer)
			//   .WithMany(c => c.FavoriteCars)
			//   .HasForeignKey(c => c.CustomerId)
			//   .OnDelete(DeleteBehavior.Restrict);

			builder.HasData(this.GenerateCars());
		}

		private Car[] GenerateCars()
		{
			ICollection<Car> cars = new HashSet<Car>();

			Car car;

			car = new Car
			{
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
				//OwnerId = Guid.Parse("E44E2759-94B3-4441-B2D4-4D9DD3260CB6"),
			};

			cars.Add(car);

			return cars.ToArray();
		}
	}
}
