namespace AutoTrade.Web.ViewModels.Car
{
	using Data.Models;
	using Services.Mapping;

	public class CarAllViewModel : IMapFrom<Car>
	{
		public string Id { get; set; } = null!;
		public string Make { get; set; } = null!;
		public string Model { get; set; } = null!;
		public decimal Price { get; set; }
		public int Year { get; set; }
		public int Horsepower { get; set; }
		public string ImageUrl { get; set; } = null!;
		public bool IsForSale { get; set; }
	}
}
