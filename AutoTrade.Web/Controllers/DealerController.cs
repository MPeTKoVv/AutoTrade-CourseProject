using AutoTrade.Services.Data;
using AutoTrade.Services.Data.Interfaces;
using AutoTrade.Web.Infrastructure.Extensions;
using AutoTrade.Web.ViewModels.Dealer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AutoTrade.Web.Controllers
{
	//using static Common.NotificationMessagesConstants;

	[Authorize]
	public class DealerController : Controller
	{
		private readonly IDealerService dealerService;

		public DealerController(IDealerService dealerService)
		{
			this.dealerService = dealerService;
		}

		public async Task<IActionResult> Become()
		{
			string? userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
			bool isDealer = await dealerService.DealerExistsByUserIdAsync(userId);

			if (isDealer)
			{
				//TempData[ErrorMessage] = "You are already an agent!";

				return RedirectToAction("Index", "Home");
			}

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Become(BecomeDealerViewModel model)
		{
			string? userId = User.GetId();
			bool isAgent = await dealerService.DealerExistsByUserIdAsync(userId!);
			if (isAgent)
			{
				//TempData[ErrorMessage] = "You are already an agent!";

				return RedirectToAction("Index", "Home");
			}

			bool isPhoneNumberTaken =
				await dealerService.DealerExistsByPhoneNumberAsync(model.PhoneNumber);
			if (isPhoneNumberTaken)
			{
				ModelState.AddModelError(nameof(model.PhoneNumber), "Dealer with the provided phone number already exists!");
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			try
			{
				await dealerService.Create(userId!, model);
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
