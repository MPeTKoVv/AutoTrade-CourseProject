namespace AutoTrade.Web.ViewModels.Car
{
	using System.ComponentModel.DataAnnotations;
	
	using Services.Mapping;
	using Data.Models;

	public class CarDeleteDetailsViewModel : IMapFrom<Car>
	{
		public string Make { get; set; } = null!;
		public string Model { get; set; } = null!;
		public decimal Price { get; set; }

		[Display(Name = "Image URL")]
		public string ImageUrl { get; set; } = null!;

	}
}
