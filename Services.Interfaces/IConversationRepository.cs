using DataLayer.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IConversationRepository
    {
        public Task CreateConversation();
        public Task AddUser();
        public Task AddMessage();
        public Task DeleteConversation();
        public List<Message> GetMessagesForUser(string userId,Guid conversationId,int howMany = 20,int partFromTheEnd = 0);
    }
}
