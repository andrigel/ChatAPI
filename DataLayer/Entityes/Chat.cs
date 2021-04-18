using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.Entityes
{
    public class Chat
    {
        public Guid Id { get; set; }
        public DateTime LastMessageTime {
            get { return Messages.Last()?.TimeStamp ?? new DateTime(); }
        }
        public virtual List<UserChat> UserChats { get; set; }
        public virtual List<Message> Messages { get; set; }
    }
}
