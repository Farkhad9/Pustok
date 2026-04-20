using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PustokApp.Models;
using PustokApp.ViewModels.UserVm;

namespace PustokApp.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AccountController 
        (
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        RoleManager<IdentityRole> roleManager
        ) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UserProfile() 
        {
            var user = await userManager.GetUserAsync(User);
            UserProfileVm vm = new UserProfileVm();
            vm.UserProfileInfo = new UserProfileInfoVm
            {
                FullName = user.FullName,
                Username = user.UserName,
                Email = user.Email
            };
            return View(vm); 
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserProfile(UserProfileVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            // Обновляем основную информацию профиля
            user.FullName = model.UserProfileInfo.FullName;
            user.Email = model.UserProfileInfo.Email;

            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }

            // Если пользователь хочет изменить пароль
            if (!string.IsNullOrEmpty(model.UserProfileInfo.CurrentPassword) && 
                !string.IsNullOrEmpty(model.UserProfileInfo.NewPassword))
            {
                if (string.IsNullOrEmpty(model.UserProfileInfo.ConfirmPassword))
                {
                    ModelState.AddModelError("UserProfileInfo.ConfirmPassword", "Please confirm your new password");
                    return View(model);
                }

                if (model.UserProfileInfo.NewPassword != model.UserProfileInfo.ConfirmPassword)
                {
                    ModelState.AddModelError("UserProfileInfo.ConfirmPassword", "New password and confirm password do not match");
                    return View(model);
                }

                var passwordChangeResult = await userManager.ChangePasswordAsync(user, 
                    model.UserProfileInfo.CurrentPassword, model.UserProfileInfo.NewPassword);

                if (!passwordChangeResult.Succeeded)
                {
                    foreach (var error in passwordChangeResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
            }

            // Перезагружаем данные и перенаправляем
            model.UserProfileInfo.Username = user.UserName;
            ViewBag.SuccessMessage = "Profile updated successfully!";
            return View(model);
        }
    }
}
