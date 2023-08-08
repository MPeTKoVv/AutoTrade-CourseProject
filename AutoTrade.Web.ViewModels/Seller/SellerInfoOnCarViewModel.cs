namespace AutoTrade.Web.ViewModels.Seller
{
	using System.ComponentModel.DataAnnotations;

	public class SellerInfoOnCarViewModel
	{
		public string FullName { get; set; } 

		public string Email { get; set; } 

		[Display(Name = "Phone")]
		public string? PhoneNumber { get; set; }
	}
}
