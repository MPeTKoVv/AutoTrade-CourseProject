namespace AutoTrade.Data.Configurations
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	using Models;

	public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(this.GenerateCategories());
        }

        private Category[] GenerateCategories()
        {
            ICollection<Category> categories = new HashSet<Category>();

            Category category;

            category = new Category
            {
                Id = 1,
                Name = "SUV"
            };
            categories.Add(category);

            category = new Category
            {
                Id = 2,
                Name = "Hatchback"
            };
            categories.Add(category);

            category = new Category
            {
                Id = 3,
                Name = "Crossover"
            };
            categories.Add(category);

            category = new Category
            {
                Id = 4,
                Name = "Cabriolet "
            };
            categories.Add(category);

            category = new Category
            {
                Id = 5,
                Name = "Sedan"
            };
            categories.Add(category);

            category = new Category
            {
                Id = 6,
                Name = "Sports Car"
            };
            categories.Add(category);

            category = new Category
            {
                Id = 7,
                Name = "Coupe"
            };
            categories.Add(category);

            category = new Category
            {
                Id = 8,
                Name = "Minivan"
            };
            categories.Add(category);

            category = new Category
            {
                Id = 9,
                Name = "Station Wagon"
            };
            categories.Add(category);

            category = new Category
            {
                Id = 10,
                Name = "Pickup Truck"
            };
            categories.Add(category);

            return categories.ToArray();
        }
    }
}
