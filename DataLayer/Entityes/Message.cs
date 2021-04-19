using System;

namespace DataLayer.Entityes
{
    public class Message
    {
        public Guid Id { get; set; }
        public virtual Chat Chat { get; set; }
        public Guid ChatId { get; set; }
        public virtual ApplicationUser Author { get; set; }
        public DateTime TimeStamp { get; set; }
        public Message IsAnswerFor { get; set; }
        public bool DeletedForOwner { get;set; } = false;
        public string Text { get; set; }
    }
}
