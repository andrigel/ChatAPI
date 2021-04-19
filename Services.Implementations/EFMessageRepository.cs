
using Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class EFMessageRepository : IMessageRepository
    {
        public Task DeleteForAnybody(Guid messageId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteForOwner(Guid messageId)
        {
            throw new NotImplementedException();
        }
    }
}
