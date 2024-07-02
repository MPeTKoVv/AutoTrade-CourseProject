namespace AutoTrade.Web.ViewModels.CreditCard
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.CreditCard;

    public class CreditCardFormModel
    {
        [Required]
        [StringLength(NameOnCardMaxLength, MinimumLength = NameOnCardMinLength)]
		[Display(Name = "Name on card")]
		public string NameOnCard { get; set; } = null!;

        [Required]
		[RegularExpression(@"^(?:\d{4}-){3}\d{4}$|^\d{16}$", ErrorMessage = "Invalid card number format")]
		[Display(Name = "Card number")]
        public string CardNumber { get; set; } = null!;

        [Required]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/(2[3-9]|[3-9][0-9])$", ErrorMessage = "Invalid expiration date format (MM/YY) or invalid card")]
        [Display(Name = "Expiration date")]
        public string ExpirationDate { get; set; } = null!;

        [Required]
        [RegularExpression(@"^\d{3,4}$", ErrorMessage = "Invalid CVV code")]
        [Display(Name = "CVV code")]
        public string CVVCode { get; set; } = null!;

        [Required]
        [StringLength(BillingAddressMaxLength, MinimumLength = BillingAddressMinLength)]
		[Display(Name = "Billing address")]
		public string BillingAddress { get; set; } = null!;

        [Required]
        [StringLength(CountryMaxLength, MinimumLength = CountryMinLength)]
        public string Country { get; set; } = null!;
    }
}
