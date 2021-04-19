using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entityes;
using Microsoft.AspNetCore.Identity;

namespace DataLayer
{
    public static class SampleData
    {
        public static void InitConversations(EFDBContext context)
        {
            Random rand = new Random();
            if (!context.Chats.Any())
            {

                context.SaveChanges();
            }
        }
        public static async Task<bool> InitUsers(UserManager<ApplicationUser> userManager)
        {
            if(userManager.Users.ToList().Count == 0)
            {
                await userManager.CreateAsync(new ApplicationUser { Email = "1", UserName = "1" },"1Qwer_");
                await userManager.CreateAsync(new ApplicationUser { Email = "2", UserName = "2" },"2Qwer_");
            }
            return true;
        }
    }
}