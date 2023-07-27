using AutoTrade.Services.Data.Interfaces;
using AutoTrade.Web.Data;
using AutoTrade.Web.ViewModels.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoTrade.Web.Controllers
{
	[Authorize]
	public class CarController : Controller
	{
		private readonly ICarService carService;

		public CarController(ICarService carService)
		{
			this.carService = carService;
		}

		[AllowAnonymous]
		public async Task<IActionResult> All()
		{
			IEnumerable<IndexViewModel> viewModel =
				await this.carService.AllCarsOrderedByAddedOnDescendingAsync();

			return View(viewModel);
		}
	}
}
