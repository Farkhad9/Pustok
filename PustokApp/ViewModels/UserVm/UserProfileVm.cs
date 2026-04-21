using PustokApp.Models;

namespace PustokApp.ViewModels.UserVm
{
    public class UserProfileVm
    {
        public UserProfileInfoVm UserProfileInfo { get; set; }
        public List<Order> Orders { get; set; }
    }
}
