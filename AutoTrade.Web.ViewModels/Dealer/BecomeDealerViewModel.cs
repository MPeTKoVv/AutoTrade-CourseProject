using System.ComponentModel.DataAnnotations;

namespace AutoTrade.Web.ViewModels.Dealer
{
	using static Common.EntityValidationConstants.Dealer;

	public class BecomeDealerViewModel
	{
		[Required]
		[StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
		[Phone]
		[Display(Name = "Phone")]
		public string PhoneNumber { get; set; } = null!;
	}
}
