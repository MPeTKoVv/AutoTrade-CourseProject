using AutoTrade.Web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoTrade.Web.Controllers
{
	[Authorize]
	public class CarController : Controller
	{

        [AllowAnonymous]
		public IActionResult All()
		{
			return View();
		}
	}
}
