using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreChat.Models
{
    public class Message
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int ChatID { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public User User { get; set; }
    }
}
