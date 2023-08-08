namespace AutoTrade.Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Authorization;

	[Authorize]
	public class ApplicatoinUserController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
