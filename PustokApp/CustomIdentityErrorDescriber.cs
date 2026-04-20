using Microsoft.AspNetCore.Identity;

namespace PustokApp
{
    public class CustomIdentityErrorDescriber: IdentityErrorDescriber
    { 
        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateUserName),
                Description = $"Username '{userName}' is already taken."
            };
        }
        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateEmail),
                Description = $"Email '{email}' is already taken."
            };
        }
    }
}
