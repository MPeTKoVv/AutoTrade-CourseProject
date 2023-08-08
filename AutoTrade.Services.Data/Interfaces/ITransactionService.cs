namespace AutoTrade.Services.Data.Interfaces
{
	public interface ITransactionService
	{
		Task RecordTransaction(string carId, string buyerId, string sellerId);
	}
}
