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
                AddedOn = DateTime.UtcNow.Date,
                Price = 150000M,
                Mileage = 0,
                ImageUrl = "https://i.ytimg.com/vi/i30EiaV-4_k/maxresdefault.jpg",
                ConditionId = 1,
                CategoryId = 5,
                SellerId = Guid.Parse("CDB33D65-5B4B-4DEC-899B-32E2B843F801"),
                //CustomerId = Guid.Parse("018D9958-F826-4FE3-9115-B9E14589BD2C")
            };
            cars.Add(car);

            return cars.ToArray();
        }
    }
}
