namespace AutoTrade.Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.EntityFrameworkCore.Metadata.Internal;
	using Services.Data.Interfaces;

	[Authorize]
	public class WalletContoller
	{
		private readonly IWalletService walletService;

		public WalletContoller(IWalletService walletService)
		{
			this.walletService = walletService;
		}

		//[HttpGet]
		//public async Task<IActionResult> Add()
		//{
		//	return View();
		//}
	}
}
