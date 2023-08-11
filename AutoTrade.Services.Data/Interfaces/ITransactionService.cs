using AutoTrade.Web.ViewModels.Transaction;

namespace AutoTrade.Services.Data.Interfaces
{
	public interface ITransactionService
	{
		Task RecordTransaction(string carId, string buyerId, string sellerId);

		Task<IEnumerable<TransactionAllViewModel>> GetTransactionHistoryByUserIdAsync(string userId);

		Task<IEnumerable<TransactionSoldAndBoughtCarsViewModel>> GetSoldCarsByUserIdAsync(string userId);

		Task<IEnumerable<TransactionSoldAndBoughtCarsViewModel>> GetBoughtCarsByUserIdAsync(string userId);
	}
}
