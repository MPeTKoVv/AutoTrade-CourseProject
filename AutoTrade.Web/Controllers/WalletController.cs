namespace AutoTrade.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Services.Data.Interfaces;
    using Web.Infrastructure.Extensions;

    using static Common.NotificationMessagesConstants;

    [Authorize]
    public class WalletController : Controller
    {
        private readonly IWalletService walletService;
        private readonly IUserService userService;

        public WalletController(IWalletService walletService, IUserService userService)
        {
            this.walletService = walletService;
            this.userService = userService;
        }

        public async Task<IActionResult> Add()
        {
            bool hasWallet = await userService.HasWalletByIdAsync(this.User.GetId()!);
            if (hasWallet)
            {
                TempData[ErrorMessage] = "You already have a wallet!";

                return RedirectToAction("Mine", "Wallet");
            }

            try
            {
                string walletId = await walletService.CreateAndReturnIdAsync(this.User.GetId()!);
                await userService.SetWalletIdAsync(this.User.GetId()!, walletId);

                TempData[SuccessMessage] = "You successfully added a wallet!";
            }
            catch (Exception)
            {
                GeneralError();

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Mine", "Wallet");

        }

        public async Task<IActionResult> Mine()
        {
            bool hasWallet = await userService.HasWalletByIdAsync(this.User.GetId()!);
            if (!hasWallet)
            {
                TempData[ErrorMessage] = "You do not have a wallet!";

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        private IActionResult GeneralError()
        {
            TempData[ErrorMessage] = "Unexpected error occurred! Please try again later or contact administrator";

            return RedirectToAction("Index", "Home");
        }

    }
}
