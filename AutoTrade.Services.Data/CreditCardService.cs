namespace AutoTrade.Services.Data
{
    using System.Threading.Tasks;

	using AutoTrade.Data.Models;
    using Interfaces;
	using Web.Data;
	using Web.ViewModels.CreditCard;

    public class CreditCardService : ICreditCardService
    {
		private readonly AutoTradeDbContext dbContext;

        public CreditCardService(AutoTradeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> CreateAndReturnIdAsync(CreditCardFormModel formModel, string walletId)
		{
			CreditCard newCreditCard = new CreditCard
			{
				NameOnCard = formModel.NameOnCard,
				CardNumber = formModel.CardNumber,
				ExpirationDate = formModel.ExpirationDate,
				CVVCode = formModel.CVVCode,
				BillingAddress = formModel.BillingAddress,
				Country = formModel.Country,
				WalletId = Guid.Parse(walletId)
			};

			await dbContext.CreditCards.AddAsync(newCreditCard);
			await dbContext.SaveChangesAsync();

			string id = newCreditCard.Id.ToString();

			return id;
		}
	}
}
