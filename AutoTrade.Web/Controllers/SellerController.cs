using AutoTrade.Services.Data.Interfaces;
using AutoTrade.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoTrade.Web.Controllers
{
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
			//string? userId = this.User.GetId();
			//bool IsSeller = sellerService.SellerExistsByUserIdAsync(userId);

			return View();
		}
	}
}
