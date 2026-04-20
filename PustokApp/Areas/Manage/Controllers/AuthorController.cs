using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PustokApp.Data;
using PustokApp.Models;
using Microsoft.EntityFrameworkCore;
using PustokApp.ViewModels.UserVm;

namespace PustokApp.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AuthorController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthorController(AppDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var authors = _context.Authors.ToList();
            return View(authors);
        }
        
        public IActionResult Delete(Guid id)
        {
            var author = _context.Authors.Find(id);
            if (author == null) return NotFound();

            _context.Authors.Remove(author);
            _context.SaveChanges();

            return Ok();
        }
        
        public IActionResult Details(Guid id)
        {
            var author = _context.Authors
                .Include(a => a.Books)
                .FirstOrDefault(a => a.Id == id);
            if (author == null) return NotFound();
            return PartialView("_DetailsPartial", author);
        }
        
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Author author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }

            if (_context.Authors.Any(a => a.FullName == author.FullName))
            {
                ModelState.AddModelError("FullName", "An author with this name already exists.");
                return View(author);
            }

            _context.Authors.Add(author);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public IActionResult Edit(Guid id)
        {
            var author = _context.Authors.Find(id);
            if (author == null) return NotFound();

            return View(author);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Author author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }

            if (_context.Authors.Any(a => a.FullName == author.FullName && a.Id != author.Id))
            {
                ModelState.AddModelError("FullName", "An author with this name already exists.");
                return View(author);
            }

            var existingAuthor = _context.Authors.Find(author.Id);
            if (existingAuthor == null) return NotFound();

            existingAuthor.FullName = author.FullName;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Check if username already exists
            var existingUser = await _userManager.FindByNameAsync(model.Username);
            if (existingUser != null)
            {
                ModelState.AddModelError("Username", "Username already exists.");
                return View(model);
            }

            // Check if email already exists
            var existingEmail = await _userManager.FindByEmailAsync(model.Email);
            if (existingEmail != null)
            {
                ModelState.AddModelError("Email", "Email already exists.");
                return View(model);
            }

            var user = new AppUser
            {
                UserName = model.Username,
                Email = model.Email,
                FullName = model.FullName
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }

            // Add user to default role if needed
            await _userManager.AddToRoleAsync(user, "User");

            ViewBag.SuccessMessage = "Registration successful! You can now login.";
            return RedirectToAction("Login", "AdminAccount");
        }
        
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}