namespace AutoTrade.Services.Data.Interfaces
{
	using AutoTrade.Web.ViewModels.Transaction;

	public interface ITransactionService
	{
		Task<IEnumerable<TransactionViewModel>> AllAsync();

		Task RecordTransaction(string carId, string buyerId, string sellerId);

		Task<IEnumerable<TransactionViewModel>> GetTransactionHistoryByUserIdAsync(string userId);

		Task<IEnumerable<TransactionSoldAndBoughtCarsViewModel>> GetSoldCarsByUserIdAsync(string userId);

		Task<IEnumerable<TransactionSoldAndBoughtCarsViewModel>> GetBoughtCarsByUserIdAsync(string userId);

	}
}
