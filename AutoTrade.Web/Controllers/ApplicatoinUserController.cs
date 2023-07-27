using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoTrade.Web.Controllers
{
	[Authorize]
	public class ApplicatoinUserController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
