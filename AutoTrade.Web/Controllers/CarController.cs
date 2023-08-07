﻿using AutoTrade.Data.Models;
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
	using static Common.NotificationMessagesConstants;

	[Authorize]
	public class CarController : Controller
	{
		private readonly ICarService carService;
		private readonly ISellerService sellerService;
		private readonly ICategoryService categoryService;
		private readonly IEngineService engineService;
		private readonly ITransmissionService transmissionService;
		private readonly ITransactionService transactionService;

		public CarController(ICarService carService, ISellerService sellerService, ICategoryService categoryService, IEngineService engineService, ITransmissionService transmissionService, ITransactionService transactionService)
		{
			this.carService = carService;
			this.sellerService = sellerService;
			this.categoryService = categoryService;
			this.engineService = engineService;
			this.transmissionService = transmissionService;
			this.transactionService = transactionService;
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
				TempData[ErrorMessage] = "Selected car does not exist! Please, try with another car!";

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
				TempData[ErrorMessage] = "You must become a seller in order to add new cars!";

				return RedirectToAction("Become", "Seller");
			}

			CarFormModel carFormModel = new CarFormModel()
			{
				Categories = await this.categoryService.AllCategoriesAsync(),
				EngineTypes = await this.engineService.AllEngineTypesAsync(),
				Transmissions = await this.transmissionService.AllTransmissionsAsync()
			};

			return View(carFormModel);
		}

		[HttpPost]
		public async Task<IActionResult> Add(CarFormModel carFormModel)
		{
			bool isSeller =
				await sellerService.SellerExistsByUserIdAsync(User.GetId()!);

			if (!isSeller)
			{
				TempData[ErrorMessage] = "You must become a seller in order to add new cars!";

				return RedirectToAction("Become", "Seller");
			}

			bool categoryExists = await categoryService.ExistsByIdAsync(carFormModel.CategoryId);
			if (!categoryExists)
			{
				ModelState.AddModelError(nameof(carFormModel.CategoryId), "Selected category does not exist!");
			}

			bool transmissionExists = await transmissionService.ExistsByIdAsync(carFormModel.TransmissionId);
			if (!transmissionExists)
			{
				ModelState.AddModelError(nameof(carFormModel.TransmissionId), "Selected transmission does not exist!");
			}

			bool engineTypeExists = await engineService.ExistsByIdAsync(carFormModel.EngineTypeId);
			if (!engineTypeExists)
			{
				ModelState.AddModelError(nameof(carFormModel.EngineTypeId), "Selected engine type does not exist!");
			}

			if (!ModelState.IsValid)
			{
				carFormModel.Categories = await categoryService.AllCategoriesAsync();
				carFormModel.Transmissions = await transmissionService.AllTransmissionsAsync();
				carFormModel.EngineTypes = await engineService.AllEngineTypesAsync();

				return View(carFormModel);
			}

			try
			{
				string? sellerId =
					await sellerService.GetSellerIdByUserIdAsync(User.GetId()!);

				await carService.CreateAndReturnIdAsync(carFormModel, sellerId!);

				TempData[SuccessMessage] = "Car was added successfully!";

				return RedirectToAction("All", "Car");
			}
			catch (Exception)
			{
				ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add your new house! Please try again later or contact administrator!");
				carFormModel.Categories = await categoryService.AllCategoriesAsync();

				return View(carFormModel);
			}
		}

		[HttpGet]
		public async Task<IActionResult> Edit(string id)
		{
			bool carExists = await carService.ExistsByIdAsync(id);

			if (!carExists)
			{
				TempData[ErrorMessage] = "Selected car does not exist! Please, try with another car!";

				return RedirectToAction("All", "Car");
			}

			string userId = this.User.GetId()!;
			bool isUserSeller = await sellerService.SellerExistsByUserIdAsync(userId);

			if (!isUserSeller)
			{
				TempData[ErrorMessage] = "In order to edit a car you have to be a seller!";

				return RedirectToAction("Become", "Seller");
			}

			bool isUserOwner = await carService.IsUserWithIdOwnerOfCarWithIdAsync(id, userId);

			if (!isUserOwner)
			{
				TempData[ErrorMessage] = "You can only edit your cars!";

				return RedirectToAction("Mine", "Car");
			}

			try
			{
				CarFormModel formModel = await carService
					.GetCarForEditByCarIdAsync(id);

				formModel.Categories = await this.categoryService.AllCategoriesAsync();
				formModel.Transmissions = await this.transmissionService.AllTransmissionsAsync();
				formModel.EngineTypes = await this.engineService.AllEngineTypesAsync();

				return View(formModel);
			}
			catch (Exception)
			{
				return RedirectToAction("Index", "Home");
			}

		}

		[HttpPost]
		public async Task<IActionResult> Edit(string id, CarFormModel formModel)
		{
			bool carExists = await carService.ExistsByIdAsync(id);

			if (!carExists)
			{
				TempData[ErrorMessage] = "Selected car does not exist! Please, try with another car!";

				return RedirectToAction("All", "Car");
			}

			string userId = this.User.GetId()!;
			bool isUserSeller = await sellerService.SellerExistsByUserIdAsync(userId);

			if (!isUserSeller)
			{
				TempData[ErrorMessage] = "In order to edit a car you have to be a seller!";

				return RedirectToAction("Become", "Seller");
			}

			bool isUserOwner = await carService.IsUserWithIdOwnerOfCarWithIdAsync(id, userId);

			if (!isUserOwner)
			{
				TempData[ErrorMessage] = "You can only edit your cars!";

				return RedirectToAction("Mine", "Car");
			}

			if (!this.ModelState.IsValid)
			{
				formModel.Categories = await this.categoryService.AllCategoriesAsync();
				formModel.Transmissions = await this.transmissionService.AllTransmissionsAsync();
				formModel.EngineTypes = await this.engineService.AllEngineTypesAsync();

				return View(formModel);
			}

			try
			{
				await this.carService.EditCarByIdAndFormModel(id, formModel);
			}
			catch (Exception)
			{
				this.ModelState.AddModelError(string.Empty, "Unexpected error occured while trying update the car.");

				formModel.Categories = await categoryService.AllCategoriesAsync();
				formModel.Transmissions = await transmissionService.AllTransmissionsAsync();
				formModel.EngineTypes = await engineService.AllEngineTypesAsync();

				return View(formModel);
			}
			TempData[SuccessMessage] = "Car was edited successfully!";
			return RedirectToAction("Details", "Car", new { id });
		}

		[HttpGet]
		public async Task<IActionResult> Delete(string id)
		{
			bool carExists = await carService.ExistsByIdAsync(id);

			if (!carExists)
			{
				TempData[ErrorMessage] = "Selected car does not exist! Please, try with another car!";

				return RedirectToAction("All", "Car");
			}

			string userId = this.User.GetId()!;
			bool isUserSeller = await sellerService.SellerExistsByUserIdAsync(userId);

			if (!isUserSeller)
			{
				TempData[ErrorMessage] = "In order to delete a car you have to be a seller!";

				return RedirectToAction("Become", "Seller");
			}

			bool isUserOwner = await carService.IsUserWithIdOwnerOfCarWithIdAsync(id, userId);

			if (!isUserOwner)
			{
				TempData[ErrorMessage] = "You can only delete your cars!";

				return RedirectToAction("Mine", "Car");
			}

			try
			{
				CarDeleteDetailsViewModel viewModel =
					await this.carService.GetCarForDeletByIdAsync(id);

				return View(viewModel);
			}
			catch (Exception)
			{
				return RedirectToAction("Mine", "Car");
			}
		}

		[HttpPost]
		public async Task<IActionResult> Delete(string id, CarDeleteDetailsViewModel viewModel)
		{
			bool carExists = await carService.ExistsByIdAsync(id);

			if (!carExists)
			{
				TempData[ErrorMessage] = "Selected car does not exist! Please, try with another car!";

				return RedirectToAction("All", "Car");
			}

			string userId = this.User.GetId()!;
			bool isUserSeller = await sellerService.SellerExistsByUserIdAsync(userId);

			if (!isUserSeller)
			{
				TempData[ErrorMessage] = "In order to delete a car you have to be a seller!";

				return RedirectToAction("Become", "Seller");
			}

			bool isUserOwner = await carService.IsUserWithIdOwnerOfCarWithIdAsync(id, userId);

			if (!isUserOwner)
			{
				TempData[ErrorMessage] = "You can only delete your cars!";

				return RedirectToAction("Mine", "Car");
			}

			try
			{
				await this.carService.DeleteCarByIdAsync(id);

				TempData[WarningMessage] = "The car was successfully deleted!";

				return RedirectToAction("Mine", "Car");
			}
			catch (Exception)
			{
				return RedirectToAction("All", "Car");
			}
		}

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

			string sellerId = await this.sellerService.GetSellerIdByUserIdAsync(this.User.GetId()!);

			myCars.AddRange(await this.carService.AllCarsForSaleBySellerIdAsync(sellerId));

			return View(myCars);
		}

		[HttpPost]
		public async Task<IActionResult> Buy(string id)
		{
			bool carExists = await this.carService.ExistsByIdAsync(id);
			if (!carExists)
			{
				TempData[ErrorMessage] = "Selected car does not exist! Please, try with another car!";

				return this.RedirectToAction("All", "Car");
			}

			bool isCarForSale = await this.carService.IsForSaleByIdAsync(id);
			if (!isCarForSale)
			{
				TempData[ErrorMessage] = "Selected car is not for sale! Please, try with another car!";

				return this.RedirectToAction("All", "Car");
			}

			string userId = this.User.GetId()!;
			bool isUserOwner = await carService.IsUserWithIdOwnerOfCarWithIdAsync(id, userId);

			if (isUserOwner)
			{
				TempData[ErrorMessage] = "You can not buy your cars!";

				return RedirectToAction("All", "Car");
			}

			try
			{
				//string sellerId = await carService.GetSellerIdAsync(id);
				//await this.transactionService.RecordTransaction(id, userId, sellerId);

				await this.carService.BuyCarAsync(id, userId);

				TempData[SuccessMessage] = "The car was successfully bought!";
			}
			catch (Exception)
			{
				GeneralError();
			}

			return RedirectToAction("Mine", "Car");
		}

		[HttpPost]
		public async Task<IActionResult> ForSale(string id)
		{
			bool carExists = await this.carService.ExistsByIdAsync(id);
			if (!carExists)
			{
				TempData[ErrorMessage] = "Selected car does not exist! Please, try with another car!";

				return this.RedirectToAction("Mine", "Car");
			}

			bool sellerExists = await this.sellerService.SellerExistsByUserIdAsync(this.User.GetId()!);
			if (!sellerExists)
			{
				TempData[ErrorMessage] = "You have to be a seller in order to add your car for sale!";

				return RedirectToAction("Become", "Seller");
			}

			string ownerId = this.User.GetId()!;
			bool isUserOwner = await carService.IsUserWithIdOwnerOfCarWithIdAsync(id, ownerId);

			if (!isUserOwner)
			{
				TempData[ErrorMessage] = "You can only add for sell your cars!";

				return RedirectToAction("Mine", "Car");
			}

			bool isCarForSale = await this.carService.IsForSaleByIdAsync(id);
			if (isCarForSale)
			{
				TempData[ErrorMessage] = "Selected car is already for sale! Please, try with another car!";

				return this.RedirectToAction("CarsForSale", "Car");
			}

			try
			{
				string sellerId = await this.sellerService.GetSellerIdByUserIdAsync(ownerId);
				await this.carService.CarForSaleAsync(id, sellerId);

				TempData[SuccessMessage] = "The car was successfully added for sale!";
			}
			catch (Exception)
			{
				GeneralError();
			}

			return RedirectToAction("CarsForSale", "Car");
		}

		[HttpPost]
		public async Task<IActionResult> ReturnToGarage(string id)
		{
			bool carExists = await this.carService.ExistsByIdAsync(id);
			if (!carExists)
			{
				TempData[ErrorMessage] = "Selected car does not exist! Please, try with another car!";

				return this.RedirectToAction("Mine", "Car");
			}

			bool sellerExists = await this.sellerService.SellerExistsByUserIdAsync(this.User.GetId()!);
			if (!sellerExists)
			{
				TempData[ErrorMessage] = "You have to be a seller in order to add your car for sale!";

				return RedirectToAction("Become", "Seller");
			}

			string ownerId = this.User.GetId()!;
			bool isUserOwner = await carService.IsUserWithIdOwnerOfCarWithIdAsync(id, ownerId);

			if (!isUserOwner)
			{
				TempData[ErrorMessage] = "You can only add for sell your cars!";

				return RedirectToAction("Mine", "Car");
			}

			bool isCarForSale = await this.carService.IsForSaleByIdAsync(id);
			if (!isCarForSale)
			{
				TempData[ErrorMessage] = "Selected car is already in your garage! Please, try with another car!";

				return this.RedirectToAction("Mine", "Car");
			}

			try
			{
				await this.carService.ReturnCarToGarageAsync(id);

				TempData[SuccessMessage] = "The car was successfully returned to your garage!";
			}
			catch (Exception)
			{
				GeneralError();
			}

			return RedirectToAction("Mine", "Car");
		}

		private IActionResult GeneralError()
		{
			TempData[ErrorMessage] =
				"Unexpected error occurred! Please try again later or contact administrator";

			return RedirectToAction("Index", "Home");
		}
	}
}
