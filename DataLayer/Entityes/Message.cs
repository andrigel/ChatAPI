using System;

namespace DataLayer.Entityes
{
    public class Message
    {
        public Guid Id { get; set; }
        public virtual Chat Chat { get; set; }
        public Guid ChatId { get; set; }
        public virtual ApplicationUser Author { get; set; }
        public string AuthorId { get; set; }
        public DateTime TimeStamp { get; set; }
        public Guid? IsAnswerForId { get; set; }
        public Guid? Reciever { get; set; }
        public DateTime? LastModify { get; set; }
        public bool DeletedForOwner { get;set; } = false;
        public string Text { get; set; }
    }
}
