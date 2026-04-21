using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PustokApp.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public List<BasketItem> BasketItems { get; set; }
        public List<Order> Orders { get; set; }
        public AppUser()
        {
            BasketItems = new List<BasketItem>();   
            Orders = new List<Order>();
        }
    }
}
