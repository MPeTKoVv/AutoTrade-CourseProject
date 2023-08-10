﻿namespace AutoTrade.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;

    using Services.Data.Interfaces;
    using Web.ViewModels.CreditCard;
    using Web.Infrastructure.Extensions;

    using static Common.NotificationMessagesConstants;
    using AutoTrade.Data.Models;

    [Authorize]
    public class CreditCardController : Controller
    {
        private readonly ICreditCardService creditCardService;
        private readonly IWalletService walletService;

        public CreditCardController(ICreditCardService creditCardService, IWalletService walletService)
        {
            this.creditCardService = creditCardService;
            this.walletService = walletService;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            CreditCardFormModel fromModel = new CreditCardFormModel();

            return View(fromModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreditCardFormModel formModel)
        {
            string? walletId = await walletService.GetIdByUserIdAsync(this.User.GetId()!);
            if (walletId == null)
            {
                TempData[ErrorMessage] = "You";
            }

            if (!ModelState.IsValid)
            {
                return View(formModel);
            }

            //await creditCardService.


            return RedirectToAction("Mine", "Wallet");
        }
    }
}
