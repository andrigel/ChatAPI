using DataLayer.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    interface IMessageRepository
    {
        public Task DeleteForOwner();
        public Task DeleteForAnybody();
        public Task AddMessage(string userId, Guid ConversationId, string text, Guid? answerForMessageId = null);
    }
}
