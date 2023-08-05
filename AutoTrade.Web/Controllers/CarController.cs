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
                //TempData[ErrorMessage] = "You must become an agent in order to add new houses!";

                return RedirectToAction("Become", "Seller");
            }

            bool categoryExists = await categoryService.ExistsByIdAsync(carFormModel.CategoryId);
            if (!categoryExists)
            {
                ModelState.AddModelError(nameof(carFormModel.CategoryId), "Selected category does not exist!");
            }

            bool engineTypeExists = await engineService.ExistsByIdAsync(carFormModel.EngineTypeId);
            if (!engineTypeExists)
            {
                ModelState.AddModelError(nameof(carFormModel.EngineTypeId), "Selected engine type does not exist!");
            }

            if (!ModelState.IsValid)
            {
                carFormModel.Categories = await categoryService.AllCategoriesAsync();
                carFormModel.EngineTypes = await engineService.AllEngineTypesAsync();
                carFormModel.Transmissions = await transmissionService.AllTransmissionsAsync();

                return View(carFormModel);
            }

            try
            {
                string? sellerId =
                    await sellerService.GetSellerIdByUserIdAsync(User.GetId()!);

                await carService.CreateAndReturnIdAsync(carFormModel, sellerId!);

                //TempData[SuccessMessage] = "House was added successfully!";
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
                return RedirectToAction("All", "Car");
            }

            bool isUserSeller = await sellerService.SellerExistsByUserIdAsync(this.User.GetId()!);

            if (!isUserSeller)
            {
                return RedirectToAction("Become", "Seller");
            }

            string sellerId = await sellerService.GetSellerIdByUserIdAsync(this.User.GetId()!);
            bool isSellerOwner = await carService.IsSellerWithIdOwnerOfCarWithIdAsync(id, sellerId);

            if (!isSellerOwner)
            {
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
                this.ModelState.AddModelError(string.Empty, "Unexoected error occured while trying update the car.");

                formModel.Categories = await categoryService.AllCategoriesAsync();
                formModel.Transmissions = await transmissionService.AllTransmissionsAsync();
                formModel.EngineTypes = await engineService.AllEngineTypesAsync();

                return View(formModel);
            }

            return RedirectToAction("Details", "Car", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            bool carExists = await carService.ExistsByIdAsync(id);

            if (!carExists)
            {
                return RedirectToAction("All", "Car");
            }

            bool isUserSeller = await sellerService.SellerExistsByUserIdAsync(this.User.GetId()!);

            if (!isUserSeller)
            {
                return RedirectToAction("Become", "Seller");
            }

            string sellerId = await sellerService.GetSellerIdByUserIdAsync(this.User.GetId()!);
            bool isSellerOwner = await carService.IsSellerWithIdOwnerOfCarWithIdAsync(id, sellerId);

            if (!isSellerOwner)
            {
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
                return RedirectToAction("All", "Car");
            }

            bool isUserSeller = await sellerService.SellerExistsByUserIdAsync(this.User.GetId()!);

            if (!isUserSeller)
            {
                return RedirectToAction("Become", "Seller");
            }

            string sellerId = await sellerService.GetSellerIdByUserIdAsync(this.User.GetId()!);
            bool isSellerOwner = await carService.IsSellerWithIdOwnerOfCarWithIdAsync(id, sellerId);

            if (!isSellerOwner)
            {
                return RedirectToAction("Mine", "Car");
            }

            try
            {
                await this.carService.DeleteCarByIdAsync(id);

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

            string sellerId = this.User.GetId()!;

            myCars.AddRange(await this.carService.AllByUserIdAsync(sellerId));

            return View(myCars);
        }

        //public async Task<IActionResult> Buy()
        //{
        //	return Ok();
        //}

    }
}
