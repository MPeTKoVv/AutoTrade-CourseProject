namespace AutoTrade.Services.Data.Interfaces
{
	using Web.ViewModels.Engine;

	public interface IEngineService
	{
		Task<IEnumerable<CarSelectEngineTypeViewModel>> AllEngineTypesAsync();

		Task<bool> ExistsByIdAsync(int id);

		Task<IEnumerable<string>> AllEngineTypeNamesAsync();
	}
}
