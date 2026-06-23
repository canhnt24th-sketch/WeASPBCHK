using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using webdulich.Models;

namespace webdulich.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
