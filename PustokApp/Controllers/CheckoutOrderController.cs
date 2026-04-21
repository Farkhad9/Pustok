using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokApp.Data;
using PustokApp.ViewModels;

namespace PustokApp.Controllers
{
    public class CheckoutOrderController(AppDbContext context) : Controller
    {
        [Authorize (Roles = "User")]
        public IActionResult Checkout()
        {
            CheckOutVm checkoutVm = new CheckOutVm();
            var user = context.Users
                .Include(u=>u.BasketItems)
                .ThenInclude(b=>b.Book)
                .FirstOrDefault(u => u.UserName == User.Identity.Name);
            checkoutVm.CheckOutItemVms = user.BasketItems.Select(b => new CheckOutItemVm
            {
                Name = b.Book.Name,
                Count = b.Count,
                Price = b.Book.Price
            }).ToList();
            var basketStr = Request.Cookies["basket"];
            return View(checkoutVm);

        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult CheckOut(CheckOutVm checkoutVm)
        {
            var user = context.Users
                .Include(u => u.BasketItems)
                .ThenInclude(b => b.Book)
                .FirstOrDefault(u => u.UserName == User.Identity.Name);

            if (!ModelState.IsValid)
            {
                checkoutVm.CheckOutItemVms = user.BasketItems.Select(b => new CheckOutItemVm
                {
                    Name = b.Book.Name,
                    Count = b.Count,
                    Price = b.Book.DiscountPercent > 0 ? b.Book.Price - (b.Book.Price * b.Book.DiscountPercent / 100) : b.Book.Price
                }).ToList();
                return View(checkoutVm);
            }
            var order = new Models.Order
            {
                AppUserId = user.Id,
                CreatedDate = DateTime.UtcNow,
                Address = checkoutVm.OrderVm.Address,
                State = checkoutVm.OrderVm.State,
                TownCity = checkoutVm.OrderVm.TownCity,
                ZipCode = checkoutVm.OrderVm.ZipCode,
                TotalPrice = user.BasketItems.Sum(b=>b.Count * (b.Book.DiscountPercent > 0 ? b.Book.Price - (b.Book.Price * b.Book.DiscountPercent / 100) : b.Book.Price)),
                OrderItems = user.BasketItems.Select(b => new Models.OrderItem
                {
                    BookId = b.BookId,
                    Count = b.Count,
                    Price = b.Book.DiscountPercent > 0 ? b.Book.Price - (b.Book.Price * b.Book.DiscountPercent / 100) : b.Book.Price
                }).ToList()
            };
            context.Orders.Add(order);
            context.BasketItems.RemoveRange(user.BasketItems);
            context.SaveChanges();
            Response.Cookies.Delete("basket",new() { Expires = DateTime.UtcNow.AddDays(-1) });
            return RedirectToAction("index", "home");

        }
    }
}
