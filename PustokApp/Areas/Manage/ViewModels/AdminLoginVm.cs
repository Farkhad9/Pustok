using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PustokApp.Areas.Manage.ViewModels
{
    public class AdminLoginVm 
    {
        [Required]
        [MinLength(2)]
        public string Username { get; set; }
        
        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
