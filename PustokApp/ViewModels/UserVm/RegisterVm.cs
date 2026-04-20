using System.ComponentModel.DataAnnotations;

namespace PustokApp.ViewModels.UserVm
{
    public class RegisterVm
    {
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string FullName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
