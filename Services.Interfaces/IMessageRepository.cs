using DataLayer.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IMessageRepository
    {
        public Task DeleteForOwner(Guid messageId, string userId);
        public Task DeleteForAnybody(Guid messageId, string userId);
        public Task UpdateMessage(Guid messageId, string userId, string newText);
    }
}
