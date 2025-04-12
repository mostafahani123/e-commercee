using Microsoft.AspNetCore.Mvc;

namespace e_commercee.Areas.Dashboard.Controllers
{
        [Area("Dashboard")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
