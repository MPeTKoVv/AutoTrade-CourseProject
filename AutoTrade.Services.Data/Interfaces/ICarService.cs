namespace AutoTrade.Services.Data.Interfaces
{
	using Data.Models.Car;
	using Web.ViewModels.Home;
	using Web.ViewModels.Car;

	public interface ICarService
	{
		Task<IEnumerable<IndexViewModel>> AllCarsOrderedByAddedOnDescendingAsync();

		Task<IEnumerable<IndexViewModel>> AllCarsAsync();

		Task CreateAndReturnIdAsync(CarFormModel carViewModel, string sellerId);

		//Task<IEnumerable<IndexViewModel>> AllMyCarsForSale(string sellerId);

		Task<AllCarsFilteredAndPagedServiceModel> AllAsync(AllCarsQueryModel queryModel);

		Task<CarDetailsViewModel?> GetDetailsByIdAsync(string carId);
	}
}
