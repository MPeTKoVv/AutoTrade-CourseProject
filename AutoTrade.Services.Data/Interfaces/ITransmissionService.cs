using AutoTrade.Web.ViewModels.Transmission;

namespace AutoTrade.Services.Data.Interfaces
{
    public interface ITransmissionService
    {
        Task<IEnumerable<CarSelectTransmissionViewModel>> AllTransmissionsAsync();

        Task<bool> ExistsByIdAsync(int id);

        Task<IEnumerable<string>> AllTransmissionNamesAsync();
    }
}
