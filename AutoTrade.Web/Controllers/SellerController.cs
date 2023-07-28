using AutoTrade.Services.Data.Interfaces;
using AutoTrade.Web.Infrastructure.Extensions;
using AutoTrade.Web.ViewModels.Seller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AutoTrade.Web.Controllers
{
	//using static Common.NotificationMessagesConstants;

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
				//TempData[ErrorMessage] = "You are already an agent!";

				return RedirectToAction("Index", "Home");
			}

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Become(BecomeSellerViewModel model)
		{
			string? userId = User.GetId();
			bool isAgent = await sellerService.SellerExistsByUserIdAsync(userId);
			if (isAgent)
			{
				//TempData[ErrorMessage] = "You are already an agent!";

				return RedirectToAction("Index", "Home");
			}

			bool isPhoneNumberTaken =
				await sellerService.SellerExistsByPhoneNumberAsync(model.PhoneNumber);
			if (isPhoneNumberTaken)
			{
				ModelState.AddModelError(nameof(model.PhoneNumber), "Agent with the provided phone number already exists!");
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			try
			{
				await sellerService.Create(userId, model);
			}
			catch (Exception)
			{
				//TempData[ErrorMessage] = "Unexpected error occurred while registering you as an agent! Please try again later or contact administrator.";

				return RedirectToAction("Index", "Home");
			}

			return RedirectToAction("All", "Cars");
		}
	}
}
