using DataLayer.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IChatRepository
    {
        public Task CreateChat(string userId);
        public Task AddUser(Guid chatId, string userId);
        public Task AddMessage(Guid chatId,string text, string authorId, Guid? isAnswerFor = null);
        public Task DeleteChat(Guid chatId);
        public Task<List<Message>> GetMessagesForUser(string userId,Guid chatId,int howMany = 20,int part = 0);
    }
}
