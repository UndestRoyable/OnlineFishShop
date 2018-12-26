using Microsoft.AspNetCore.Mvc;

namespace OnlineFishShop.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
