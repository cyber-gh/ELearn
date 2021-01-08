using Microsoft.AspNetCore.Identity;

namespace ELearn.Infrastructure.Entity.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FullName { get; set; }
       
    }
}