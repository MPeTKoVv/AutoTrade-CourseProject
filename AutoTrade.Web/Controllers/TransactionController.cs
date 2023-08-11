namespace AutoTrade.Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Authorization;

	using Services.Data.Interfaces;
	using ViewModels.Car;
	using Infrastructure.Extensions;

	using static Common.NotificationMessagesConstants;

	[Authorize]
	public class TransactionController : Controller
	{
		private readonly ISellerService sellerService;
		private readonly ICarService carService;

		public TransactionController(ISellerService sellerService, ICarService carService)
		{
			this.sellerService = sellerService;
			this.carService = carService;
		}

		public async Task<IActionResult> MySoldCars()
		{
			List<CarAllViewModel> myCars = new List<CarAllViewModel>();

			return View(myCars);
		}

		public async Task<IActionResult> MyBoughtCars()
		{
			List<CarAllViewModel> myCars = new List<CarAllViewModel>();

			return View(myCars);
		}

		private IActionResult GeneralError()
		{
			TempData[ErrorMessage] =
				"Unexpected error occurred! Please try again later or contact administrator";

			return RedirectToAction("Index", "Home");
		}

	}
}
