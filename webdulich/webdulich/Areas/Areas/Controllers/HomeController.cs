using Microsoft.AspNetCore.Mvc;

namespace webdulich.Areas.Areas.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
