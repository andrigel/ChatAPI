using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DataLayer.Entityes
{
    public class ApplicationUser : IdentityUser
    {
        public List<UserChat> UserChats { get; set; }
    }
}
