using AutoTrade.Data.Models;
using AutoTrade.Services.Data.Interfaces;
using AutoTrade.Web.Data;
using AutoTrade.Web.Infrastructure.Extensions;
using AutoTrade.Web.ViewModels.Car;
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
		private readonly ISellerService sellerService;
		private readonly ICategoryService categoryService;
		private readonly IConditionService conditionService;
		private readonly IEngineService engineService;

		public CarController(ICarService carService, ISellerService sellerService, ICategoryService categoryService, IConditionService conditionService, IEngineService engineService)
		{
			this.carService = carService;
			this.sellerService = sellerService;
			this.categoryService = categoryService;
			this.conditionService = conditionService;
			this.engineService = engineService;
		}

		[AllowAnonymous]
		public async Task<IActionResult> All()
		{
			IEnumerable<IndexViewModel> viewModel =
				await this.carService.AllCarsOrderedByAddedOnDescendingAsync();

			return View(viewModel);
		}

		[HttpGet]
		public async Task<IActionResult> Add()
		{
			bool isAgent =
				await this.sellerService.SellerExistsByUserIdAsync(this.User.GetId()!);

			if (!isAgent)
			{

				return RedirectToAction("Become", "Seller");
			}

			CarViewModel carViewModel = new CarViewModel()
			{
				//Images = new HashSet<Image>(),
				Categories = await this.categoryService.AllCategoriesAsync(),
				Conditions = await this.conditionService.AllConditoinsAsync(),
				EngineTypes = await this.engineService.AllEngineTypesAsync(),
			};

			return View(carViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Add(CarViewModel carViewModel)
		{
			bool isAgent =
				await sellerService.SellerExistsByUserIdAsync(User.GetId()!);
			if (!isAgent)
			{
				//TempData[ErrorMessage] = "You must become an agent in order to add new houses!";

				return RedirectToAction("Become", "Agent");
			}

			bool categoryExists =
				await categoryService.ExistsByIdAsync(carViewModel.CategoryId);
			if (!categoryExists)
			{
				// Adding model error to ModelState automatically makes ModelState Invalid
				ModelState.AddModelError(nameof(carViewModel.CategoryId), "Selected category does not exist!");
			}

			if (!ModelState.IsValid)
			{
				carViewModel.Categories = await categoryService.AllCategoriesAsync();

				return View(carViewModel);
			}

			//try
			//{
				//string? agentId =
				//	await sellerService.GetSellerIdByUserIdAsync(User.GetId()!);

				//string carId =
				//	await carService.CreateAndReturnIdAsync(carViewModel, agentId!);

				//TempData[SuccessMessage] = "House was added successfully!";
			//	return RedirectToAction("Details", "House", new { id = carId });
			//}
			//catch (Exception)
			//{
				ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add your new house! Please try again later or contact administrator!");
				carViewModel.Categories = await categoryService.AllCategoriesAsync();

				return View(carViewModel);
			//}
		}

		public async Task<IActionResult> Buy()
		{
			return Ok();
		}

		public async Task<IActionResult> Garage()
		{
			return Ok();
		}
	}
}
