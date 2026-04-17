using Microsoft.AspNetCore.Mvc;
using PustokApp.Data;

namespace PustokApp.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AuthorController : Controller
    {
        private readonly AppDbContext _context;

        public AuthorController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var authors = _context.Authors.ToList();
            return View(authors);
        }
        public IActionResult Delete(int id)
        { 
            var author = _context.Authors.Find(id);
            if (author == null) return NotFound();

            _context.Authors.Remove(author);
            _context.SaveChanges();

            return Ok();
        }
    }
}
