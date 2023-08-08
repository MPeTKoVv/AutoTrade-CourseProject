namespace AutoTrade.Web.ViewModels.Car
{
	using System.ComponentModel.DataAnnotations;
	
	using Car.Enums;

	using static Common.GeneralApplicationConstants;

	public class AllCarsQueryModel
	{
		public AllCarsQueryModel()
		{
			this.CurrentPage = DefaultPage;
			this.CarsPerPage = EntitiesPerPage;

			this.Categories = new HashSet<string>();
			this.EngineTypes = new HashSet<string>();
			this.Transmissions = new HashSet<string>();
		}

		public string? Category { get; set; }

		[Display(Name = "Engine type")]
		public string? EngineType { get; set; }

		public string? Transmission { get; set; }

		[Display(Name = "Search by text")]
		public string? SearchString { get; set; }

		[Display(Name = "Car sorting")]
		public CarSorting CarSorting { get; set; }

		public int CurrentPage { get; set; }

		[Display(Name = "Cars per page")]
		public int CarsPerPage { get; set; }

		public int TotalCars { get; set; }

		public IEnumerable<string> Categories { get; set; }

		public IEnumerable<string> EngineTypes { get; set; }

		public IEnumerable<string> Transmissions { get; set; }

		public IEnumerable<CarAllViewModel> Cars { get; set; }
	}
}
