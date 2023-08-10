namespace AutoTrade.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.CreditCard;

    public class CreditCard
    {
        public CreditCard()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(NameOnCardMaxLength)]
        public string NameOnCard { get; set; } = null!;

        [Required]
        [MaxLength(CreditNumberLength)]
        public string CardNumber { get; set; } = null!;

        [Required]
        [MaxLength(ExpirationDateMaxLength)]
        public string ExpirationDate { get; set; } = null!;

		[Required]
        [MaxLength(CVVCodeMaxLength)]
        public string CVVCode { get; set; } = null!;

		[Required]
        [MaxLength(BillingAddressMaxLength)]
        public string BillingAddress { get; set; } = null!;

        [Required]
        [MaxLength(CountryMaxLength)]
        public string Country { get; set; } = null!;

        public Guid? WalletId { get; set; }
        public virtual Wallet? Wallet { get; set; }
    }
}
