
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class EFMessageRepository : IMessageRepository
    {
        private readonly EFDBContext _context;

        public EFMessageRepository(EFDBContext context)
        {
            _context = context;
        }
        public async Task DeleteForAnybody(Guid messageId, string userId)
        {
            var message = await _context.Messages.FindAsync(messageId);
            if ((message != null) && (message.AuthorId == userId))
            {
                _context.Messages.Attach(message);
                _context.Messages.Remove(message);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteForOwner(Guid messageId, string userId)
        {
            var message = await _context.Messages.Where(m => m.Id == messageId).FirstOrDefaultAsync();
            if ((message != null) && (message.AuthorId == userId))
            {
                message.DeletedForOwner = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateMessage(Guid messageId, string userId, string newText)
        {
            var message = await _context.Messages.Where(m => m.Id == messageId).FirstOrDefaultAsync();
            if ((message != null) && (message.AuthorId == userId))
            {
                try
                {
                    _context.Messages.Attach(message);
                    message.Text = newText;
                    message.LastModify = DateTime.Now;
                    _context.Entry(message).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex) { }

            }
        }
    }
}
