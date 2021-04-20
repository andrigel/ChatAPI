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
        public static async Task<bool> InitUsers(UserManager<ApplicationUser> userManager)
        {
            if(userManager.Users.ToList().Count == 0)
            {
                await userManager.CreateAsync(new ApplicationUser { Email = "1", UserName = "1" },"1Qwer_");
                await userManager.CreateAsync(new ApplicationUser { Email = "2", UserName = "2" },"2Qwer_");
            }
            return true;
        }
        public static void InitChats(EFDBContext context)
        {
            if (!context.Chats.Any())
            {
                context.Chats.Add(new Chat { Id = new Guid("6F9619FF-8B86-D011-B42D-00CF4FC964FF") });
                context.SaveChanges();
            }
            if(!context.UserChats.Any())
            {
                var u1 = context.ApplicationsUsers.Where(u => u.Email == "1").FirstOrDefault();
                var u2 = context.ApplicationsUsers.Where(u => u.Email == "2").FirstOrDefault();
                context.UserChats.Add(new UserChat
                {
                    ChatId = new Guid("6F9619FF-8B86-D011-B42D-00CF4FC964FF"),
                    UserId = u1.Id
                });
                context.UserChats.Add(new UserChat
                {
                    ChatId = new Guid("6F9619FF-8B86-D011-B42D-00CF4FC964FF"),
                    UserId = u2.Id
                });
                context.SaveChanges();
            }
        }

        public static void InitMessages(EFDBContext context)
        {
            var u1 = context.ApplicationsUsers.Where(u => u.Email == "1").FirstOrDefault();
            var u2 = context.ApplicationsUsers.Where(u => u.Email == "2").FirstOrDefault();
            if (!context.Messages.Any())
            {
                var firstMessage = context.Messages.Add(new Message
                {
                    AuthorId = u2.Id,
                    ChatId = new Guid("6F9619FF-8B86-D011-B42D-00CF4FC964FF"),
                    DeletedForOwner = false,
                    Text = "Hello friend!",
                    TimeStamp = DateTime.Now
                });

                context.Messages.Add(new Message
                {
                    AuthorId = u2.Id,
                    ChatId = new Guid("6F9619FF-8B86-D011-B42D-00CF4FC964FF"),
                    DeletedForOwner = false,
                    Text = "Hello hello!",
                    TimeStamp = DateTime.Now
                });

                context.Messages.Add(new Message
                {
                    AuthorId = u2.Id,
                    ChatId = new Guid("6F9619FF-8B86-D011-B42D-00CF4FC964FF"),
                    DeletedForOwner = true,
                    Text = "Deleted for owner message",
                    TimeStamp = DateTime.Now,
                });

                context.Messages.Add(new Message
                {
                    AuthorId = u1.Id,
                    ChatId = new Guid("6F9619FF-8B86-D011-B42D-00CF4FC964FF"),
                    DeletedForOwner = false,
                    Text = "How do you do?",
                    TimeStamp = DateTime.Now,
                    IsAnswerForId = firstMessage.Entity.Id
                });
                context.SaveChanges();
            }
        }
    }
}