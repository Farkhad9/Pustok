using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokApp.Data;
using PustokApp.Models;
using PustokApp.ViewModels;

namespace PustokApp.Controllers
{
    public class HomeController(AppDbContext dbContext) : Controller
    {
        
        public IActionResult Index()
        {
            HomeVm homeVm = new HomeVm
            {
                Sliders = dbContext.Sliders.ToList(),

                FeaturedBooks = dbContext.Books
                .Include(b => b.Author)
                .Include(b => b.BookImages)
                .Where(b => b.IsFeatured)
                .ToList(),
                NewBooks = dbContext.Books
                .Include(b => b.Author)
                .Include(b => b.BookImages)
                .Where(b => b.IsNew)
                .ToList(),
                DiscountedBooks = dbContext.Books
                .Include(b => b.Author)
                .Include(b => b.BookImages)
                .Where(b => b.DiscountPercent > 0)
                .ToList()
            };
            return View(homeVm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
