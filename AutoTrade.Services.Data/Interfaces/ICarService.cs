namespace AutoTrade.Services.Data.Interfaces
{
	using Data.Models.Car;
	using Web.ViewModels.Home;
	using Web.ViewModels.Car;
    using AutoTrade.Services.Data.Models.Statistics;

    public interface ICarService
	{
		Task<IEnumerable<IndexViewModel>> AllCarsOrderedByAddedOnDescendingAsync();

		Task CreateAndReturnIdAsync(CarFormModel carViewModel, string sellerId);

		Task<AllCarsFilteredAndPagedServiceModel> AllAsync(AllCarsQueryModel queryModel);

		Task<IEnumerable<CarAllViewModel>> AllByUserIdAsync(string userId);

		Task<CarDetailsViewModel> GetDetailsByIdAsync(string carId);

		Task<bool> ExistsByIdAsync(string carId);

		Task<CarFormModel> GetCarForEditByCarIdAsync(string carId);

		Task<bool> IsUserWithIdOwnerOfCarWithIdAsync(string carId, string ownerId);

		Task EditCarByIdAndFormModel(string carId, CarFormModel formModel);

		Task<CarDeleteDetailsViewModel> GetCarForDeletByIdAsync(string carId);

		Task DeleteCarByIdAsync(string carId);

		Task<bool> IsForSaleByIdAsync(string carId);

		Task BuyCarAsync(string carId, string userId);

		Task CarForSaleAsync(string carId, string sellerId);

		Task<IEnumerable<CarAllViewModel>> AllCarsForSaleBySellerIdAsync(string sellerId);

		Task<StatisticsServiceModel> GetStatisticsAsync();

		Task ReturnCarToGarageAsync(string id);

		Task<string> GetSellerIdAsync(string carId);
	}
}
