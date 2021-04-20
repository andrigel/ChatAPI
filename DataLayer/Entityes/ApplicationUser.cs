using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DataLayer.Entityes
{
    public class ApplicationUser : IdentityUser
    {
        public virtual List<UserChat> UserChats { get; set; }
        public virtual List<Message> Messages { get; set; }
    }
}
