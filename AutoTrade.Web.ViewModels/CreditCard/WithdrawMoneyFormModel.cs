namespace AutoTrade.Web.ViewModels.CreditCard
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.CreditCard;

    public class WithdrawMoneyFormModel
    {
        [Range(typeof(decimal), WithdrawMinValue, WithdrawMaxValue)]
        public decimal Money { get; set; }
    }
}
