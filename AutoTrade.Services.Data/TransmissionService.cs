namespace AutoTrade.Services.Data
{
	using Microsoft.EntityFrameworkCore;

	using Interfaces;
	using Web.Data;
	using Web.ViewModels.Transmission;

	public class TransmissionService : ITransmissionService
	{
		private readonly AutoTradeDbContext dbContext;

		public TransmissionService(AutoTradeDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<IEnumerable<string>> AllTransmissionNamesAsync()
		{
			IEnumerable<string> allNames = await dbContext
				.Transmissions
				.Select(c => c.Name)
				.ToArrayAsync();

			return allNames;
		}

		public async Task<IEnumerable<CarSelectTransmissionViewModel>> AllTransmissionsAsync()
		{
			IEnumerable<CarSelectTransmissionViewModel> allCategories = await this.dbContext
				.Transmissions
				.Select(c => new CarSelectTransmissionViewModel
				{
					Id = c.Id,
					Name = c.Name,
				})
				.ToArrayAsync();

			return allCategories;
		}

		public async Task<bool> ExistsByIdAsync(int id)
		{
			bool result = await dbContext
				.Categories
				.AnyAsync(c => c.Id == id);

			return result;
		}
	}
}
