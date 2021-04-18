using System;

namespace DataLayer.Entityes
{
    public class Message
    {
        public Guid Id { get; set; }
        public virtual Conversation Conversation { get; set; }
        public virtual ApplicationUser Author { get; set; }
        public DateTime TimeStamp { get; set; }
        public Message IsAnswerFor { get; set; }
    }
}
