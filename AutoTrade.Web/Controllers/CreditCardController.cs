namespace AutoTrade.Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Authorization;

	using Services.Data.Interfaces;
	using ViewModels.CreditCard;
	using Infrastructure.Extensions;

	using static Common.NotificationMessagesConstants;

    [Authorize]
	public class CreditCardController : Controller
	{
		private readonly ICreditCardService creditCardService;
		private readonly IWalletService walletService;
		private readonly IUserService userService;

		public CreditCardController(ICreditCardService creditCardService, IWalletService walletService, IUserService userService)
		{
			this.creditCardService = creditCardService;
			this.walletService = walletService;
			this.userService = userService;
		}

		[HttpGet]
		public async Task<IActionResult> Add()
		{
			bool hasWallet = await userService.HasWalletByIdAsync(this.User.GetId()!);
			if (!hasWallet)
			{
				TempData[ErrorMessage] = "You do not have a wallet!";

				return RedirectToAction("Index", "Home");
			}

			string? walletId = await walletService.GetIdByUserIdAsync(this.User.GetId()!);
			bool hasCard = await walletService.HasCreditCardByIdAsync(walletId!);
			if (hasCard)
			{
				TempData[ErrorMessage] = "You already have a credit card!";

				return RedirectToAction("Mine", "Wallet");
			}

			CreditCardFormModel fromModel = new CreditCardFormModel();

			return View(fromModel);
		}

		[HttpPost]
		public async Task<IActionResult> Add(CreditCardFormModel formModel)
		{
			bool hasWallet = await userService.HasWalletByIdAsync(this.User.GetId()!);
			if (!hasWallet)
			{
				TempData[ErrorMessage] = "You do not have a wallet!";

				return RedirectToAction("Index", "Home");
			}

			string? walletId = await walletService.GetIdByUserIdAsync(this.User.GetId()!);
			bool hasCard = await walletService.HasCreditCardByIdAsync(walletId!);
			if (hasCard)
			{
				TempData[ErrorMessage] = "You already have a credit card!";

				return RedirectToAction("Mine", "Wallet");
			}

			if (!ModelState.IsValid)
			{
				return View(formModel);
			}

			try
			{
				string creditCardId = await creditCardService.CreateAndReturnIdAsync(formModel, walletId!);
				await walletService.AddCreditCardByIdAndWalletIdAsync(walletId!, creditCardId);

				TempData[SuccessMessage] = "Credit card was added successfully!";
			}
			catch (Exception)
			{
				GeneralError();

				return View(formModel);
			}

			return RedirectToAction("Mine", "Wallet");
		}

		public async Task<IActionResult> Delete(string id)
		{
			bool hasWallet = await userService.HasWalletByIdAsync(this.User.GetId()!);
			if (!hasWallet)
			{
				TempData[ErrorMessage] = "You do not have a wallet!";

				return RedirectToAction("Index", "Home");
			}

			string? walletId = await walletService.GetIdByUserIdAsync(this.User.GetId()!);
			bool hasCard = await walletService.HasCreditCardByIdAsync(walletId!);
			if (!hasCard)
			{
				TempData[ErrorMessage] = "You do not have a credit card!";

				return RedirectToAction("Mine", "Wallet");
			}

			try
			{
				await creditCardService.DeleteByIdAsync(id);
				await walletService.DeleteCreditCardByIdAsync(walletId!);

				TempData[WarningMessage] = "Credit card was successfully deleted!";
			}
			catch (Exception)
			{
				GeneralError();

				return RedirectToAction("Mine", "Wallet");
			}

			return RedirectToAction("Mine", "Wallet");
		}

		[HttpGet]
		public async Task<IActionResult> Withdraw(string id)
		{
			bool cardBelongsToUser = await creditCardService.CardBelongsToUserByIdAsync(id, this.User.GetId()!);
			if (!cardBelongsToUser)
			{
				TempData[ErrorMessage] = "You can only wirhdraw from you card! Please, try with your credit card!";

				return RedirectToAction("Mine", "Wallet");
			}

			WithdrawMoneyFormModel formModel = new WithdrawMoneyFormModel();

			return View();
		}

        [HttpPost]
        public async Task<IActionResult> Withdraw(WithdrawMoneyFormModel formModel, string id)
        {
            bool cardBelongsToUser = await creditCardService.CardBelongsToUserByIdAsync(id, this.User.GetId()!);
            if (!cardBelongsToUser)
            {
                TempData[ErrorMessage] = "You can only wirhdraw from you card! Please, try with your credit card!";

                return RedirectToAction("Mine", "Wallet");
            }

			try
			{
				decimal money = await creditCardService.WithdrawByIdAsync(formModel, id);

				TempData[SuccessMessage] = $"{money.ToString("N"):f2}€ was withdrawn successully to your wallet balance!";
			}
			catch (Exception)
			{
				GeneralError();
            }

			return RedirectToAction("Mine", "Wallet");
        }

        private IActionResult GeneralError()
		{
			TempData[ErrorMessage] =
				"Unexpected error occurred! Please try again later or contact administrator";

			return RedirectToAction("Index", "Home");
		}

	}
}
