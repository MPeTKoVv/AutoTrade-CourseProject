using AutoTrade.Web.ViewModels.Home;

namespace AutoTrade.Services.Data.Interfaces
{
	public  interface ICarService
	{
		Task<IEnumerable<IndexViewModel>> AllCarsOrderedByAddedOnDescendingAsync();
		Task<IEnumerable<IndexViewModel>> AllCarsAsync();
	}
}
