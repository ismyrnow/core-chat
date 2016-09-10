using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreChat.Models
{
    public class Chat
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public Message LastMessage { get; set; }
        public User User { get; set; }
    }
}
