using Microsoft.AspNetCore.Mvc;
using PustokApp.Data;
using System.Linq;

namespace PustokApp.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;

        public SliderController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var sliders = _context.Sliders.ToList();
            return View(sliders);
        }
    }
}
