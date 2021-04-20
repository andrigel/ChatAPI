using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automapper.Models
{
    public class MessageModel
    {
        public Guid Id { get; set; }
        public Guid ChatId { get; set; }
        public string AuthorId { get; set; }
        public DateTime TimeStamp { get; set; }
        public Guid? IsAnswerForId { get; set; }
        public bool DeletedForOwner { get; set; }
        public Guid? Reciever { get; set; }
        public DateTime? LastModify { get; set; }
        public string Text { get; set; }
    }
}
