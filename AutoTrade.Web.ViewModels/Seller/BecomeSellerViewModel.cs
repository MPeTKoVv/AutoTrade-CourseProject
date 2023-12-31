﻿namespace AutoTrade.Web.ViewModels.Seller
{
	using System.ComponentModel.DataAnnotations;

	using static Common.EntityValidationConstants.Seller;

	public class BecomeSellerViewModel
	{
		[Required]
		[StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
		[Phone]
		[Display(Name = "Phone")]
		public string PhoneNumber { get; set; } = null!;
	}
}
