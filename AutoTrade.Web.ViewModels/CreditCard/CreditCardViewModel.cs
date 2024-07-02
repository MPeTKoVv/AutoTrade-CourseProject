namespace AutoTrade.Web.ViewModels.CreditCard
{
	public class CreditCardViewModel
	{
		public string Id { get; set; } = null!;

		public string NameOnCard { get; set; } = null!;

		public string CardNumber { get; set; } = null!;

		public string ExpirationDate { get; set; } = null!;

		public string CVVCode { get; set; } = null!;

		public string BillingAddress { get; set; } = null!;

		public string Country { get; set; } = null!;

		public string WalletId { get; set; } = null!;
	}
}
