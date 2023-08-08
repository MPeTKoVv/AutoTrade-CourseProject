namespace AutoTrade.Web.ViewModels.Home
{
    using Data.Models;
	using Services.Mapping;

	public class IndexViewModel : IMapFrom<Car>
	{
        public string Id { get; set; } = null!;
        public string Make { get; set; } = null!;
		public string Model { get; set; } = null!;
        public int Year { get; set; }
        public int Horsepower { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}
