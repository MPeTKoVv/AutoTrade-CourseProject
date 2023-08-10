namespace AutoTrade.Web.ViewModels.CreditCard
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.CreditCard;

    public class CreditCardFormModel
    {
        [Required]
        [StringLength(NameOnCardMaxLength, MinimumLength = NameOnCardMinLength)]
        public string NameOnCard { get; set; } = null!;

        [Required]
        [StringLength(CreditNumberLength)]
        public string CardNumber { get; set; } = null!;

        [DisplayFormat(DataFormatString = "MM/yy")]
        public DateTime ExpirationDate { get; set; }

        [Range(typeof(int), CVVCodeMinValue, CVVCodeMaxValue)]
        public int CVVCode { get; set; }

        [Required]
        [StringLength(BillingAddressMaxLength, MinimumLength = BillingAddressMinLength)]
        public string BillingAddress { get; set; } = null!;

        [Required]
        [StringLength(CountryMaxLength, MinimumLength = CountryMinLength)]
        public string Country { get; set; } = null!;
    }
}
