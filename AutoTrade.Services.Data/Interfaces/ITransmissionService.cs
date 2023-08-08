namespace AutoTrade.Services.Data.Interfaces
{
	using Web.ViewModels.Transmission;

	public interface ITransmissionService
	{
		Task<IEnumerable<CarSelectTransmissionViewModel>> AllTransmissionsAsync();

		Task<bool> ExistsByIdAsync(int id);

		Task<IEnumerable<string>> AllTransmissionNamesAsync();
	}
}
