using Microsoft.AspNetCore.Mvc;

namespace AutoTrade.Web.Controllers
{
	public class CarController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
