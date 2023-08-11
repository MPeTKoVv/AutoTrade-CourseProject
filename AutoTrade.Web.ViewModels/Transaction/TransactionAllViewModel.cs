namespace AutoTrade.Web.ViewModels.Transaction
{
	using Data.Models;
	using Services.Mapping;

	public class TransactionAllViewModel : IMapFrom<Transaction>
	{
		public string Id { get; set; } = null!;

		public string SellerId { get; set; } = null!;
		public string SellerFullName { get; set; } = null!;

		public string BuyerId { get; set; } = null!;
		public string BuyerFullName { get; set; } = null!;

		public decimal Amount { get; set; }

		public DateTime Date { get; set; }
	}
}
