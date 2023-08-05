namespace AutoTrade.Services.Data.Interfaces
{
	using Data.Models.Car;
	using Web.ViewModels.Home;
	using Web.ViewModels.Car;

	public interface ICarService
	{
		Task<IEnumerable<IndexViewModel>> AllCarsOrderedByAddedOnDescendingAsync();

		Task CreateAndReturnIdAsync(CarFormModel carViewModel, string sellerId);

		Task<AllCarsFilteredAndPagedServiceModel> AllAsync(AllCarsQueryModel queryModel);

		Task<IEnumerable<CarAllViewModel>> AllByUserIdAsync(string userId);

		Task<CarDetailsViewModel> GetDetailsByIdAsync(string carId);

		Task<bool> ExistsByIdAsync(string carId);

		Task<CarFormModel> GetCarForEditByCarIdAsync(string carId);

		Task<bool> IsSellerWithIdOwnerOfCarWithIdAsync(string carId, string sellerId);

		Task EditCarByIdAndFormModel(string carId, CarFormModel formModel);
	}
}
