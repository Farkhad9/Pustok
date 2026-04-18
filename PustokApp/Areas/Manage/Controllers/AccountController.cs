using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PustokApp.Models;

namespace PustokApp.Areas.Manage.Controllers
{
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
        //public async Task<IActionResult> CreateRole()
        //{ 
        //    await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
        //    await roleManager.CreateAsync(new IdentityRole { Name = "User" });
        //    await roleManager.CreateAsync(new IdentityRole { Name = "SuperAdmin" });
        //    return Content("Role  Created");
        //}
    }
}
