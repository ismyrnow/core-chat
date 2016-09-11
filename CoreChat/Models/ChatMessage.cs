using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreChat.Models
{
    public class ChatMessage
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int ChatID { get; set; }
        public string Message { get; set; }
        public DateTime Created { get; set; }
        public SimpleUser User { get; set; }
    }
}
