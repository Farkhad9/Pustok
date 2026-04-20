using System.ComponentModel.DataAnnotations;

namespace PustokApp.ViewModels.UserVm
{
    public class LoginVm
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
