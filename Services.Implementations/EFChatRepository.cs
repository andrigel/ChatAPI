using DataLayer;
using DataLayer.Entityes;
using Microsoft.AspNetCore.Identity;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class EFChatRepository : IChatRepository
    {
        private readonly EFDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EFChatRepository(EFDBContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task AddMessage(Guid chatId, string text, string authorId, Guid? isAnswerFor)
        {
            var chat = await _context.Chats.FindAsync(chatId);
            var author = await _userManager.FindByIdAsync(authorId);
            Message answerFor = null;
            if (isAnswerFor != null)
            {
                answerFor = await _context.Messages.FindAsync(isAnswerFor);
            }
            if((chat != null)&&(author!=null))
            {
                _context.Messages.Add(new Message
                {
                    Author = author,
                    DeletedForOwner = false,
                    ChatId = chat.Id,
                    IsAnswerFor = answerFor,
                    TimeStamp = DateTime.Now
                });
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddUser(Guid chatId, string userId)
        {
            var chat = await _context.Chats.FindAsync(chatId);
            var user = await _userManager.FindByIdAsync(userId);
            if ((chat != null) && (user != null))
            {
                _context.UserChats.Add(new UserChat
                {
                    ChatId = chat.Id,
                    UserId = userId
                });
                await _context.SaveChangesAsync();
            }
        }

        public async Task CreateChat(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var chat = await _context.Chats.AddAsync(new Chat());
            if(user!= null)
            {
                var userChat = new UserChat { UserId = userId, ChatId = chat.Entity.Id };
                await _context.UserChats.AddAsync(userChat);
                await _context.SaveChangesAsync();
            }

        }

        public async Task DeleteChat(Guid chatId)
        {
            var chat = await _context.Chats.FindAsync(chatId);
            _context.Chats.Remove(chat);
            await _context.SaveChangesAsync();
        }

        public bool CheckUserInChat(Chat chat, ApplicationUser user)
        {
            var userChat = _context.UserChats.Where(uc => ((uc.UserId == user.Id) && (uc.ChatId == chat.Id))).FirstOrDefault();
            return userChat != null ? true : false;
        }

        public async Task<List<Message>> GetMessagesForUser(string userId, Guid chatId, int howMany = 20, int part = 0)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var chat = await _context.Chats.FindAsync(chatId);
            if ((chat == null) || (user == null)||CheckUserInChat(chat,user))return null;

            var count = chat.Messages.Where(m => m.DeletedForOwner == false && m.Author == user).Count();
            int parts = (int)count / howMany;
            int from = howMany * part;
            /*int to = from + howMany; if(to > count) { to = count; }*/

            var data = chat.Messages.Where(m => m.DeletedForOwner == false && m.Author == user)
                .OrderByDescending(c => c.TimeStamp)
                .Skip(from)
                .Take(howMany);
            return data.ToList();
        }
    }
}
