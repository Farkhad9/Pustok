using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PustokApp.Models;
using PustokApp.Areas.Manage.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace PustokApp.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AdminAccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : Controller
    {
        public async Task<IActionResult> CreateAdmin()
        {
            AppUser admin = new AppUser()
            {
                UserName = "_admin",
                FullName = "Farkhad Ismiyev",
                Email = "sds37061@gmail.com"
            };
            IdentityResult result = await userManager.CreateAsync(admin, "_Admin123!");
            if (!result.Succeeded)
            {
               return Json(result.Errors);
            }
            await userManager.AddToRoleAsync(admin, "Admin");
            
            // Входим в систему после создания
            await signInManager.SignInAsync(admin, isPersistent: true);
            
            return Content("Admin account created successfully");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AdminLoginVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                ViewBag.ErrorMessage = "Username or password is incorrect";
                return View(model);
            }

            // Проверяем если аккаунт заблокирован
            if (await userManager.IsLockedOutAsync(user))
            {
                ViewBag.ErrorMessage = "Your account is locked. Please try again after 15 minutes.";
                return View(model);
            }

            var isPasswordValid = await userManager.CheckPasswordAsync(user, model.Password);
            if (!isPasswordValid)
            {
                // Увеличиваем счётчик неудачных попыток
                await userManager.AccessFailedAsync(user);
                
                // Проверяем если аккаунт теперь заблокирован после этой попытки
                if (await userManager.IsLockedOutAsync(user))
                {
                    ViewBag.ErrorMessage = "Your account has been locked due to multiple failed login attempts. Please try again after 15 minutes.";
                    return View(model);
                }

                var failedAttempts = await userManager.GetAccessFailedCountAsync(user);
                var remainingAttempts = 3 - failedAttempts;

                if (remainingAttempts > 0)
                {
                    ViewBag.ErrorMessage = $"Username or password is incorrect. {remainingAttempts} attempt(s) remaining before your account is locked.";
                }
                else
                {
                    ViewBag.ErrorMessage = "Your account has been locked due to multiple failed login attempts. Please try again after 15 minutes.";
                }

                return View(model);
            }

            // Сбрасываем счётчик неудачных попыток при успешном входе
            if (await userManager.GetAccessFailedCountAsync(user) > 0)
            {
                await userManager.ResetAccessFailedCountAsync(user);
            }

            // Вход с Remember Me опцией
            await signInManager.SignInAsync(user, isPersistent: model.RememberMe);

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        [Authorize]
        public async Task<IActionResult> UserProfile()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            return Json(user);
        }
    }
}
