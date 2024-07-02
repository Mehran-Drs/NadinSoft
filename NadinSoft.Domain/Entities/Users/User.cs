using Microsoft.AspNetCore.Identity;

namespace NadinSoft.Domain.Entities.Users
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
