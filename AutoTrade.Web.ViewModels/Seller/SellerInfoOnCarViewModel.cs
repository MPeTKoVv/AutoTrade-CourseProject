namespace AutoTrade.Web.ViewModels.Seller
{
	using System.ComponentModel.DataAnnotations;

	public class SellerInfoOnCarViewModel
	{
		public string FullName { get; set; } = null!;

		public string Email { get; set; } = null!;

		[Display(Name = "Phone")]
		public string PhoneNumber { get; set; } = null!;
	}
}
