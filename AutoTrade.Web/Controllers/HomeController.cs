using AutoTrade.Services.Data.Interfaces;
using AutoTrade.Web.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AutoTrade.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarService carService;

        public HomeController(ICarService carService)
        {
            this.carService = carService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<IndexViewModel> viewModel =
                await this.carService.AllCarsOrderedByAddedOnDescendingAsync();

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}