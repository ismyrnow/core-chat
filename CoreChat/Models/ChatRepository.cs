using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreChat.Models
{
    public class ChatRepository : IChatRepository
    {
        private IUserRepository Users;
        private List<Chat> _chats = new List<Chat>();
        private List<Message> _messages = new List<Message>();
        private int _id;

        public ChatRepository(IUserRepository users)
        {
            Users = users;
            _id = 0;

            // Add test chats.

            _chats.Add(new Chat
            {
                ID = _id++,
                Created = DateTime.Now,
                LastMessage = null,
                Name = "Alpha",
                UserID = 0
            });

            _chats.Add(new Chat
            {
                ID = _id++,
                Created = DateTime.Now,
                LastMessage = null,
                Name = "Bravo",
                UserID = 1
            });
        }

        public Chat Add(Chat newChat)
        {
            var chat = new Chat
            {
                ID = _id++,
                Name = newChat.Name,
                UserID = newChat.UserID,
                Created = DateTime.Now,
                LastMessage = null
            };

            _chats.Add(chat);

            return chat;
        }

        public PagedResults<Chat> GetChats(string query, int page, int limit)
        {
            // Note: This does not protect against invalid page or limit values.

            var allChats = _chats.OrderBy(x => x.ID)
                .Where(x => x.Name.Contains(query));

            var allCount = allChats.Count();
            var pageCount = (int)Math.Ceiling((double)allCount / limit);
            var hasNextPage = pageCount > page;
            var hasPrevPage = page > 1;

            var pagedChats = allChats
                .Skip(limit * (page - 1))
                .Take(limit);

            foreach(var chat in pagedChats)
            {
                chat.User = Users.FindByID(chat.UserID);
            }

            var pagination = new Pagination
            {
                PageCount = pageCount,
                CurrentPage = page,
                HasNextPage = hasNextPage,
                HasPrevPage = hasPrevPage,
                Count = allCount,
                Limit = limit
            };

            return new PagedResults<Chat>
            {
                Data = pagedChats,
                Pagination = pagination
            };
        }

        public PagedResults<Message> GetMessages(int chatId, int page, int limit)
        {
            // Note: This does not protect against invalid page or limit values.

            var allMessages = _messages.OrderBy(x => x.ID)
                .Where(x => x.ChatID == chatId);

            var allCount = allMessages.Count();
            var pageCount = (int)Math.Ceiling((double)allCount / limit);
            var hasNextPage = pageCount > page;
            var hasPrevPage = page > 1;

            var pagedMessages = allMessages
                .Skip(limit * (page - 1))
                .Take(limit);

            var pagination = new Pagination
            {
                PageCount = pageCount,
                CurrentPage = page,
                HasNextPage = hasNextPage,
                HasPrevPage = hasPrevPage,
                Count = allCount,
                Limit = limit
            };

            return new PagedResults<Message>
            {
                Data = pagedMessages,
                Pagination = pagination
            };
        }
    }
}
