using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreChat.Models
{
    public interface IChatRepository
    {
        Chat AddChat(Chat chat);
        ChatMessage AddMessage(ChatMessage message);
        PagedResults<Chat> GetChats(string query, int page, int limit);
        PagedResults<ChatMessage> GetMessages(int chatId, int page, int limit);
    }
}
