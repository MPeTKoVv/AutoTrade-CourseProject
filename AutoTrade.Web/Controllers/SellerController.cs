namespace AutoTrade.Web.Controllers
{
	using System.Security.Claims;

	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	using Services.Data.Interfaces;
	using Web.Infrastructure.Extensions;
	using Web.ViewModels.Seller;

	using static Common.NotificationMessagesConstants;

	[Authorize]
	public class SellerController : Controller
	{
		private readonly ISellerService sellerService;

		public SellerController(ISellerService sellerService)
		{
			this.sellerService = sellerService;
		}

		public async Task<IActionResult> Become()
		{
			string? userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
			bool isSeller = await sellerService.SellerExistsByUserIdAsync(userId);
			if (isSeller)
			{
				TempData[ErrorMessage] = "You are already a seller!";

				return RedirectToAction("Index", "Home");
			}

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Become(BecomeSellerViewModel model)
		{
			string? userId = User.GetId();
			bool isSeller = await sellerService.SellerExistsByUserIdAsync(userId!);
			if (isSeller)
			{
				TempData[ErrorMessage] = "You are already a seller!";

				return RedirectToAction("Index", "Home");
			}

			bool isPhoneNumberTaken = await sellerService.SellerExistsByPhoneNumberAsync(model.PhoneNumber);
			if (isPhoneNumberTaken)
			{
				ModelState.AddModelError(nameof(model.PhoneNumber), "Seller with the provided phone number already exists!");
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			try
			{
				await sellerService.Create(userId!, model);
			}
			catch (Exception)
			{
				GeneralError();
			}

			return RedirectToAction("All", "Car");
		}

		private IActionResult GeneralError()
		{
			TempData[ErrorMessage] = "Unexpected error occurred while registering you as a seller! Please try again later or contact administrator.";

			return RedirectToAction("Index", "Home");
		}
	}
}
