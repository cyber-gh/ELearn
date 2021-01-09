using System;
using ELearn.Domain;
using Microsoft.AspNetCore.Identity;

namespace ELearn.Infrastructure.Entity.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FullName { get; set; }


        public AppUser ToModel()
        {
            return new AppUser()
            {
                Id = Guid.Parse(Id),
                FullName = FullName,
                Email = Email,
                IsConfirmed = EmailConfirmed
                
            };
        }
    }
}