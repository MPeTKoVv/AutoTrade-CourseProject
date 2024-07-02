namespace AutoTrade.Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Authentication;
	using Microsoft.Extensions.Caching.Memory;

	using AutoTrade.Data.Models;
	using Web.ViewModels.User;

    using static Common.NotificationMessagesConstants;
    using static Common.GeneralApplicationConstants;

	public class UserController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMemoryCache memoryCache;

        public UserController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IMemoryCache memoryCache)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.memoryCache = memoryCache;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            ApplicationUser user = new ApplicationUser
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };

            await this.userManager.SetEmailAsync(user, model.Email);
            await this.userManager.SetUserNameAsync(user, model.Email);

            IdentityResult result = await this.userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(model);
            }

            await this.signInManager.SignInAsync(user, false);
            this.memoryCache.Remove(UsersCacheKey);

            return this.RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            LoginFormModel model = new LoginFormModel()
            {
                ReturnUrl = returnUrl,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginFormModel model)
        {
			await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

           var result = await this.signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            if (!result.Succeeded)
            {
                TempData[ErrorMessage] = "There was an error while logging you in! Please, try again later or contact an administrator.";

                return this.View(model);
            }

            return this.Redirect(model.ReturnUrl ?? "/Home/Index");
        }
	}
}
