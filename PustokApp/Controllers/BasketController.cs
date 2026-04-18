using Microsoft.AspNetCore.Mvc;

namespace PustokApp.Controllers
{
    public class BasketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SetCookie()
        {
            return View();
        }
    }
}
