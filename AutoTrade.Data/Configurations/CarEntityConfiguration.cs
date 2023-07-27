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
				.HasOne(c => c.Category)
				.WithMany(c => c.Cars)
				.HasForeignKey(c => c.CategoryId)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.HasOne(c => c.Condition)
				.WithMany(c => c.Cars)
				.HasForeignKey(c => c.ConditionId)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.HasOne(c => c.Seller)
				.WithMany(s => s.CarsForSale)
				.HasForeignKey(c => c.SellerId)
				.OnDelete(DeleteBehavior.Restrict);

			builder
			   .HasOne(c => c.Customer)
			   .WithMany(c => c.Garage)
			   .HasForeignKey(c => c.CustomerId)
			   .OnDelete(DeleteBehavior.Restrict);

			builder
				.HasOne(c => c.EngineType)
				.WithMany(et => et.Cars)
				.HasForeignKey(c => c.EngineId)
				.OnDelete(DeleteBehavior.Restrict);

			//builder
			//	.HasMany(c => c.Images)
			//	.WithOne(i => i.Car)
			//	.HasForeignKey(i => i.CarId)
			//	.OnDelete(DeleteBehavior.Restrict);

			builder
				.Property(c => c.AddedOn)
				.HasDefaultValue(DateTime.UtcNow);

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
				Country = "Germany",
				Description = "This is my favorite car and the first my app :)",
                Horsepower = 500,
				Year = 2023,
				Price = 150000M,
				Mileage = 0,
				EngineId = 1,
				ConditionId = 1,
				CategoryId = 5,
				SellerId = Guid.Parse("CDB33D65-5B4B-4DEC-899B-32E2B843F801"),
				//CustomerId = Guid.Parse("018D9958-F826-4FE3-9115-B9E14589BD2C")
			};
			car.Images.Add("https://i.kinja-img.com/gawker-media/image/upload/c_fill,f_auto,fl_progressive,g_center,h_675,pg_1,q_80,w_1200/f03a0277f53ad691e6ab18fad632edc6.jpg");

			cars.Add(car);

			return cars.ToArray();
		}
	}
}
