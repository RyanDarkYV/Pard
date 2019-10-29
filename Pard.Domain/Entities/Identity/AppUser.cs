using System;
using Microsoft.AspNetCore.Identity;

namespace Pard.Domain.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public DateTime JoinDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
