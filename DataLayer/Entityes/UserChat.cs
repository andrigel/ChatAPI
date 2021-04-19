using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entityes
{
    public class UserChat
    {
        [Key]
        Guid Id { get; set; }
        public virtual ApplicationUser User { get; set;}
        public virtual Chat Chat { get; set; }
        public string UserId { get; set; }
        public Guid ChatId { get; set; }
    }
}
