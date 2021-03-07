using System;
using Microsoft.AspNetCore.Identity;

namespace RollingRetention.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            RegistrationDate = DateTime.Now;
        }

        public DateTime? RegistrationDate { get; set; }
        public DateTime? LastActivityDate { get; set; }
    }
}