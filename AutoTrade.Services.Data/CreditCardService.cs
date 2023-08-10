namespace AutoTrade.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

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

		public async Task<bool> CardBelongsToUserByIdAsync(string id, string userId)
		{
			ApplicationUser user = await dbContext
				.Users
				.Include(u=>u.Wallet)
				.FirstAsync(u => u.Id.ToString() == userId);

			bool result = user.Wallet.CreditCardId.ToString() == id;

			return result;
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

		public async Task DeleteByIdAsync(string id)
		{
			CreditCard creditCard = await dbContext
				.CreditCards
				.FirstAsync(c => c.Id.ToString() == id);

			creditCard.IsActive = false;
			await dbContext.SaveChangesAsync();
		}

		public async Task<CreditCardViewModel> GetCreditCardByIdAsync(string creditCardId)
		{
			CreditCard creditCard = await dbContext
				.CreditCards
				.FirstAsync(c => c.Id.ToString() == creditCardId);

			CreditCardViewModel viewModel = new CreditCardViewModel
			{
				Id = creditCard.Id.ToString(),
				NameOnCard = creditCard.NameOnCard,
				CardNumber = creditCard.CardNumber,
				ExpirationDate = creditCard.ExpirationDate,
				CVVCode = creditCard.CVVCode,
				BillingAddress = creditCard.BillingAddress,
				Country = creditCard.Country
			};

			return viewModel;
		}

        public async Task<decimal> WithdrawByIdAsync(WithdrawMoneyFormModel formModel, string id)
        {
			decimal money = formModel.Money;

			Wallet wallet = await dbContext
				.Wallets
				.FirstAsync(w => w.CreditCardId.ToString()== id);

			wallet.Balance += money;
			await dbContext.SaveChangesAsync();

			return money;
        }
    }
}
