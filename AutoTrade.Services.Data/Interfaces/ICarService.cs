using AutoTrade.Web.ViewModels.Car;
using AutoTrade.Web.ViewModels.Home;

namespace AutoTrade.Services.Data.Interfaces
{
	public  interface ICarService
	{
		Task<IEnumerable<IndexViewModel>> AllCarsOrderedByAddedOnDescendingAsync();
		
		Task<IEnumerable<IndexViewModel>> AllCarsAsync();

		Task CreateAndReturnIdAsync(CarViewModel carViewModel, string sellerId);

		Task<IEnumerable<IndexViewModel>> AllMyCarsForSale(string sellerId);
	}
}
