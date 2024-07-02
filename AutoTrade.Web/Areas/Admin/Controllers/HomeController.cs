using Microsoft.AspNetCore.Mvc;

namespace AutoTrade.Web.Areas.Admin.Controllers
{
	public class HomeController : BaseAdminController
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
