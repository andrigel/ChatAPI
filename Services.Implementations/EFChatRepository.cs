using Automapper.Models;
using AutoMapper;
using DataLayer;
using DataLayer.Entityes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMapper _mapper;

        public EFChatRepository(EFDBContext context, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task AddMessage(Guid chatId, string text, string authorId, Guid? isAnswerFor)
        {
            var chat = await _context.Chats.FindAsync(chatId);
            var author = await _userManager.FindByIdAsync(authorId);
            if((text!=null)&&(text != "")&&(chat != null)&&(author!=null))
            {
                _context.Messages.Add(new Message
                {
                    Author = author,
                    DeletedForOwner = false,
                    ChatId = chat.Id,
                    IsAnswerForId = isAnswerFor,
                    TimeStamp = DateTime.Now,
                    Text = text
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

        public bool CheckUserInChat(Guid chatId, string userId)
        {
            var userChat = _context.UserChats.Where(uc => ((uc.UserId == userId) && (uc.ChatId == chatId))).FirstOrDefault();
            return userChat == null ? true : false;
        }

        public async Task<List<MessageModel>> GetMessagesForUser(string userId, Guid chatId, int howMany = 20, int part = 0)
        {
            var user = await _context.ApplicationsUsers.Where(u => u.Id == userId).FirstOrDefaultAsync();
            var chat = await _context.Chats.Where(c => c.Id == chatId).FirstOrDefaultAsync();
            if ((user == null) || (chat == null) || (CheckUserInChat(chatId,userId))) return null;

            int count = _context.Messages.Where(m => !((m.DeletedForOwner == true) && (m.AuthorId == userId)))
                .Count();
            int parts = (int)count / howMany;
            int from = part * howMany;


            return await _context.Messages.Where(m => (m.ChatId == chatId)&&
                !((m.DeletedForOwner == true) && (m.AuthorId == userId)))
                .OrderByDescending(m => m.TimeStamp)
                .Skip(from)
                .Take(from + howMany > count ? count - from : howMany)
                .Select(m => _mapper.Map<Message,MessageModel>(m))
                .ToListAsync();
        }
    }
}
