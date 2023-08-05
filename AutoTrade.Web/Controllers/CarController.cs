using AutoTrade.Data.Models;
using AutoTrade.Services.Data.Interfaces;
using AutoTrade.Services.Data.Models.Car;
using AutoTrade.Web.Data;
using AutoTrade.Web.Infrastructure.Extensions;
using AutoTrade.Web.ViewModels.Car;
using AutoTrade.Web.ViewModels.Home;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Security.Permissions;

namespace AutoTrade.Web.Controllers
{
	[Authorize]
	public class CarController : Controller
	{
		private readonly ICarService carService;
		private readonly ISellerService sellerService;
		private readonly ICategoryService categoryService;
		private readonly IEngineService engineService;
		private readonly ITransmissionService transmissionService;

		public CarController(ICarService carService, ISellerService sellerService, ICategoryService categoryService, IEngineService engineService, ITransmissionService transmissionService)
		{
			this.carService = carService;
			this.sellerService = sellerService;
			this.categoryService = categoryService;
			this.engineService = engineService;
			this.transmissionService = transmissionService;
		}

		[AllowAnonymous]
		public async Task<IActionResult> All([FromQuery] AllCarsQueryModel queryModel)
		{
			AllCarsFilteredAndPagedServiceModel serviceModel =
			   await carService.AllAsync(queryModel);

			queryModel.Cars = serviceModel.Cars;
			queryModel.TotalCars = serviceModel.TotalCarsCount;

			queryModel.Categories = await categoryService.AllCategoryNamesAsync();
			queryModel.EngineTypes = await engineService.AllEngineTypeNamesAsync();
			queryModel.Transmissions = await transmissionService.AllTransmissionNamesAsync();
			
			return View(queryModel);
		}

		[AllowAnonymous]
		public async Task<IActionResult> Details(string id)
		{
			bool carExists = await carService
				.ExistsByIdAsync(id);

			if (!carExists)
			{
				return RedirectToAction("All", "Car");
			}

			CarDetailsViewModel viewModel = await carService
				.GetDetailsByIdAsync(id);

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

			CarFormModel carViewModel = new CarFormModel()
			{
				Categories = await this.categoryService.AllCategoriesAsync(),
				EngineTypes = await this.engineService.AllEngineTypesAsync(),
				Transmissions = await this.transmissionService.AllTransmissionsAsync()
			};

			return View(carViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Add(CarFormModel carViewModel)
		{
			bool isSeller =
				await sellerService.SellerExistsByUserIdAsync(User.GetId()!);
			if (!isSeller)
			{
				//TempData[ErrorMessage] = "You must become an agent in order to add new houses!";

				return RedirectToAction("Become", "Seller");
			}

			bool categoryExists = await categoryService.ExistsByIdAsync(carViewModel.CategoryId);
			if (!categoryExists)
			{
				ModelState.AddModelError(nameof(carViewModel.CategoryId), "Selected category does not exist!");
			}

			bool engineTypeExists = await engineService.ExistsByIdAsync(carViewModel.EngineTypeId);
			if (!engineTypeExists)
			{
				ModelState.AddModelError(nameof(carViewModel.EngineTypeId), "Selected engine type does not exist!");
			}

			if (!ModelState.IsValid)
			{
				carViewModel.Categories = await categoryService.AllCategoriesAsync();
				carViewModel.EngineTypes = await engineService.AllEngineTypesAsync();
				carViewModel.Transmissions = await transmissionService.AllTransmissionsAsync();

				return View(carViewModel);
			}

			try
			{
				string? sellerId =
					await sellerService.GetSellerIdByUserIdAsync(User.GetId()!);


				await carService.CreateAndReturnIdAsync(carViewModel, sellerId!);

				//TempData[SuccessMessage] = "House was added successfully!";
				return RedirectToAction("All", "Car");
			}
			catch (Exception)
			{
				ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add your new house! Please try again later or contact administrator!");
				carViewModel.Categories = await categoryService.AllCategoriesAsync();

				return View(carViewModel);
			}
		}

		//public async Task<IActionResult> Buy()
		//{
		//	return Ok();
		//}

		public async Task<IActionResult> Mine()
		{
			List<CarAllViewModel> myCars = new List<CarAllViewModel>();

			string userId = this.User.GetId()!;

			myCars.AddRange(await this.carService.AllByUserIdAsync(userId));

			return View(myCars);
		}

		public async Task<IActionResult> CarsForSale()
		{
			List<CarAllViewModel> myCars = new List<CarAllViewModel>();

			string sellerId = this.User.GetId()!;

			myCars.AddRange(await this.carService.AllByUserIdAsync(sellerId));

			return View(myCars);
		}
	}
}
