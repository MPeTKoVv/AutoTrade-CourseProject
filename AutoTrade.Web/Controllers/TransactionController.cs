namespace AutoTrade.Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Authorization;

	using Services.Data.Interfaces;
	using Infrastructure.Extensions;
	using ViewModels.Transaction;

	using static Common.NotificationMessagesConstants;
	using NuGet.Packaging;

	[Authorize]
	public class TransactionController : Controller
	{
		private readonly ISellerService sellerService;
		private readonly ICarService carService;
		private readonly ITransactionService transactionService;

		public TransactionController(ISellerService sellerService, ICarService carService, ITransactionService transactionService)
		{
			this.sellerService = sellerService;
			this.carService = carService;
			this.transactionService = transactionService;
		}

		public async Task<IActionResult> MySoldCars()
		{
			List<TransactionSoldAndBoughtCarsViewModel> soldCars = new List<TransactionSoldAndBoughtCarsViewModel>();
			soldCars.AddRange(await this.transactionService.GetSoldCarsByUserIdAsync(this.User.GetId()!));

			return View(soldCars);
		}

		public async Task<IActionResult> MyBoughtCars()
		{
			List<TransactionSoldAndBoughtCarsViewModel> boughtCars = new List<TransactionSoldAndBoughtCarsViewModel>();
			boughtCars.AddRange(await transactionService.GetBoughtCarsByUserIdAsync(this.User.GetId()!));
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
