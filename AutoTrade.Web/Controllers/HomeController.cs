﻿namespace AutoTrade.Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;

	using Services.Data.Interfaces;
	using ViewModels.Home;

	using static Common.GeneralApplicationConstants;

	public class HomeController : Controller
	{
		private readonly ICarService carService;

		public HomeController(ICarService carService)
		{
			this.carService = carService;
		}

		public async Task<IActionResult> Index()
		{
			if (this.User.IsInRole(AdminRoleName))
			{
				return this.RedirectToAction("Index", "Home", new { Area = AdminAreaName });
			}

			IEnumerable<IndexViewModel> viewModel =
				await this.carService.AllCarsOrderedByAddedOnDescendingAsync();

			return View(viewModel);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error(int statusCode)
		{
			if (statusCode == 400 || statusCode == 404)
			{
				return this.View("Error404");
			}

			if (statusCode == 401)
			{
				return this.View("Error401");
			}

			return this.View();
		}
	}
}