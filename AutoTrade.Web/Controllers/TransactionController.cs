namespace AutoTrade.Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Authorization;

	using Services.Data.Interfaces;
	using Infrastructure.Extensions;
	using ViewModels.Transaction;

	using static Common.NotificationMessagesConstants;

	[Authorize]
	public class TransactionController : Controller
	{
		private readonly ISellerService sellerService;
		private readonly ITransactionService transactionService;

		public TransactionController(ISellerService sellerService, ITransactionService transactionService)
		{
			this.sellerService = sellerService;
			this.transactionService = transactionService;
		}

		public async Task<IActionResult> MySoldCars()
		{
			bool sellerExists = await this.sellerService.SellerExistsByUserIdAsync(this.User.GetId()!);
			if (!sellerExists)
			{
				TempData[ErrorMessage] = "You have to be a seller in order to see your sold cars!";

				return RedirectToAction("Become", "Seller");
			}

			List<TransactionSoldAndBoughtCarsViewModel> soldCars = new List<TransactionSoldAndBoughtCarsViewModel>();

			try
			{
				soldCars.AddRange(await this.transactionService.GetSoldCarsByUserIdAsync(this.User.GetId()!));
			}
			catch (Exception)
			{
				GeneralError();
			}

			return View(soldCars);
		}

		public async Task<IActionResult> MyBoughtCars()
		{
			List<TransactionSoldAndBoughtCarsViewModel> boughtCars = new List<TransactionSoldAndBoughtCarsViewModel>();

			try
			{
				boughtCars.AddRange(await transactionService.GetBoughtCarsByUserIdAsync(this.User.GetId()!));
			}
			catch (Exception)
			{
				GeneralError();
			}
			return View(boughtCars);
		}

		private IActionResult GeneralError()
		{
			TempData[ErrorMessage] =
				"Unexpected error occurred! Please try again later or contact administrator";

			return RedirectToAction("Index", "Home");
		}

	}
}
