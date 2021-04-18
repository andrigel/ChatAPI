using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.Entityes
{
    public class Conversation
    {
        public Guid Id { get; set; }
        public bool IsGroup { get; set; }
        public DateTime LastMessageTime {
            get { return Messages.Last()?.TimeStamp ?? new DateTime(); }
        }
        public virtual List<UserConversation> UserConversations { get; set; }
        public virtual List<Message> Messages { get; set; }
    }
}
