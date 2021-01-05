using Microsoft.AspNetCore.Identity;

namespace ELearn.Infrastructure.Entity.Models
{
    public class ApplicationUser: IdentityUser
    {
     

        public ApplicationUser(string userName) : base(userName)
        {
        }

        public ApplicationUser()
        {
        }
    }
}