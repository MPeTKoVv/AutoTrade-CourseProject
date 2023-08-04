using AutoTrade.Data.Models;
using AutoTrade.Services.Data.Interfaces;
using AutoTrade.Services.Data.Models.Car;
using AutoTrade.Web.Data;
using AutoTrade.Web.Infrastructure.Extensions;
using AutoTrade.Web.ViewModels.Car;
using AutoTrade.Web.ViewModels.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Permissions;

namespace AutoTrade.Web.Controllers
{
	[Authorize]
	public class CarController : Controller
	{
		private readonly ICarService carService;
		private readonly IDealerService sellerService;
		private readonly ICategoryService categoryService;
		private readonly IConditionService conditionService;
		private readonly IEngineService engineService;

		public CarController(ICarService carService, IDealerService sellerService, ICategoryService categoryService, IConditionService conditionService, IEngineService engineService)
		{
			this.carService = carService;
			this.sellerService = sellerService;
			this.categoryService = categoryService;
			this.conditionService = conditionService;
			this.engineService = engineService;
		}

		[AllowAnonymous]
		public async Task<IActionResult> All([FromQuery] AllCarsQueryModel queryModel)
		{
			AllCarsFilteredAndPagedServiceModel serviceModel =
			   await carService.AllAsync(queryModel);

			queryModel.Cars = serviceModel.Cars;
			queryModel.TotalCars = serviceModel.TotalCarsCount;
			queryModel.Categories = await categoryService.AllCategoryNamesAsync();
			queryModel.Conditions = await conditionService.AllConditionNamesAsync();
			queryModel.EngineTypes = await engineService.AllEngineTypeNamesAsync();

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
		public async Task<IActionResult> Edit(string id)
		{
            bool carExists = await carService
                .ExistsByIdAsync(id);

            if (!carExists)
            {
                return RedirectToAction("All", "Car");
            }

			bool isUserSeller = await sellerService
				.DealerExistsByUserIdAsync(User.GetId()!);

			if (!isUserSeller)
			{
                return RedirectToAction("Become", "Dealer");
            }

			string sellerId = await sellerService.GetDealerIdByUserIdAsync(User.GetId()!);
			bool isSellerOwner = await carService.IsDealerWithIdOwnerOfCarWiithIdAsync(id, sellerId);

			if (!isSellerOwner)
			{
				return RedirectToAction("Garage", "Car");
			}

			CarFormModel formModel = await carService
				.GetCarForEditByIdAsync(id);

			formModel.Categories = await categoryService.AllCategoriesAsync();
			formModel.Conditions = await conditionService.AllConditionsAsync();
			formModel.EngineTypes = await engineService.AllEngineTypesAsync();

			return View(formModel);
        }

		[HttpGet]
		public async Task<IActionResult> Add()
		{
			bool isAgent =
				await this.sellerService.DealerExistsByUserIdAsync(this.User.GetId()!);

			if (!isAgent)
			{

				return RedirectToAction("Become", "Dealer");
			}

			CarFormModel carViewModel = new CarFormModel()
			{
				//Images = new HashSet<Image>(),
				Categories = await this.categoryService.AllCategoriesAsync(),
				Conditions = await this.conditionService.AllConditionsAsync(),
				EngineTypes = await this.engineService.AllEngineTypesAsync(),
			};

			return View(carViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Add(CarFormModel carViewModel)
		{
			bool isSeller =
				await sellerService.DealerExistsByUserIdAsync(User.GetId()!);
			if (!isSeller)
			{
				//TempData[ErrorMessage] = "You must become an agent in order to add new houses!";

				return RedirectToAction("Become", "Dealer");
			}

			bool categoryExists = await categoryService.ExistsByIdAsync(carViewModel.CategoryId);
			if (!categoryExists)
			{
				ModelState.AddModelError(nameof(carViewModel.CategoryId), "Selected category does not exist!");
			}

			bool conditionExists = await conditionService.ExistsByIdAsync(carViewModel.ConditionId);
			if (!conditionExists)
			{
				ModelState.AddModelError(nameof(carViewModel.ConditionId), "Selected condition does not exist!");
			}

			bool engineTypeExists = await engineService.ExistsByIdAsync(carViewModel.EngineTypeId);
			if (!engineTypeExists)
			{
				ModelState.AddModelError(nameof(carViewModel.EngineTypeId), "Selected engine type does not exist!");
			}

			if (!ModelState.IsValid)
			{
				carViewModel.Categories = await categoryService.AllCategoriesAsync();
				carViewModel.Conditions = await conditionService.AllConditionsAsync();
				carViewModel.EngineTypes = await engineService.AllEngineTypesAsync();

				return View(carViewModel);
			}

			try
			{
				string? sellerId =
					await sellerService.GetDealerIdByUserIdAsync(User.GetId()!);


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

		public async Task<IActionResult> Garage()
		{
			return Ok();
		}
	}
}
