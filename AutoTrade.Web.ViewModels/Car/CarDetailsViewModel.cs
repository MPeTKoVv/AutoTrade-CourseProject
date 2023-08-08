namespace AutoTrade.Web.ViewModels.Car
{
	using Seller;

	public class CarDetailsViewModel : CarAllViewModel
	{
		public string Description { get; set; } = null!;

		public string Category { get; set; } = null!;

		public string EngineType { get; set; } = null!;

		public string Transmission { get; set; } = null!;

		public int Mileage { get; set; }

		public bool IsForSale { get; set; }

		public SellerInfoOnCarViewModel Seller { get; set; } = null!;
	}
}
